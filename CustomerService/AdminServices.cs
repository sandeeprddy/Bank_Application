using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace AllServices
{
    public class AdminServices
    {
        
        public static void ChangeDefaultCurrency(string targetBankId, Dictionary<string, Bank> allBanks, string currencyCode)
        {
            allBanks[targetBankId].currency = currencyCode.ToUpper();
        }

        
        public static void AddCurrencyToAcceptedCurrencies(string targetBankId, Dictionary<string, Bank> allBanks,string currencyCode, double currencyExchangeValue)
        {
            allBanks[targetBankId].AcceptedCurrencies.Add(currencyCode.ToUpper(), currencyExchangeValue);
        }

        
        public static void CreateBank(string name, string location, Dictionary<string, Bank> allBanks)
       {
            Bank newBank = new Bank(name.ToUpper(), location);
            allBanks.Add(newBank.Name, newBank);
       }
       
        
        public static void CreateNewStaffAccount(string firstName, string lastName, string email, string password,Bank targetBank)
        { 
            Staff newStaffAccount = new(firstName, lastName, email, password, targetBank.Name);

            targetBank.StaffAccounts.Add(newStaffAccount.ID, newStaffAccount);

        }

        
        public static void DisplayAcceptedCurrencies(string targetBankId, Dictionary<string, Bank> allBanks)
        {

            foreach (KeyValuePair<string, double> currencies in allBanks[targetBankId].AcceptedCurrencies)
            {
                Console.WriteLine(currencies.Key);
            }
        }

        
        public static Bank GetBank(string bankName, Dictionary<string, Bank> allBanks)
        {
            if (allBanks.ContainsKey(bankName))
            {
                return (allBanks[bankName]);
            }
            else
            {
                return null;
            }
        }

    }
}
