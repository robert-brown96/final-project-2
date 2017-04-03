using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Optimization;
using Microsoft.AspNet.Identity;

namespace FinalProject.Models
{
   
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }


        //phone number, should have been here by default?
        [Required(ErrorMessage = "Phone number is required")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        //TODO:  Add any fields that you need for creating a new user
        //For example, first name
        [Required]
        [Display(Name = "First Name")]
        public string FName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [Display(Name = "Last Name")]
        public string LName { get; set; }

        [Display(Name = "Middle Initial")]
        public char? MInitial { get; set; }

        //address, city, state, zip properties
        //could be simplified somehow I think
        [Required(ErrorMessage = "Address is required")]
        [Display(Name = "Street Address")]
        [RegularExpression(@"^(?:\w+\s?)+\w+$", ErrorMessage = "Street Address not valid.")]
        public string Address { get; set; }
        /*
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }
        */
        [Required(ErrorMessage = "Zip code is required")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "Zip is not valid")]
        public string Zip { get; set; }

        //required birthday property
        //need extra data annotation here
        [Required(ErrorMessage = "Birthday is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Birthday { get; set; }

    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
