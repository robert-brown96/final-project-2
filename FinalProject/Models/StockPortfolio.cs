using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
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
        

        public virtual List<StockTransaction> StockTransactions { get; set; }

        
    }
}