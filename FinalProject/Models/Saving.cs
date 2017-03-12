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
        //set private backing variabl
        private Decimal _decBalance;
        //hopefully this works
        public Decimal Balance
        {
            //normal get method
            get { return _decBalance; }
            //set method gets fucked
            set
            {
                //iterate through list of objects
                foreach(var SavingTransactions in SavingTransactions)
                {
                    //temp variable from saving transactions model
                    decimal decAmount = SavingTransactions.Amount;
                    //add to total balance
                    _decBalance = _decBalance + decAmount;
                }
            }
        }

       
        

        //link to transactions
        public virtual List<SavingTransactions> SavingTransactions { get; set; }

    }
}