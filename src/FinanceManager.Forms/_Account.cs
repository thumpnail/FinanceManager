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
        public void addTransfare(string description, double amount, DateTime done) {
            for (int i = 0; i < transfsres.Count; i++) {
                if (!transfsres[i].id.Equals(i+1)) {
                    transfsres.Add(new _Transfare(i+1, description, amount, done));
                }
            }            
        }
    }
}
