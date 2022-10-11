using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace hairsalon.Data
{
    public class MyContext : DbContext
    {
        public MyContext() : base("name=MyConnectionString")
        {

        }
        public virtual DbSet<Account> Accounts { get; set; }
    }
}