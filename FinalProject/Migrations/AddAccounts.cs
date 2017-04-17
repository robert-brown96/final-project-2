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
            using (var sr = new StreamReader(@"AccountsOnly.csv"))
            {
                var csv = new CsvReader(sr);
                var records = csv.GetRecords<BankAccount>().ToList();

                Console.WriteLine(records);
            }
            
                
        }
        
    }
}