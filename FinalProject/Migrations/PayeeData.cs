using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinalProject.Models;
using FinalProject.DAL;

namespace FinalProject.Migrations
{
    public class AddPayees
    {
        public static AppDbContext db = new AppDbContext();

        public static void SeedPayees()
        {
            Payee p1 = new Payee();
            p1.PayeeName = "City of Austin Water";
            p1.Address = "113 South Congress Ave.";
            p1.PayeeType = "Utilities";
        }
    }
}