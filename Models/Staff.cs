﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Staff : Account
    {
        public Staff(string FirstName, string LastName, string Email, string Password, string BankName)
        {
            this.ID = FirstName[..3] + DateTime.Now.ToString("");
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.Password = Password;
            this.BankName = BankName;
        }
       
    }
}
