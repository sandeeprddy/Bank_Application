using System.Reflection;
using Models;

namespace AllServices
{
    public  class CustomerService
    {
 
        public  static void Deposit(Customer customer, double money)
        {
            customer.Balance += money;
        }

        public static void Withdraw(Customer customer, double amount)
        {
             customer.Balance -= amount;
        }
       
        public static void ViewTransaction(Customer customer)
            {
                foreach (Transaction transaction in customer.Transactions.Values)
                {
                    Console.WriteLine(transaction.TransactionInfo);
                };
        }

        public static void Transfer(Customer sender, Customer receiver, Bank senderBank, Bank receiverBank, double moneyToTransfer, double transactionCharge,double senderAccountAmountDebited, ref double receiverAccountAmountCredited)
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

            GenerateTransactionInfo(sender, receiver, senderBank, receiverBank, senderAccountAmountDebited, receiverAccountAmountCredited);

        }

        public static void GenerateTransactionInfo(Customer sender, Customer receiver, Bank senderBank, Bank receiverBank, double senderAccountAmountDebited, double receiverAccountAmountCredited)
        {
            string SenderTransactionID = "TXN" + senderBank.ID + sender.ID + DateTime.Now.ToString("");

            string ReceiverTransactionID = "TXN" + (receiverBank).ID + receiver.ID + DateTime.Now.ToString("");

            Transaction SenderTransaction = new(SenderTransactionID, sender, receiver, senderAccountAmountDebited, ReceiverTransactionID);
           
            Transaction ReceiverTransaction = new(ReceiverTransactionID, sender, receiver, receiverAccountAmountCredited, SenderTransactionID);

            sender.Transactions.Add(SenderTransactionID, SenderTransaction);

            receiver.Transactions.Add(ReceiverTransactionID, ReceiverTransaction);

            string senderTransactionInfo = sender.FirstName + " sent " + senderAccountAmountDebited + senderBank.currency + " to " + receiver.FirstName;

            string receiverTransactionInfo = receiver.FirstName + " received " + receiverAccountAmountCredited + receiverBank.currency + " from " + sender.FirstName;

            sender.Transactions[SenderTransactionID].TransactionInfo = senderTransactionInfo;

            receiver.Transactions[ReceiverTransactionID].TransactionInfo = receiverTransactionInfo;

        }
    }

}

