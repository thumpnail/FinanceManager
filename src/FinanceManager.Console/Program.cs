using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using FinanceManager.Forms;
using ChunkGenerator;

namespace FinanceManager {
    public class Program {
        public static void Main(string[] args) {
            Console.WriteLine("Starting Chunk Generator");
            ChunkGen.DownloadCSV();
            Console.WriteLine(""+DateTime.Now.ToString());
            Console.ReadKey();
            ChunkGenerator.ChunkLogger.LOGITwriter.Close();
        }

        public static void forFinanceManager() {
            Console.WriteLine("Hallo World");
            Thread MainFormThread = new Thread(new ThreadStart(MainForm.StartForm));
            MainFormThread.Start();
        }
    }
}
