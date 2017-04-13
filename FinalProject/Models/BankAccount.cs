using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using FinalProject.Utilities;
using FinalProject.DAL;


namespace FinalProject.Models
{

    public enum AccountTypes { Saving, Checking, IRA, Stock }
    
    public class BankAccount
    {
        //account number primary key
        [Display(Name = "Account ID")]
        [Key]
        public Int32 BankAccountID { get; set; }

        [Display(Name = "Initital Deposit")]
        [Required(ErrorMessage = "An initial deposit is required")]
        public Decimal InitialDeposit { get; set; }

        //account constructor
        public void CreateBankAccount()
        {
            Balance = InitialDeposit;
            //Transaction FirstTransaction = new Transaction();
            ////set amount from transaction
            //Balance = FirstTransaction.Amount;
            ////auto increment account number
            //AccountNumber = AccountUtitlities.AccountNumber + BankAccountID;

            
        }

        
        //account number for display
        [Display(Name ="Account Number")]
        public Int32 AccountNumber { get; set; }
        
        
        
        //account name
        public string Name { get; set; }

        //calculated balance
        //logic needed to recognize >5000 deposits
        //actual balance will be computed through the saving transactions class
        //set private backing variable
        [DisplayFormat(DataFormatString = "{0:C}")]      
        public Decimal Balance { get; set; }


        public AccountTypes AccountType { get; set; }

    
        //link to transactions
        public virtual List<Transaction> Transactions { get; set; }

        public virtual List<Withdraw> Withdrawals { get; set; }
        public virtual List<Deposit> Deposits { get; set; }
        public virtual List<Transfer> Transfers { get; set; }

        //link to user
        public virtual AppUser AppUser { get; set; }
    }
}