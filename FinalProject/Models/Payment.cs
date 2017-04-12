using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class Payment
    {

        //key
        public Int32 PaymentID { get; set; }


        [Required(ErrorMessage ="Date is required")]
        public DateTime Date { get; set; }


        [Required(ErrorMessage ="Amount is required")]
        public Decimal Amount { get; set; }


        //link to user
        public virtual AppUser User { get; set; }

        //link to payee
        public virtual Payee Payee { get; set; }
    }
}