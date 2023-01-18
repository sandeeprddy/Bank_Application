using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Models;

namespace AllServices
{
    public class ValidationServices
    {
        
        public static bool ValidateAdmin(string adminId, string adminPassword)
        {
            return (adminId == "admin" && adminPassword == "admin");
        }

        
        public static Boolean ValidateBank(string BankName, Dictionary<string, Bank> AllBanks)
        {
            return (AllBanks.ContainsKey(BankName.ToUpper()));
        }

        
        public static Boolean ValidateAccount(Bank currentUsersBank, string AccountID)
        {
            return (currentUsersBank.CustomerAccounts.ContainsKey(AccountID));
        }

        
        public static Boolean ValidateStaff(string Id, string password, Bank Bank)
        {
            return (Bank.StaffAccounts.ContainsKey(Id) && Bank.StaffAccounts[Id].Password == password);
        }

        
        public static Boolean EmailValidator(string email)
        {

            Regex patternForEmailValidation = new("^[a-z0-9]+@([-a-z0-9]+.)+[a-z]{2,5}$");
            return (patternForEmailValidation.IsMatch(email));
        }

        
        public static Boolean PasswordValidator(string password)
        {
            Regex PatternForPasswordValidation = new("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$");
            return PatternForPasswordValidation.IsMatch(password);
        }

        
        public static Boolean ValidateBankCurrency(string BankId, Dictionary<string, Bank> AllBanks, string currency)
        {
            return (AllBanks[BankId].AcceptedCurrencies.ContainsKey(currency.ToUpper()));
        }

        public static Boolean ValidateTransaction(Customer customer, string transactionId)
        {
           return (customer.Transactions.ContainsKey(transactionId));
        }

    }
}
