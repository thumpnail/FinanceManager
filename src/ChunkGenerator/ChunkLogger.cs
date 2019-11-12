using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ChunkGenerator {
    public static class ChunkLogger {
        public static StreamWriter LOGITwriter = new StreamWriter(Directory.GetCurrentDirectory() + @"\Chunk.log");
        public static void LOGIT(string msg) {
            msg = DateTime.Now.ToString("hh-mm-ss-fff") + " | " + msg;
            LOGITwriter.WriteLine(msg);
            Console.WriteLine(msg);
        }
        public static void LOGIT(string msg, string msg_logstack) {
            msg = DateTime.Now.ToString("hh-mm-ss-fff") + " | " + msg;
            LOGITwriter.WriteLine(msg);
            LOGITwriter.WriteLine(msg_logstack);
            Console.WriteLine(msg);
        }
    }
}
