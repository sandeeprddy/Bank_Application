using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Customer : Account 
    {
        public double Balance { get; set; }

        public Dictionary<string, Transaction> Transactions = new();

        public Customer(string firstName, string lastName, string email, string password, string bankName)
        {
            this.ID = firstName[..3] + DateTime.Now.ToString("");
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.Password = password;
            this.BankName = bankName;
            this.Balance = 0;
        }
    
    }
}
