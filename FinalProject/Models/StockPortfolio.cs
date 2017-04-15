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
        
        public Int32 StockPortfolioID { get; set; }

        public Int32 AccountNumber { get; set; }

        //cash value
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal CashBalance { get; set; }

        //full balance
        private decimal _decBalance;
        public decimal Balance
        {
            get { return _decBalance; }
            set
            {
                

                
                //here will go something to calculate balance

            }
        }


        // TODO: get this working
        //public virtual List<StockTransaction> StockTransactions { get; set; }

        
    }
}