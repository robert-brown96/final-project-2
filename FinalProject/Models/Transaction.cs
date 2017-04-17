﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    //enum for transaction types
    public enum Types { Withdraw, Deposit, Transfer }


    public class Transaction
    {
        

        //transaction id
        [Display(Name = "Transaction Number")]
        public Int32 TransactionID { get; set; }



        //transaction date
        [Display(Name = "Transaction Date")]
        [Required(ErrorMessage = ("A transaction date is required"))]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }


        [Display(Name = "Transaction Amount")]
        [Required(ErrorMessage = "Enter an amount")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal Amount { get; set; }


        [Display(Name = "Transaction Type")]
        [Required(ErrorMessage = "Enter the type of transaction")]
        //type of transaction
        public Types type { get; set; }

        ////Description of Transaction
        [Display(Name = "Description of Transaction")]
        public string Description { get; set; }

        


        //other comments?
        public string Comments { get; set; }

        //link to the associated account
        public virtual BankAccount Accounts { get; set; }







    }
}