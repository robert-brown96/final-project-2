using System;
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
        //constructor that automatically applies transaction when instantiated
        public Transaction()
        {
            Accounts.Balance = ChangeAmount(Accounts.Balance);
        }

        //transaction id
        [Display(Name ="Transaction Number")]
        [Key]
        public Int32 TransactionID { get; set; }



        //transaction date
        [Display(Name ="Transaction Date")]
        [Required(ErrorMessage =("A transaction date is required"))]
        [DisplayFormat(DataFormatString ="{0:MM.dd.yyyy}",ApplyFormatInEditMode =true)]
        public DateTime Date { get; set; }


        [Display(Name ="Transaction Amount")]
        [Required(ErrorMessage ="Enter an amount")]
        [DisplayFormat(DataFormatString ="{0:C}")]
        public Decimal Amount { get; set; }


        //type of transaction
        public Types type { get; set; }


        //change amount method
        public Decimal ChangeAmount (decimal Balance)
        {
            

            if (type == Types.Deposit)
            {
                //add to the amount
                Balance = Amount + Balance;
            }
            else if (type == Types.Withdraw)
            {
                
                //subtract from amount
                Balance -= Amount;
            }
            return Balance;
            //logic for transfers should go here

            

        }


        //other comments?
        public string Comments { get; set; }

        //link to the associated account
        public virtual BankAccount Accounts { get; set; }

        





    }
}