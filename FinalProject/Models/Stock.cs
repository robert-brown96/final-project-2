using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public enum StockTypes { Ordinary, IndexFund, ETF, Mutual, Futures }
    public class Stock
    {
        //primary key
        public int StockID { get; set; }

        public string Ticker { get; set; }

        public string Name { get; set; }

        public Decimal CurrentPrice { get; set; }

        public decimal Fee { get; set; }

        public StockTypes StockType { get; set; }

        public virtual List<StockTransaction> Transactions { get; set; }


    }
}