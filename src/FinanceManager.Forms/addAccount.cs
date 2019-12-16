using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinanceManager.Forms {
    public partial class addAccount : Form {
        public void START() {
            InitializeComponent();
        }

        private void buttonAccept_Click(object sender, EventArgs e) {
            if (FinanceManager.accounts.Count.Equals(0)) {
                FinanceManager.accounts.Add(new _Account(0, textBoxAccountName.Text));
            } else if (FinanceManager.accounts.Count > 0) {
                FinanceManager.accounts.Add(new _Account(FinanceManager.accounts.Count + 1, textBoxAccountName.Text));
            } else {
                MessageBox.Show("Some Errors Accoured", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
