using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalProject.Models;
using FinalProject.Utilities;
using Microsoft.AspNet.Identity;
using FinalProject.DAL;

namespace FinalProject.Controllers
{
    public class TransactionsController : Controller
    {

        private AppDbContext db = new AppDbContext();

        // GET: Transactions
        public ActionResult Index(BankAccount bankAccount)
        {

            var query = from d in db.Deposits
                        where d.Account == bankAccount
                        select d;

            var query1 = from w in db.Withdrawals
                        where w.Account == bankAccount
                        select w;

            var query2 = from t in db.Transfers
                         where t.Account == bankAccount
                         select t;



            List<Deposit> DespositList = query.ToList();





            

            return View();
        }
    }
}