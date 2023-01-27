using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Account
    {
        public string ID { get; set; }
        public double Balance { get; set; }

        public Dictionary<string, Transaction> Transactions;

        public Account(string firstName)
        {
            this.ID = firstName[..2] + DateTime.Now.ToString("");
            
            this.Balance = 0;

            Transactions = new Dictionary<string, Transaction>();
        }


    }
}
