using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinalProject.Models;
using FinalProject.DAL;

namespace FinalProject.Utilities
{

    public static class AccountUtitlities
    {
        //static class for validation and other misc items

        
        
        //list for account types
        public static List<string> AccountTypes()
        {
            List<string> AccountTypeList = new List<string>();

            AccountTypeList.Add("Savings");
            AccountTypeList.Add("Checking");
            AccountTypeList.Add("IRA");
            AccountTypeList.Add("Stock");         

            return AccountTypeList;
        }

        public static string NullName(BankAccount account)
        {
            string Name;

            Name = "Longhorn " + account.AccountType;

            return Name;
                    
        }

        //Noah recommendations: Auto increment bank account here. Get largest current bank account and add one. 
        //Call method on the controller. 
        //public static Int32 AccountNumber = 1000000000;
        public static Int32 AddAccountNumber(Int32 LargestAccountNumber)
        {
            Int32 NewAccountNumber;
            NewAccountNumber = LargestAccountNumber + 1;

            return NewAccountNumber;
        }

        public static Int32 SetAccountNumber(AppDbContext db)
        {
            Int32 account_number;


            var query = from a in db.Accounts
                        select a.AccountNumber;

            var query2 = from p in db.Portfolios
                         select p.AccountNumber;

            List<int> AccountNumbers = query.ToList();

            AccountNumbers.AddRange(query2.ToList());

            account_number = AccountNumbers.Max() + 1;

            return account_number;

        }





    }
}
 