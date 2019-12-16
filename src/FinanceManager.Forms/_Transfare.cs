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
        public _Account ownerAccount;

        public _Transfare(int id, string description, double amount, DateTime done, _Account ownerAccount) {
            this.id = id;
            this.description = description;
            this.amount = amount;
            this.done = done;
            this.ownerAccount = ownerAccount;
        }
        public void Print() {
            Console.WriteLine("ID: " + id);
            Console.WriteLine("Descripotion: " + description);
            Console.WriteLine("Cost: " + amount + "€");
            Console.WriteLine("Done: " + done);
            Console.WriteLine("Owner: " + ownerAccount.id + "//" + ownerAccount.name);
        }
    }
}
