using System.Reflection;
using Models;

namespace AllServices
{
    public  class CustomerService
    {
        public  static void Deposit(Account customerAccount, double money)
        {
            customerAccount.Balance += money;
        }

        public static void Withdraw(Account customerAccount, double amount)
        {
             customerAccount.Balance -= amount;
        }
       
        public static void ViewTransaction(Account customerAccount)
            {
                foreach (Transaction transaction in customerAccount.Transactions.Values)
                {
                    Console.WriteLine(transaction.TransactionInfo);
                };
        }

        public static void Transfer(Account sender,Account receiver, Bank senderBank, Bank receiverBank, double moneyToTransfer, double transactionCharge,double senderAccountAmountDebited, ref double receiverAccountAmountCredited)
        {
           Withdraw(sender, moneyToTransfer);

            if (senderBank.currency != receiverBank.currency)
            {
                double sendersCurrencyValue = senderBank.AcceptedCurrencies[senderBank.currency];
                double receiversCurrencyValue = receiverBank.AcceptedCurrencies[receiverBank.currency];

                if (senderBank.currency != "INR" && receiverBank.currency != "INR")
                {
                    double senderCurrencyToINR = (moneyToTransfer - transactionCharge) * (sendersCurrencyValue);

                    double INRToReceiversCurrency = (senderCurrencyToINR / receiversCurrencyValue);

                    receiver.Balance += INRToReceiversCurrency;

                    receiverAccountAmountCredited = INRToReceiversCurrency;

                }
                else
                {
                    if (sendersCurrencyValue > receiversCurrencyValue)
                    {
                        moneyToTransfer = (moneyToTransfer - transactionCharge) * (sendersCurrencyValue);

                        receiver.Balance += moneyToTransfer;
                    }
                    else
                    {
                        moneyToTransfer = (moneyToTransfer - transactionCharge) / (sendersCurrencyValue);

                        receiver.Balance += moneyToTransfer;
                    }

                    receiverAccountAmountCredited = moneyToTransfer;
                }
            }
            else
            {
                receiver.Balance += (moneyToTransfer - transactionCharge);
            }

           
        }

        public static void GenerateTransactionInfo(Account sender, Account receiver, Bank senderBank, Bank receiverBank, double senderAccountAmountDebited, double receiverAccountAmountCredited, string senderFirstName, string receiverFirstName)
        {
            string senderTransactionID = "TXN" + senderBank.ID + sender.ID + DateTime.Now.ToString("");

            string receiverTransactionID = "TXN" + (receiverBank).ID + receiver.ID + DateTime.Now.ToString("");

            Transaction SenderTransaction = new(senderTransactionID, sender, receiver, senderAccountAmountDebited, receiverTransactionID);
           
            Transaction ReceiverTransaction = new(receiverTransactionID, sender, receiver, receiverAccountAmountCredited, senderTransactionID);

            sender.Transactions.Add(senderTransactionID, SenderTransaction);

            receiver.Transactions.Add(receiverTransactionID, ReceiverTransaction);

            string senderTransactionInfo = senderFirstName + " sent " + senderAccountAmountDebited + senderBank.currency + " to " + receiverFirstName;

            string receiverTransactionInfo = receiverFirstName + " received " + receiverAccountAmountCredited + receiverBank.currency + " from " + senderFirstName;

            sender.Transactions[senderTransactionID].TransactionInfo = senderTransactionInfo;

            receiver.Transactions[receiverTransactionID].TransactionInfo = receiverTransactionInfo;

        }
    }

}

