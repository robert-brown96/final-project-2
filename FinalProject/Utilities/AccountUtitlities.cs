using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinalProject.Models;

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

        public static string NullName(string type)
        {
            string Name;

            Name = "Longhorn" + type;

            return Name;
                    
        }

       

        //account number property
        static Int32 _intAccountNumber;

        public static Int32 AccountNumber
        {
            get { return _intAccountNumber; }
            set { _intAccountNumber += 1; }
        }
 
    }
}
 