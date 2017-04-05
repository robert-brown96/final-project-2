using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using FinalProject.Utilities;
using FinalProject.DAL;

namespace FinalProject.Models
{
    public class StockPortfolio
    {

        //primary key
        public Int32 StockPortfolioID { get; set; }


        //cash value
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal CashBalance { get; set; }
        /*

        //account number for display
        [Display(Name = "Account Number")]
        private Int32 _intAccountNumber;
        public Int32 AccountNumber
        {
            get { return _intAccountNumber; }
            set { _intAccountNumber =}
        }
        */
        //reference to user
        public virtual AppUser User { get; set; }
    }
}