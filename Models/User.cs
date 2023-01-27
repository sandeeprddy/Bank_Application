using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Enum_Types;

namespace Models
{
     public  class User
    {
        public UserTypes.Types UserType { get; set; }
        public string? ID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? BankName { get; set; }
        public List<String> AccountIDs { get; set; }
    }
}
