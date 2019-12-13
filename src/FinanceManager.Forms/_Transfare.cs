using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Forms {
    public class _Transfare {
        public DateTime done { get; set; }
        public string description { get; set; }
        public int id { get; private set; }
        public double amount { get; set; }

        public _Transfare(int id, string description, double amount, DateTime done) {
            this.id = id;
            this.description = description;
            this.amount = amount;
            this.done = done;
        }
    }
}
