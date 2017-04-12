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
        [Key]
        public int StockPortfolioID { get; set; }

        public Int32 AccountNumber { get; set; }

        //cash value
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal CashBalance { get; set; }


        [DisplayFormat(DataFormatString = "{0:C}")]
        private decimal _decStockBalance;
        public decimal StockBalance
        {
            get { return _decStockBalance; }
            set
            {
                //loop through all owned stocks
                foreach (var item in StockTransactions)
                {
                    //temp variable for current values
                    decimal currentValue;
                    //multiply shares by current price
                    currentValue = item.Shares * item.Stock.CurrentPrice;
                    //add to stock balance
                    _decStockBalance += currentValue;
                }
            }
        }

        [DisplayFormat(DataFormatString = "{0:C}")]
        //full balance
        private decimal _decBalance;
        public decimal Balance
        {
            get { return _decStockBalance + CashBalance; }
            private set
            {
                //add stock and cash portions
                _decBalance = _decStockBalance + CashBalance;
            }
        }

        /*[ForeignKey("StockPortfolioID")]
        [Required]
        public virtual AppUser User { get; set; }*/

        public virtual List<StockTransaction> StockTransactions { get; set; }

        
    }
}