﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinalProject.DAL;

namespace FinalProject.Models
{
    public class StockQuote
    {
            public String Symbol { get; set; }
            public String Name { get; set; }
            public Double PreviousClose { get; set; }
            public Double LastTradePrice { get; set; }
            public Double Volume { get; set; }

           
    }
}
