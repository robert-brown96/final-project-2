using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class Transfer
    {

        //transaction id
        [Display(Name = "Transaction Number")]
        public Int32 TransferID { get; set; }



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
        [Display(Name = "Description of Transaction")]
        public string Description { get; set; }




        //other comments?
        public string Comments { get; set; }

        //link to the associated account
        public virtual BankAccount Account { get; set; }

        
    }
}