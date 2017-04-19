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
        [Key]
        public Int32 PayeeID { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        public String PayeeName { get; set; }

        [Required(ErrorMessage ="Payee Type is required")]
        public string PayeeType { get; set; }

        [Required(ErrorMessage ="Address is required")]
        public string Address { get; set; }


        public virtual List<Payment> Payments { get; set; }
    }
}