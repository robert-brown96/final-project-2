using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using FinalProject.Models;



namespace FinalProject.Models
{
    public class Saving
    {
        //account number primary key
        [Display(Name="Account Number")]
        public Int32 SavingID { get; set; }

        //account name
        [Required(ErrorMessage="Account name is required")]
        public string Name { get; set; }

        //calculated balance
        //logic needed to recognize >5000 deposits
        //actual balance will be computed through the saving transactions class
        //set private backing variable
        [DisplayFormat(DataFormatString ="{0:C}")]
        //private Decimal _decBalance;
        /*
        public Decimal Balance
        {
            get { return _decBalance; }
            set { //in here should link to the change amount method in transaction}

        }
       */
        public Decimal Balance { get; set; }



        

        //link to transactions
        public virtual List<Transaction> SavingTransactions { get; set; }


        
    }



}
