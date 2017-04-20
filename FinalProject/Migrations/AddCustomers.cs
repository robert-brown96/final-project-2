using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinalProject.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using FinalProject.DAL;

namespace FinalProject.Migrations
{
    public class SeedIdentity
    {
        public static void SeedUsers(AppDbContext db)
        {
            //create a user manager to add users to databases
            UserManager<AppUser> userManager = new UserManager<AppUser>(new UserStore<AppUser>(db));

            //create a role manager
            AppRoleManager roleManager = new AppRoleManager(new RoleStore<AppRole>(db));

            //create an admin role
            String roleName = "Admin";
            //check to see if the role exists
            if (roleManager.RoleExists(roleName) == false) //role doesn't exist
            {
                roleManager.Create(new AppRole(roleName));
            }

            //create a user
            String strEmail = "cbaker@freezing.co.uk";
            AppUser user = new AppUser()
            {
                
                UserName = strEmail,
                Email = strEmail,
                FName = "Christopher",
                LName = "Baker",
                PhoneNumber = "5125571146",
                MInitial = char.Parse("L"),
                Address = "1245 Lake Austin Blvd.",
                Zip = "78733",              
                City = "Austin",
                State = "TX",
                Birthday = DateTime.ParseExact("1991-02-07", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)


            };
            //see if the user is already there
            AppUser userToAdd = userManager.FindByName(strEmail);
            if (userToAdd == null) //this user doesn't exist yet
            {
                userManager.Create(user, "Password123!");
                userToAdd = userManager.FindByName(strEmail);

                //add the user to the role
                if (userManager.IsInRole(userToAdd.Id, roleName) == false) //the user isn't in the role
                {
                    userManager.AddToRole(userToAdd.Id, roleName);
                }
            }
        }
    }
}