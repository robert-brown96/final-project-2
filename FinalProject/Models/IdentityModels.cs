using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using System.Web.Optimization;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class AppUser : IdentityUser
    {
     
        //TODO: Put any additional fields that you need for your user here
        //For instance
        [Required(ErrorMessage = "First name is required")]
        [Display(Name = "First Name")]
        public string FName { get; set; }

        

        [Required(ErrorMessage = "Last name is required")]
        [Display(Name = "Last Name")]
        public string LName { get; set; }

        [Display(Name = "Middle Initial")]
        public char? MInitial { get; set; }

        //address, city, state, zip properties
        //could be simplified somehow I think
        [Required(ErrorMessage = "Addresse is required")]
        [Display(Name = "Street Address")]
        [RegularExpression(@"^(?:\w+\s?)+\w+$", ErrorMessage = "Street Address not valid.")]
        public string Address { get; set; }
        
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }
        
        [Required(ErrorMessage = "Zip code is required")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "Zip is not valid")]
        public string Zip { get; set; }

        //required birthday property
        //need extra data annotation here
        [Required(ErrorMessage = "Birthday is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Birthday { get; set; }

        //account foreign key
        public virtual List<BankAccount> Accounts { get; set; }

        public virtual List<Payment> Payments { get; set; }

        //1:1 with a portfolio
        public virtual StockPortfolio Portfolio { get; set; }

        

        //This method allows you to create a new user
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    
}