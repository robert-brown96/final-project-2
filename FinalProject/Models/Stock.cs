using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinalProject.Utilities;

namespace FinalProject.Models
{
    public enum StockTypes { Ordinary, IndexFund, ETF, Mutual, Futures }
    public class Stock
    {
        //primary key
        public int StockID { get; set; }

        public string Symbol { get; set; }

        public string Name { get; set; }


        private Double _decCurrentPrice;
        public Double CurrentPrice
        { 
            get
            {
                StockQuote stock = GetQuote.GetStock(Symbol);
                _decCurrentPrice = stock.PreviousClose;
                return _decCurrentPrice;
            }
             
        }

        public Double Fee { get; set; }

        public StockTypes StockType { get; set; }

       

       


    }
}