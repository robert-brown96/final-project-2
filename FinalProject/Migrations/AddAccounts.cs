using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CsvHelper;
using FinalProject.DAL;
using FinalProject.Models;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Collections;

namespace FinalProject.Migrations
{
    public class AddAccounts
    {
        public static void AddAllAccounts(AppDbContext db)
        {
            BankAccount ba1 = new BankAccount()
            {
                AccountNumber = 1000000,
                Name = "Shan's Stock",
                AccountType = AccountTypes.Stock,
                Balance = 0,
                InitialDeposit = 50,
                // foreign key to app user after users are seeded
            };
        
           
            
                
        }
        
    }
}