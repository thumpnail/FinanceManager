using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace FinanceManager.Forms {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e) {
            Thread thread = new Thread(new ThreadStart(Additional.AdditionalForms.StartDiagramForm));
            thread.Start();
        }
    }
}//was ne scheiße, eyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy
