using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hairsalon.Data
{
    public class Account
    {
        public int Id { get; set; }
        public int PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
    }
}