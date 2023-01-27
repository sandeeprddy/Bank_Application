using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Models;

namespace AllServices
{
    public class StaffService
    {
        
        public static void CreateAccountForNewCustomer(string firstName, string lastName,string email, string password, Bank bank)
        {
            Customer newCustomerAccount = new(firstName, lastName, email, password,bank.Name);

            bank.AllUsers.Add(newCustomerAccount.ID, newCustomerAccount);

            Account newAccount = new(firstName);

            bank.AllAccounts.Add(newAccount.ID, newAccount);

            newCustomerAccount.AccountIDs.Add(newAccount.ID);
        }

        public static void CreateAccountForExistingCustomer(User customer, Bank bank)
        {
            Account newAccount = new(customer.FirstName);

            bank.AllAccounts.Add(newAccount.ID, newAccount);

            customer.AccountIDs.Add(newAccount.ID);

        }

        public static void DeleteCustomer(Bank bank, User customerToDelete)
        {
            foreach (string accounts in customerToDelete.AccountIDs)
            {
                bank.AllAccounts.Remove(accounts);
            }

            bank.AllUsers.Remove(customerToDelete.ID);
        }
       
        public static void DeleteAccount(Bank bank,User customer,string accountIDToDelete)
        {
            customer.AccountIDs.Remove(accountIDToDelete);

            bank.AllAccounts.Remove(accountIDToDelete);
        }

      
        public static void AddServiceChargeForTransferringBank(Bank staffBank, double updatedRTGSChargeToOtherBank, double updatedIMPSChargeToOtherBank)
        {
             staffBank.TransferRTGSCharge = updatedRTGSChargeToOtherBank;
             staffBank.TransferRTGSCharge = updatedIMPSChargeToOtherBank;
        }

        
        public static void AddServiceChargeForSameBank(Bank bank, double updatedRTGSCharge, double updatedIMPSCharge)
        {
            bank.RTGSCharge = updatedRTGSCharge;
            bank.IMPSCharge = updatedIMPSCharge;
         }

        
        public static void ViewTransaction(Account customer)
        {
            foreach (Transaction transaction in customer.Transactions.Values)
            {
                Console.WriteLine(transaction.TransactionInfo);
            };
        }

        
        public static Boolean ValidateAmountForDebit(Account customerAccount, double amountToWithdraw)
        {
            return (amountToWithdraw <= customerAccount.Balance);
        }

        
        public static Boolean ValidateTransaction(Bank receiverBank, string currency)
        {
            return receiverBank.AcceptedCurrencies.ContainsKey(currency);
        }

        
      /*  public static Customer GetAccount(Bank receiversBank, string accountID)
        {
            return receiversBank.CustomerAccounts[accountID];
        }*/
    }
}
