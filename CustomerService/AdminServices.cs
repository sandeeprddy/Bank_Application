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
        
        public static void ChangeDefaultCurrency(string targetBankId, Dictionary<string, Bank> AllBanks, string currencyCode)
        {
            AllBanks[targetBankId].currency = currencyCode.ToUpper();
        }

        
        public static void AddCurrencyToAcceptedCurrencies(string targetBankId, Dictionary<string, Bank> AllBanks,string currencyCode, double currencyExchangeValue)
        {
            AllBanks[targetBankId].AcceptedCurrencies.Add(currencyCode.ToUpper(), currencyExchangeValue);
        }

        
        public static void CreateBank(string name, string location, Dictionary<string, Bank> AllBanks)
       {
            Bank newBank = new Bank(name.ToUpper(), location);
            AllBanks.Add(newBank.Name, newBank);
       }
       
        
        public static void CreateNewStaffAccount(string firstName, string lastName, string email, string password,Bank TargetBank)
        { 
            Staff newStaffAccount = new(firstName, lastName, email, password, TargetBank.Name);

            TargetBank.StaffAccounts.Add(newStaffAccount.ID, newStaffAccount);

        }

        
        public static void DisplayAcceptedCurrencies(string targetBankId, Dictionary<string, Bank> AllBanks)
        {

            foreach (KeyValuePair<string, double> currencies in AllBanks[targetBankId].AcceptedCurrencies)
            {
                Console.WriteLine(currencies.Key);
            }
        }

        
        public static Bank GetBank(string BankName, Dictionary<string, Bank> AllBanks)
        {
            if (AllBanks.ContainsKey(BankName))
            {
                return (AllBanks[BankName]);
            }
            else
            {
                return null;
            }
        }

    }
}
