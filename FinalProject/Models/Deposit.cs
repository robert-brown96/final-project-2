using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class Deposit
    {
        //transaction id
        [Display(Name = "Transaction Number")]
        public Int32 DepositID { get; set; }

        public Types type
        {
            get { return Types.Deposit; }
        }

        //transaction date
        [Display(Name = "Transaction Date")]
        [Required(ErrorMessage = ("A transaction date is required"))]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }


        [Display(Name = "Transaction Amount")]
        [Required(ErrorMessage = "Enter an amount")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal Amount { get; set; }


        

        ////Description of Transaction
        //"withdrawal from Account123232131"
        [Display(Name = "Description of Transaction")]
        public string Description { get; set; }

        //other comments?
        public string Comments { get; set; }

        //link to the associated account
        public virtual BankAccount Account { get; set; }

        //TODO: the controller will have to be modified 
        //we should be able to overload the create and edit methods to recognize stock accounts
      //  public virtual StockPortfolio Portfolio { get; set; }


    }
}