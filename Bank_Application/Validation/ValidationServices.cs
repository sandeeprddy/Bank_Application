using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Models;
using Models.Enum_Types;

namespace Bank_Application
{
    public class ValidationServices
    {
        
        public static bool ValidateAdmin(string adminId, string adminPassword)
        {
            return (adminId == "admin" && adminPassword == "admin");
        }

        
        public static Boolean ValidateBank(string bankName, Dictionary<string, Bank> allBanks)
        {
            return (allBanks.ContainsKey(bankName.ToUpper()));
        }

        
        public static Boolean ValidateCustomer(Bank currentUsersBank, string customerID)
        {
            return (currentUsersBank.AllUsers.ContainsKey(customerID) && (currentUsersBank.AllUsers[customerID].UserType.Equals(UserTypes.Types.Customer)));
        }

        public static Boolean ValidateAccount(User user, string accountID)
        {
            return (user.AccountIDs.Contains(accountID));
        }


        public static Boolean ValidateStaff(string Id, string password, Bank bank)
        {
            return (bank.AllUsers.ContainsKey(Id) && bank.AllUsers[Id].Password == password && (bank.AllUsers[Id].UserType.Equals(UserTypes.Types.Staff)));
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

        
        public static Boolean ValidateBankCurrency(string BankId, Dictionary<string, Bank> allBanks, string currency)
        {
            return (allBanks[BankId].AcceptedCurrencies.ContainsKey(currency.ToUpper()));
        }

        public static Boolean ValidateTransaction(Account customer, string transactionId)
        {
           return (customer.Transactions.ContainsKey(transactionId));
        }

    }
}
