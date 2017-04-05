using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class StockTransaction
    {

        public Int32 StockTransactionID { get; set; }


        public int Shares { get; set; }


        [Display(Name ="Price when Bought")]
        public decimal BuyPrice { get; set; }

        public virtual Stock Stock { get; set; }
        public virtual StockPortfolio Portfolio { get; set; }


    }
}