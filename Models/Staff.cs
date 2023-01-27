using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Enum_Types;

namespace Models
{
    public class Staff : User
    {
        public Staff(string FirstName, string LastName, string Email, string Password, string BankName)
        {
            this.ID = FirstName[..3] + DateTime.Now.ToString("");
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.Password = Password;
            this.BankName = BankName;
            this.UserType = UserTypes.Types.Staff;
            this.AccountIDs = null;
        }
       
    }
}
