using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class SavingTransactions
    {

        //transaction id
        [Display(Name ="Transaction Number")]
        [Key]
        public Int32 SavingTransactionsID { get; set; }



        //transaction date
        [Display(Name ="Transaction Date")]
        [Required(ErrorMessage =("A transaction date is required"))]
        [DisplayFormat(DataFormatString ="{0:MM.dd.yyyy}",ApplyFormatInEditMode =true)]
        public DateTime Date { get; set; }


        [Display(Name ="Transaction Amount")]
        [Required(ErrorMessage ="Enter an amount")]
        [DisplayFormat(DataFormatString ="{0:C}")]
        public Decimal Amount { get; set; }


        //same from other places
        public void Deposit ()
        {
            Savings.Balance = Amount + Savings.Balance;
        }
        

        //link to the associated account
        public virtual Saving Savings { get; set; }

        //other comments?
        public string Comments { get; set; }





    }
}