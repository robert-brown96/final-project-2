using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinalProject.Models;
using FinalProject.DAL;

namespace FinalProject.Migrations
{
    public class AddCustomers
    {
        public static AppDbContext db = new AppDbContext();

        public static void SeedAccounts()
        {
            AppUser u1 = new AppUser();
            u1.FName = "Christopher";
            u1.LName = "Baker";
            u1.Email = "cbaker@freezing.co.uk";
            u1.PasswordHash = "hello";
            u1.Zip = "78733";
            u1.Address = "1245 Lake Austin Blvd.";
            u1.PhoneNumber = "5125571146";
            //u1.Birthday = ;

        }

    }
}