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
                WaitLib.wait.waitMin(10);
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
            string chunkFolderName = Directory.GetCurrentDirectory() + @"\Data\" + "Chunk" + DateTime.Now.ToString("yyyy.MM.dd_HH-mm-ss");
            ChunkGenerator.ZipToChunk.CreateSample(chunkFolderName + @".CompressedChunk.zip", "0f1befeecde23c7ba0d361a001dbebeb0806e9d54be771c4c83d276867c4997e", genFolderName, 6);
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
        private static void EncryptChunk(string input, string output) {
            string password = "7f480082f660d66760c10e5ac3afe8519e4b977cb3d0bcfeca424f0c0a3b282a";
            GCHandle gch = GCHandle.Alloc(password, GCHandleType.Pinned);
            DycryptionForChunks.FileEncrypt(input, output, password);
        }
    }
}
