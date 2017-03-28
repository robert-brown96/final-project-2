using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinalProject.Models;
using System.Data.Entity;

namespace FinalProject.DAL
{
    public class AppDbContext : DbContext
    {
        //Constructor that invokes the base constructor
        public AppDbContext() : base("MyDBConnection") { }

        //Create the db set
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> SavingTransactions { get; set; }
    }
}