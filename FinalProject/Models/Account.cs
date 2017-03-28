using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using FinalProject.Utilities;


namespace FinalProject.Models
{
    
    public class Account
    {
        //account number primary key
        [Display(Name = "Account ID")]
        public Int32 AccountID { get; set; }

        //account constructor
        public Account(Transaction FirstTransaction)
        {
            //set amount from transaction
            Balance = FirstTransaction.Amount;
            //auto increment account number
            AccountNumber = AccountUtitlities.AccountNumber + 1;
            AccountType = AccountUtitlities.SetupAccount(AccountType);


        }
        //account number for display
        public Int32 AccountNumber { get; set; }
        /*
        private Int32 _intAccountNumber;
        public Int32 AccountNumber
        {
            get { return _intAccountNumber; }
            //auto increment the values each time it is instantiated
            set { _intAccountNumber = AccountUtitlities.AccountNumber + 1; }
        }
        */

        //account name
        [Required(ErrorMessage = "Account name is required")]
        public string Name { get; set; }

        //calculated balance
        //logic needed to recognize >5000 deposits
        //actual balance will be computed through the saving transactions class
        //set private backing variable
        [DisplayFormat(DataFormatString = "{0:C}")]      
        public Decimal Balance { get; set; }


        public string AccountType { get; private set; }

    
        //link to transactions
        public virtual List<Transaction> SavingTransactions { get; set; }
    }
}