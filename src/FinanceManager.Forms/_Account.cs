using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Forms {
    public class _Account {
        public string name { get; private set; }
        public int id { get; private set; }
        public List<_Transfare> transfsres = new List<_Transfare>();

        public _Account(int id, string name) {
            this.name = name;
            this.id = id;
        }
        public void addTransfare(string description, double amount, DateTime done, _Account ownerAccount) {
            if (!transfsres.Count.Equals(0)) {
                //int highCount = 0;
                transfsres.Add(new _Transfare(transfsres.Count + 1, description, amount, done, ownerAccount));
            } else {
                transfsres.Add(new _Transfare(1, description, amount, done, ownerAccount));
            }
                        
        }
        public void Print() {
            Console.WriteLine("ID: " + id);
            Console.WriteLine("Name: " + name);
            Console.WriteLine("transfares: " + transfsres.Count);
            foreach (var item in transfsres) {
                item.Print();
            }
        }
    }
}
