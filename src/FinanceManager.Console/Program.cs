using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using FinanceManager.Forms;
using ChunkGenerator;

namespace FinanceManager {
    public static class Program {
        public static string[] help = {
            "Parameter:", "   use 'start' to start a tool", "   use 'start help' to get help about start parameter", @"Usage: .\FinanceManager.Console.exe <Parameter>" 
        };
        public static string[] startHelp = { 
            "Parameter:", "   BTCChunkGenerator", "   FinanceManager", "   BTCDecryption", @"Usage: .\FinanceManager.Console.exe start <Parameter>" 
        };
        public static void Main(string[] args) {
            if (args == null) {
                Console.WriteLine("No args");
            } else if (ArgsHandling.CheckArgs(args)[0] == "start" && ArgsHandling.CheckArgs(args)[1] == "BTCChunkGenerator") {
                forBTCChunkGenerator();
            } else if (ArgsHandling.CheckArgs(args)[0] == "start" && ArgsHandling.CheckArgs(args)[1] == "FinanceManager") {
                forFinanceManager();
            } else if (ArgsHandling.CheckArgs(args)[0] == "start" && ArgsHandling.CheckArgs(args)[1] == "BTCDecryption") {
                forBTCDecryption();
            } else if (ArgsHandling.CheckArgs(args)[0] == "start" && ArgsHandling.CheckArgs(args)[1] == null || ArgsHandling.CheckArgs(args)[0] == "start" && ArgsHandling.CheckArgs(args)[1] == "help") {
                for (int i = 0; i < help.Length; i++) {
                    Console.WriteLine(startHelp[i]);
                }
            } else {
                for (int i = 0; i < help.Length; i++) {
                    Console.WriteLine(help[i]);
                }
            }
            Console.ReadKey();
        }

        public static void forFinanceManager() {
            Console.WriteLine("Hallo World");
            Thread MainFormThread = new Thread(new ThreadStart(MainForm.StartForm));
            MainFormThread.Start();
        }
        public static void forBTCChunkGenerator() {
            ChunkLogger.LOGIT("Starting Chunk Generator");
            ChunkGen.DownloadCSV();
            Console.WriteLine("" + DateTime.Now.ToString());
            ChunkGenerator.ChunkLogger.LOGITwriter.Close();
        }
        public static void forBTCDecryption() {
            try {
                ChunkLogger.LOGIT("Trying to Decrypt");
                ChunkGenerator.ChunkGen.DycryptAllChunk();
            } catch (Exception r) {
                ChunkLogger.LOGIT("An Error showed up. Check Log For Details", r.ToString());
                ChunkLogger.LOGIT("cant fix any error whiched showed up");
            }
            ChunkGenerator.ChunkLogger.LOGITwriter.Close();
        }
    }
    public static class ArgsHandling {
        public static string[] CheckArgs(string[] args) {
            try {
                for (int i = 0; i < args.Length; i++) {
                    if (isStartParameter(args[i]) && args[i + 1] == null) {
                        string[] tmp = { args[i] };
                        return tmp;
                    } else if (isStartParameter(args[i]) && args[i + 1] != null) {
                        string[] tmp = { args[i], args[i + 1] };
                        return tmp;
                    }
                }
            } catch (Exception) {
                
            }
            string[] tmp2 = { "404", "no content" };
            return tmp2;
        }
        public static bool isStartParameter(string arg) {
            if (arg == "start") {
                return true;
            } else {
                return false;
            }
        }
    }
}
