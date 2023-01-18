using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Transaction
    {
        public string Id { get; set; }
        public Customer Sender { get; set; }
        public Customer Receiver { get; set; }
        public double MoneyTransferred { get; set; }

        public string TransactionInfo { get; set; }

        public Transaction(string id, Customer sender, Customer receiver, double moneyTransferred)
        {
            Id = id;
            Sender = sender;
            Receiver = receiver;
            MoneyTransferred = moneyTransferred;
        }
    }
}
