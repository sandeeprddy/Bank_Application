﻿using System;
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

        
        public static void CreateCustomerAccount(string firstName, string lastName,string email, string password, Bank Bank)
        {
            Customer newCustomerAccount = new(firstName, lastName, email, password,Bank.Name);

            Bank.CustomerAccounts.Add(newCustomerAccount.ID, newCustomerAccount);

        }

        
        public static void DeleteAccount(Bank Bank,string accountIDToDelete)
        { 
            Bank.CustomerAccounts.Remove(accountIDToDelete);
        }


      
        public static void AddServiceChargeForTransferringBank(Bank staffBank, double updatedRTGSChargeToOtherBank, double updatedIMPSChargeToOtherBank)
        {
             staffBank.TransferRTGSCharge = updatedRTGSChargeToOtherBank;
             staffBank.TransferRTGSCharge = updatedIMPSChargeToOtherBank;
        }

        
        public static void AddServiceChargeForSameBank(Bank Bank, double updatedRTGSCharge, double updatedIMPSCharge)
        {
            Bank.RTGSCharge = updatedRTGSCharge;
            Bank.IMPSCharge = updatedIMPSCharge;
         }

        
        public static void ViewTransaction(Customer customer)
        {
            foreach (Transaction transaction in customer.Transactions.Values)
            {
                Console.WriteLine(transaction.TransactionInfo);
            };
        }

        
        public static Boolean ValidateAmountForDebit(Customer customer, double amountToWithdraw)
        {
            return (amountToWithdraw <= customer.Balance);
        }

        
        public static Boolean ValidateTransaction(Bank receiverBank, string currency)
        {
            return receiverBank.AcceptedCurrencies.ContainsKey(currency);
        }

        
        public static Customer GetAccount(Bank receiversBank, string AccountID)
        {
            return receiversBank.CustomerAccounts[AccountID];
        }
    }
}
