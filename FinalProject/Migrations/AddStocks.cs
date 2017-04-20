using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinalProject.Models;
using FinalProject.DAL;

namespace FinalProject.Migrations
{
    public class AddStocks
    {
        public static void AddAllStocks(AppDbContext db)
        {
            Stock s1 = new Stock()
            {
                Name = "Alphabet Inc.",
                Symbol = "GOOG",
                StockType = StockTypes.Ordinary,
                Fee = 25
                //MARK: How to check price dynamically?      
                // want to call stock.currentprice method but not allowed
            };

        }
    }
}