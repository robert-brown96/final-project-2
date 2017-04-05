using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using FinalProject.Utilities;

namespace FinalProject.Models
{
    public class Payee
    {

        public Int32 PayeeID { get; set; }

        public string PayeeType { get; set; }

        public string Address { get; set; }

        
    }
}