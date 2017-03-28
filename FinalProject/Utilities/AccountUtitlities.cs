using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Utilities
{
    public static class AccountUtitlities
    {
        //static class for validation and other misc items


        


        
        //list for account types
        public static List<string> AccountTypes()
        {
            List<string> AccountTypeList = new List<string>();

            AccountTypeList.Add("Saving");
            AccountTypeList.Add("Checking");
            AccountTypeList.Add("IRA");
            AccountTypeList.Add("Stock");         

            return AccountTypeList;
        }

        public static string SetupAccount(string Type)
        {
            List<string> AccList = AccountTypes();
            
            if (AccList.Contains(Type))
            {
                return Type;
            }
            else
            {
                return "1";
            }

            
        }

        //account number property
        public static Int32 AccountNumber = 1000000;
 
    }
}
 