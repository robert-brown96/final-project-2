using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalProject.DAL;
using FinalProject.Models;
using FinalProject.Utilities;


namespace FinalProject.Controllers
{
    public class BankAccountsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: BankAccounts
        public ActionResult Index(/*BankAccount bankaccount*/)
        {
            //String currentnumber = bankaccount.AccountNumber.ToString()
            //int startIndex = 6;
            //int length = 4;
            //String toshowcustomer = currentnumber.Substring(startIndex, length);
            //ViewBag.AccountNumber = "XXXXXX" + toshowcustomer;
            
           
            return View(db.Accounts.ToList());
        }

        public ActionResult Confirmation()
        {
            return View();
        }



        // GET: BankAccounts/Details/5
        public ActionResult Details(int? id, String SearchString)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccount bankAccount = db.Accounts.Find(id);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }


            ////query all three transaction types
            //var query0 = from d in db.Deposits
            //            where d.Account == bankAccount
            //            select d;

            //var query1 = from w in db.Withdrawals
            //             where w.Account == bankAccount
            //             select w;

            //var query2 = from t in db.Transfers
            //             where t.Account == bankAccount
            //             select t;

            //List<object> Transactions = new List<object>();
            //List<object> SelectedTransactions = new List<object>();

            ////count all
            //ViewBag.Count = Transactions.Count();

            ////add queries to list
            //Transactions.AddRange(query0);
            //Transactions.AddRange(query1);
            //Transactions.AddRange(query2);

            ////SearchString is the string from the first textbox
            //if (SearchString == null || SearchString == "") //they didn't select anything
            //{


            //    SelectedTransactions = Transactions;
            //    ViewBag.SelectedTransactionsCount = SelectedTransactions.Count();
                
            //}
            //else
            //{
            //    var deposit_query = db.Deposits.Where(d => d.Description.Contains(SearchString) || d.TransactionNumber.Equals(SearchString));
            //    var withdrawal_query = db.Withdrawals.Where(d => d.Description.Contains(SearchString) || d.TransactionNumber.Equals(SearchString));
            //    var transfer_query = db.Deposits.Where(d => d.Description.Contains(SearchString) || d.TransactionNumber.Equals(SearchString));

            //    SelectedTransactions = deposit_query.ToList();








            //}










            return View(bankAccount);
        }

        // GET: BankAccounts/Create
        public ActionResult Create()
        {
            ViewBag.allAccountTypes = GetAllAccountTypes();
            return View();
        }

        // POST: BankAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BankAccountID,InitialDeposit,Name,Balance,AccountType")] BankAccount bankAccount, AccountTypes AccountType)
        {
            ViewBag.allAccountTypes = GetAllAccountTypes();
            //set account number
            bankAccount.AccountNumber = AccountUtitlities.SetAccountNumber(db);


            if (bankAccount.Name == null)
            {
                bankAccount.Name = AccountUtitlities.NullName(bankAccount);
            }


            
            if (ModelState.IsValid)
            {

                bankAccount.CreateBankAccount();
                /*Int32 LargestAccountNumber = db.Accounts.Select(b => b.AccountNumber).DefaultIfEmpty(999999999).Max();
                Int32 number = AccountUtitlities.AddAccountNumber(LargestAccountNumber);
                bankAccount.AccountNumber = number;*/
                db.Accounts.Add(bankAccount);
                db.SaveChanges();
                return RedirectToAction("Confirmation","BankAccounts");
            }

            ViewBag.allAccountTypes = GetAllAccountTypes();
            return View(bankAccount);
        }



    // GET: BankAccounts/Edit/5
    public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccount bankAccount = db.Accounts.Find(id);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }
            return View(bankAccount);
        }

        // POST: BankAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BankAccountID,InitialDeposit,Name,Balance")] BankAccount bankAccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bankAccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bankAccount);
        }

        // GET: BankAccounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccount bankAccount = db.Accounts.Find(id);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }
            return View(bankAccount);
        }
        //Note: This isn't working. Need to change to accivate deactivate. Multiple dependencies --> won't 
        //delete bc connected to a transaction
        // POST: BankAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BankAccount bankAccount = db.Accounts.Find(id);
            db.Accounts.Remove(bankAccount);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //select list for dropdown
        public SelectList GetAllAccountTypes() //none chosen
        {
            

            //execute query
            List<string> allAccountTypes = AccountUtitlities.AccountTypes().ToList();

            // Build a List<SelectListItem>
            // var selectListItems = allAccountTypes.Select(x => new SelectListItem() { Value = x, Text = x }).ToList();

            //convert list to select list
            SelectList allAccountTypelist = new SelectList(allAccountTypes, "Value", "Text");

            //return the select list
            return allAccountTypelist;
        }
    }
}
