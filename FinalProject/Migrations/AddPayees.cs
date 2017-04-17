using FinalProject.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinalProject.Models;
using System.Data.Entity.Migrations;

namespace FinalProject.Migrations
{
    public static class AddPayees
    {

        public static void AddAllPayees(AppDbContext db)
        {
            Payee p1 = new Payee();
            p1.Address = "113 South Congress Ave.";
            p1.PayeeName = "City of Austin Water";
            p1.PayeeType = "Utilities";
            db.Payees.AddOrUpdate(p => p.PayeeName, p1);

            Payee p2 = new Payee();
            p2.Address = "3500 E. Interstate 10";
            p2.PayeeName = "Reliant Energy";
            p2.PayeeType = "Utilities";
            db.Payees.AddOrUpdate(p => p.PayeeName, p2);

            Payee p3 = new Payee();
            p3.Address = "2500 Salado";
            p3.PayeeName = "Lee Properties";
            p3.PayeeType = "Rent";
            db.Payees.AddOrUpdate(p => p.PayeeName, p3);

            Payee p4 = new Payee();
            p4.Address = "1299 Fargo Blvd.";
            p4.PayeeName = "Capital One";
            p4.PayeeType = "Credit Card";
            db.Payees.AddOrUpdate(p => p.PayeeName, p4);

            Payee p5 = new Payee();
            p5.Address = "10976 Interstate 35 South";
            p5.PayeeName = "Vanguard Title";
            p5.PayeeType = "Mortgage";
            db.Payees.AddOrUpdate(p => p.PayeeName, p5);

            Payee p6 = new Payee();
            p6.Address = "4473 W. 3rd Street";
            p6.PayeeName = "Lawn Care of Texas";
            p6.PayeeType = "Other";
            db.Payees.AddOrUpdate(p => p.PayeeName, p6);





        }
    }
}