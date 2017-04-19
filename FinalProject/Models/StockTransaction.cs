using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{

    public enum OrderType { Buy, Sell }
    public class StockTransaction
    {

        public Int32 StockTransactionID { get; set; }

        [Required(ErrorMessage ="Must enter a number of shares")]
        public int Shares { get; set; }

        public OrderType Order { get; set; }

        [Display(Name = "Price")]
        public Double Price { get; set; }
        //private Double _doubPrice;
        //public Double Price
        //{
        //    get { return _doubPrice; }
        //    set
        //    {
        //        StockQuote quote = null;
        //        quote = new StockQuote();
        //        quote.PreviousClose = Utilities.GetQuote.GetStock(Stock.Symbol).PreviousClose;
        //        _doubPrice = quote.PreviousClose;


        //    }
        //}

        public virtual Stock Stock { get; set; }
        public virtual StockPortfolio Portfolio { get; set; }


    }
}