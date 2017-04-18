using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using FinalProject.Models;
using System.Web.Mvc;
using FinalProject.Utilities;
using FinalProject.DAL;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Models
{
    public class StockPortfolio
    {

        //primary key 
        //[Key, ForeignKey("User")]
        public int StockPortfolioID { get; set; }

        public Int32 AccountNumber { get; set; }

        //cash value
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Display(Name ="Cash Balance")]
        public Double CashBalance { get; set; }

        [Display(Name = "Portfolio Name")]
        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        [Display(Name ="Stock Balance")]
        private Double _decStockBalance;
        public Double StockBalance
        {
            get
            {
                //loop through all owned stocks
                foreach (var item in StockTransactions)
                {
                    double fee = item.Stock.Fee;
                    //temp variable for current values
                    double currentValue;
                    //multiply shares by current price
                    currentValue = item.Shares * item.Stock.CurrentPrice - fee;
                    //add to stock balance
                    _decStockBalance += currentValue;
                }
            return _decStockBalance;
            
            
            }
        }

        [DisplayFormat(DataFormatString = "{0:C}")]
        [Display(Name ="Portfolio Balance")]
        public Double Balance
        {
            get { return _decStockBalance + CashBalance; }           
        }

        [Display(Name = "Initital Deposit")]
        [Required(ErrorMessage = "An initial deposit is required")]
        public double InitialDeposit { get; set; }

        //TODO: add 1:1 relationship
        [Required]
        public virtual AppUser User { get; set; }

       
        public virtual List<StockTransaction> StockTransactions { get; set; }


        public virtual List<Withdraw> Withdrawals { get; set; }
        public virtual List<Deposit> Deposits { get; set; }
        

    }
}