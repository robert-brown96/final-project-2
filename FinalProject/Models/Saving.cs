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
        public Decimal Balance { get; set; }
       // private Decimal _decBalance;
        /*
        public Decimal Balance
        {

            get { return _decBalance}
            
                /*
                //iterate through list
                //UNHANDLED ERROR HERE
                foreach(var SavingTransactions in SavingTransactions)
                {
                    //temp variable
                    decimal decAmount = SavingTransactions.Amount;
                    _decBalance += decAmount;
                }
                return _decBalance;
            }
            /*
            //normal get method
            get { return _decBalance; }
            //set method 
            set
            {
                //iterate through list of objects
                foreach(Decimal SavingTransactions.Amount in SavingTransactions)
                {
                    //temp variable from saving transactions model
                    decimal decAmount = SavingTransactions.Amount;
                    //add to total balance
                    _decBalance = _decBalance + decAmount;
                }
            }*/
        }

       
        //deposit method
        public void Deposit (SavingTransactions SavingTransaction)
        {
            //get the amount from transaction
            
        }

        //link to transactions
        public virtual List<SavingTransactions> SavingTransactions { get; set; }

    }
}