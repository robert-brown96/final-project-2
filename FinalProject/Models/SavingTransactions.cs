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
        [DisplayFormat(DataFormatString ="{0:dd.MM.yyyy}",ApplyFormatInEditMode =true)]
        public DateTime Date { get; set; }



        [Required(ErrorMessage ="Enter an amount")]
        public Decimal Amount { get; set; }

        /*
        public void Calc_Balance ()
        {
            Savings.Balance = Amount + Savings.Balance;
        }

        */
        //link to the associated account
        public virtual Saving Savings { get; set; }

        //other comments?
        public string Comments { get; set; }





    }
}