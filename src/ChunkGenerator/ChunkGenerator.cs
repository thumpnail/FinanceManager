using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using DownloadManager;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Core;
using WaitLib;
using ArtisanCode.SimpleAesEncryption;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace ChunkGenerator {
    public static class ChunkGen {
        private static string url = @"http://data.bitcoinity.org/export_data.csv?c=e&currency=USD&data_type=price&r=second&t=l&timespan=10m";
        private static string AESPassword = "7f480082f660d66760c10e5ac3afe8519e4b977cb3d0bcfeca424f0c0a3b282a";
        private static string ZipPassword = "0f1befeecde23c7ba0d361a001dbebeb0806e9d54be771c4c83d276867c4997e";
        public static string[] allCmpChnkFullPath;
        public static string[] allCmpChnkFilenames;
        public static string[] allCmpChnkRAWNames;

        public static string data;
        public static int ChunkCounter = 0;
        public static void DownloadCSV() {
            string time = @"" + DateTime.Now;

            Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\Data");

            do {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\Data\_CurrentChunk");
                WebClient wc = new WebClient();
                string file = Directory.GetCurrentDirectory() + @"\Data\_CurrentChunk\" + @"BTCAnalysis_" + DateTime.Now.ToString("yyyy.MM.dd_HH-mm-ss-fff") + ".csv";
                StreamWriter sw = new StreamWriter(file);
                ChunkLogger.LOGIT("ChunkCounter: " + ChunkCounter);
                ChunkLogger.LOGIT("Downloading Data | File: " + file);
                sw.WriteLine(wc.DownloadString(url));
                sw.Close();
                ChunkCounter += 1;
                if (ChunkCounter == 6) {
                    ChunkCounter = 0;
                    Thread thread = new Thread(new ThreadStart(CreateChunk));
                    ChunkLogger.LOGIT("Chunk Creation | Staritng thread");
                    thread.Start();
                }
                WaitLib.wait.waitSec(1);
            } while (true);
        }
        public static void CreateChunk() {
            ChunkLogger.LOGIT("Creating Chunk");
            string genFolderName = Directory.GetCurrentDirectory() + @"\Data\Chunk" + DateTime.Now.ToString("yyyy.MM.dd_HH-mm-ss");
            
            Directory.CreateDirectory(genFolderName);

            string[] allDatasetFullPath = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\Data\_CurrentChunk");
            string[] allDatasetFilenames = new string[allDatasetFullPath.Length];

            ChunkLogger.LOGIT("All Datasets:");

            for (int i = 0; i < allDatasetFullPath.Length; i++) {
                allDatasetFilenames[i] = Path.GetFileName(allDatasetFullPath[i]);
                ChunkLogger.LOGIT(allDatasetFilenames[i]);
            }
                
            for (int i = 0; i < allDatasetFullPath.Length; i++) {
                File.Move(allDatasetFullPath[i], genFolderName + @"\" + allDatasetFilenames[i]);
            }
            string chunkFolderName = genFolderName;
            ChunkGenerator.ZipToChunk.CreateSample(chunkFolderName + @".CompressedChunk.zip", ZipPassword, genFolderName, 6);
            File.Move(chunkFolderName + @".CompressedChunk.zip", chunkFolderName + @".CompressedChunk");
            ChunkLogger.LOGIT("Encrypting Chunk " + chunkFolderName + @".CompressedChunk");
            //Encrypt CmpChnk
            EncryptChunk(chunkFolderName + @".CompressedChunk", chunkFolderName + @".CmpChnk");
            try {
                Directory.Delete(chunkFolderName, true);
            } catch (Exception r) {
                ChunkLogger.LOGIT(r.ToString());
            }
            try {
                File.Delete(chunkFolderName + @".CompressedChunk");
            } catch (Exception r) {
                ChunkLogger.LOGIT(r.ToString());
            }
        }
        //verschlüsseln
        private static void EncryptChunk(string input, string output) {
            GCHandle gch = GCHandle.Alloc(AESPassword, GCHandleType.Pinned);
            DycryptionForChunks.FileEncrypt(input, output, AESPassword);
        }
        public static void DycryptAllChunk() {

            GCHandle gch = GCHandle.Alloc(AESPassword, GCHandleType.Pinned);

            string CmpChnkPath = Directory.GetCurrentDirectory() + @"\Data\";
            allCmpChnkFullPath = Directory.GetFiles(CmpChnkPath);
            allCmpChnkFilenames = allCmpChnkFullPath;
            allCmpChnkRAWNames = allCmpChnkFullPath;

            for (int i = 0; i < allCmpChnkFilenames.Length; i++) {
                ChunkLogger.LOGIT("allCmpChnkFilenames: " + allCmpChnkFilenames[i]);
                ChunkLogger.LOGIT("allCmpChnkFullPath: " + allCmpChnkFullPath[i]);
            }

            for (int i = 0; i < allCmpChnkFilenames.Length; i++) {
                allCmpChnkFilenames[i] = Path.GetFileName(allCmpChnkFilenames[i]);
                ChunkLogger.LOGIT("Filename: " + allCmpChnkFilenames[i]);
            }
            char[] seperator;
            seperator = ".".ToCharArray();
            //Encryption
            for (int i = 0; i < allCmpChnkFilenames.Length; i++) {
                ChunkLogger.LOGIT("Format: " + allCmpChnkFilenames[i]);
                string[] splitStorage = allCmpChnkFilenames[i].Split(seperator);
                ChunkLogger.LOGIT("AFTER SPLIT Format: " + allCmpChnkFilenames[i]);

                for (int x = 0; x < splitStorage.Length; x++) {
                    ChunkLogger.LOGIT("Splited: " + splitStorage[x]);
                }

                allCmpChnkRAWNames[i] = splitStorage[0] + "." + splitStorage[1] + "." + splitStorage[2];
                ChunkLogger.LOGIT("");
                ChunkLogger.LOGIT("RAW Name: " + allCmpChnkRAWNames[i]);
                ChunkLogger.LOGIT("Filename: " + allCmpChnkFilenames[i]);
                ChunkLogger.LOGIT("1st Path: " + CmpChnkPath + allCmpChnkFilenames[i]);
                ChunkLogger.LOGIT("2nd Path: " + CmpChnkPath);
                ChunkLogger.LOGIT("Password: " + AESPassword);
                ChunkLogger.LOGIT("");


                DycryptionForChunks.FileDecrypt(CmpChnkPath + allCmpChnkRAWNames[i] + ".CmpChnk", CmpChnkPath + allCmpChnkRAWNames[i] + ".CommpressedChunk", AESPassword);
            }
            //unzipping
            ChunkLogger.LOGIT("Extracting");
            for (int i = 0; i < allCmpChnkFullPath.Length; i++) {
                ChunkLogger.LOGIT("Extracting: " + allCmpChnkFilenames[i] + ".CommpressedChunk" + " | Target Folder: " + allCmpChnkRAWNames[i]);
                Directory.CreateDirectory(CmpChnkPath + allCmpChnkRAWNames[i]);
                ChunkGenerator.ZipToChunk.ExtractZipFile(CmpChnkPath + allCmpChnkRAWNames[i] + ".CommpressedChunk", ZipPassword, CmpChnkPath + allCmpChnkRAWNames[i]);
            }
            wait.waitSec(2);
            ChunkLogger.LOGIT("Deleting junk");
            for (int i = 0; i < allCmpChnkRAWNames.Length; i++) {
                ChunkLogger.LOGIT("operation " + (i+1) + " of " + (allCmpChnkRAWNames.Length+1));

                ChunkLogger.LOGIT("Deleting: " + CmpChnkPath + allCmpChnkRAWNames[i] + ".CmpChnk");
                //File.Delete(CmpChnkPath + allCmpChnkRAWNames[i] + ".CmpChnk");

                ChunkLogger.LOGIT("Deleting" + CmpChnkPath + allCmpChnkRAWNames[i] + ".CommpressedChunk");
                File.Delete(CmpChnkPath + allCmpChnkRAWNames[i] + ".CommpressedChunk");
            }
            
        }
    }
}
