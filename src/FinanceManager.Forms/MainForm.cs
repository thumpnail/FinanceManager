using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinanceManager.Forms {
    public static class MainForm {
        public static FinanceManager fm;
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        public static void StartForm() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            fm = new FinanceManager();
            Application.Run(fm);
        }
    }
}
