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
    public partial class FinanceManager : Form {
        public static List<_Account> accounts = new List<_Account>();

        public void update(List<_Account> accounts) {
            listViewAllTransfares.Clear();

            listViewAllTransfares.Columns.Add("ID");
            listViewAllTransfares.Columns.Add("Description");
            listViewAllTransfares.Columns.Add("Amount");
            listViewAllTransfares.Columns.Add("Done");

            try {
                for (int i = 0; i < accounts.Count; i++) {
                    try {
                        for (int a = 0; a < accounts[i].transfsres.Count; a++) {
                            ListViewItem item = new ListViewItem(accounts[i].transfsres[a].id.ToString());
                            item.SubItems.Add(accounts[i].transfsres[a].description);
                            item.SubItems.Add(accounts[i].transfsres[a].amount.ToString());
                            item.SubItems.Add(accounts[i].transfsres[a].done.ToString());
                            listViewAllTransfares.Items.Add(item);
                        }
                    } catch (Exception) {
                        Console.WriteLine("No transfares found");
                    }
                }
            } catch (Exception) {
                Console.WriteLine("No Accounts found");
            }
            foreach (var item in accounts) {
                comboBoxSelectAccount.Items.Add(item.name);
            }
        }

        public FinanceManager() {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e) {
            //Thread thread = new Thread(new ThreadStart(Additional.AdditionalForms.StartDiagramForm));
            //thread.Start();
        }

        private void FinanceManager_Load(object sender, EventArgs e) {
            Console.WriteLine("Loading...");
            update(accounts);
        }

        private void comboBoxSelectAccount_SelectedIndexChanged(object sender, EventArgs e) {
            //List<>
        }
    }
}//was ne scheiße, eyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy
