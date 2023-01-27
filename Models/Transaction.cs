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
        public Account Sender { get; set; }
        public Account Receiver { get; set; }
        public double MoneyTransferred { get; set; }

        public string TransactionInfo { get; set; }

        public string ReceiverTransactionID { get; set; }

        public Transaction(string id, Account sender, Account receiver, double moneyTransferred, String receiverTransactionID)
        {
            Id = id;
            Sender = sender;
            Receiver = receiver;
            MoneyTransferred = moneyTransferred;
            this.ReceiverTransactionID = receiverTransactionID;
        }
    }
}
