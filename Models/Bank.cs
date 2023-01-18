using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Bank
    {
        public string? ID { get; set; }
        public string? Name { get; set; }  
        public string? Location { get; set; }

        public double RTGSCharge = 0;

        public double IMPSCharge = 0.05;

        public double TransferRTGSCharge = 0.02;

        public double TransferIMPSCharge = 0.06;

        public string currency = "INR";

        public IDictionary<string, double> AcceptedCurrencies { get; set; }


        public IDictionary<string, Staff> StaffAccounts { get;set; }

        public IDictionary<string, Customer> CustomerAccounts { get; set; }

        public IDictionary<string, string> SenderRecieverTransactionsMapping { get; set; }

        public Bank(string Name, string Location) 
        { 
           this.ID = Name[..3] + DateTime.Now.ToString("");
           this.Name = Name;
           this.Location = Location;

            StaffAccounts = new Dictionary<string, Staff>();

            CustomerAccounts = new Dictionary<string, Customer>();

            AcceptedCurrencies = new Dictionary<string, double>
            {
                { "INR", 0 },
                { "USD", 80},
                { "EUR", 90}
            };

            SenderRecieverTransactionsMapping = new Dictionary<string, string>();
        }
    }
}
