﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using FinalProject.Utilities;
using FinalProject.DAL;


namespace FinalProject.Models
{
    
    public class BankAccount
    {
        //account number primary key
        [Display(Name = "Account ID")]
        [Key]
        public Int32 BankAccountID { get; set; }

        //account constructor
        public BankAccount()
        {
            

            Transaction FirstTransaction = new Transaction();
            //set amount from transaction
            Balance = FirstTransaction.Amount;
            //auto increment account number
            AccountNumber = AccountUtitlities.AccountNumber + 1;
            AccountType = AccountUtitlities.SetupAccount(AccountType, Name);


        }

        
        //account number for display
        public Int32 AccountNumber { get; }
        

        //account name
        public string Name { get; set; }

        //calculated balance
        //logic needed to recognize >5000 deposits
        //actual balance will be computed through the saving transactions class
        //set private backing variable
        [DisplayFormat(DataFormatString = "{0:C}")]      
        public Decimal Balance { get; set; }


        public string AccountType { get; }

    
        //link to transactions
        public virtual List<Transaction> Transactions { get; set; }

        //link to user
        public virtual AppUser AppUser { get; set; }
    }
}