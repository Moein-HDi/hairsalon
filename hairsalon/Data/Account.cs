using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace hairsalon.Data
{
    public class Account
    {
        public int Id { get; set; }
        public int PhoneNumber { get; set; }
        [MaxLength(30)]
        public string Role { get; set; }
        [MaxLength(30)]
        public string FirstName { get; set; }
        [MaxLength(30)]
        public string LastName { get; set; }
        [MaxLength(60)]
        public string Password { get; set; }

        public string FullName()
        {
            return this.FirstName + " " + this.LastName;
        }
    }
}