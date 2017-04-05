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


        //account number for display
        [Display(Name = "Account Number")]
        public Int32 AccountNumber { get; set; }

        //reference to user
        public virtual AppUser User { get; set; }
    }
}