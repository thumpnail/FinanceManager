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

            listViewAllTransfares.Columns.Add("ID", 50);
            listViewAllTransfares.Columns.Add("Owner", 100);
            listViewAllTransfares.Columns.Add("Description", 150);
            listViewAllTransfares.Columns.Add("Amount", 50);
            listViewAllTransfares.Columns.Add("Done", 180);

            try {
                for (int i = 0; i < accounts.Count; i++) {
                    try {
                        for (int a = 0; a < accounts[i].transfsres.Count; a++) {
                            ListViewItem item = new ListViewItem(accounts[i].transfsres[a].id.ToString());
                            item.SubItems.Add(accounts[i].transfsres[a].ownerAccount.id + "  |  " + accounts[i].transfsres[a].ownerAccount.name);
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
            comboBoxSelectAccount.Items.Clear();
            comboBoxSelectAccount.Items.Add("All");
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
            listViewAllTransfares.View = View.Details;
            listViewAllTransfares.FullRowSelect = true;

            listViewAllTransfares.MultiSelect = false;

            Console.WriteLine("Loading...");

            update(accounts);
        }

        private void comboBoxSelectAccount_SelectedIndexChanged(object sender, EventArgs e) {
            try {
                List<_Account> tmp = new List<_Account>();
                for (int i = 0; i < accounts.Count; i++) {
                    if (accounts[i].name.Equals(comboBoxSelectAccount.SelectedItem)) {
                        tmp.Add(accounts[i]);
                    } else if(comboBoxSelectAccount.SelectedItem.Equals("All")) {
                        tmp.Add(accounts[i]);
                    }
                }
                update(tmp);
            } catch (Exception) {
                Console.WriteLine("Select a Account option");
            }
        }
        public static addAccount aa = new addAccount();

        [STAThread]
        private void addAccountToolStripMenuItem_Click(object sender, EventArgs e) {
            aa.FormClosed += closeAddAccount;
            Thread t = new Thread(new ThreadStart(aa.START));
            t.Start();
        }
        private void closeAddAccount(object sender, EventArgs e) {
            update(accounts);
        }
    }
}//was ne scheiße, eyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy
