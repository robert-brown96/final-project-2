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

            Payee p2 = new Payee();
            p1.PayeeName = "Reliant Energy";
            p1.Address = "3500 E. Interstate 10";
            p1.PayeeType = "Utilities";

            Payee p3 = new Payee();
            p1.PayeeName = "Lee Properties";
            p1.Address = "2500 Salado";
            p1.PayeeType = "Rent";

            Payee p4 = new Payee();
            p1.PayeeName = "Capital One";
            p1.Address = "1299 Fargo Blvd.";
            p1.PayeeType = "Credit Card";

            Payee p5 = new Payee();
            p1.PayeeName = "Vanguard Title";
            p1.Address = "10976 Interstate 35 South";
            p1.PayeeType = "Mortgage";

            Payee p6 = new Payee();
            p1.PayeeName = "Lawn Care of Texas";
            p1.Address = "4473 W. 3rd Street";
            p1.PayeeType = "Other";

        }
    }
}