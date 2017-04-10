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

//*******************************************************
//ELIZABETH HAS MADE CHANGES TO THIS. PLEASE DON'T DELETE
//*******************************************************

namespace FinalProject.Controllers
{
    public class TransactionsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Transactions
        public ActionResult Index()
        {
            return View(db.Transactions.ToList());
        }

        // GET: Transactions
        public ActionResult Summary()
        {
            return View(db.Transactions.ToList());
        }

        // GET: Transactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: Transactions/Create
        public ActionResult Create()
        {
            ViewBag.AllAccounts = GetAllAccounts();
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TransactionID,Date,Amount,type,Comments")] Transaction transaction, Int32 BankAccountID)
        {
            //find selected account
            BankAccount SelectedAccount = db.Accounts.Find(BankAccountID);

            //associate with transaction
            transaction.Accounts = SelectedAccount;

            Decimal Balance = SelectedAccount.Balance;
            if (transaction.type == Types.Withdraw)
            {
                Balance = Balance - transaction.Amount;
                SelectedAccount.Balance = Balance;
            }
            else if (transaction.type == Types.Deposit)
            {
                Balance = Balance + transaction.Amount;
                SelectedAccount.Balance = Balance;
            }
            else
            {
                //insert transfer logic
            }
            
            if (ModelState.IsValid)
            {
                db.Transactions.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AllAccounts = GetAllAccounts(transaction);
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.AllAccounts = GetAllAccounts(transaction);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TransactionID,Date,Amount,type,Comments")] Transaction transaction, Int32 BankAccountID)
        {
            if (ModelState.IsValid)
            {
                //find associated transaction

                Transaction transactiontoChange = db.Transactions.Find(transaction.TransactionID);

                //change Account if necessary 
                if (transactiontoChange.Accounts.BankAccountID != BankAccountID)
                {
                    //find account
                    BankAccount SelectedAccount = db.Accounts.Find(BankAccountID);

                    transactiontoChange.Accounts = SelectedAccount;
                }
                //update all other fields
                transactiontoChange.Date = transaction.Date;
                transactiontoChange.Amount = transaction.Amount;
                transactiontoChange.type = transaction.type;
                transactiontoChange.Comments = transaction.Comments;

                db.Entry(transactiontoChange).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //re-populate list
            ViewBag.AllAccounts = GetAllAccounts(transaction);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            db.Transactions.Remove(transaction);
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

        public SelectList GetAllAccounts(Transaction transaction)
        {
            //populate list of Accounts
            var query = from b in db.Accounts
                        orderby b.AccountType
                        select b;

            List<BankAccount> allAccounts = query.ToList();

            SelectList list = new SelectList(allAccounts, "BankAccountID", "Name", transaction.Accounts.BankAccountID);

            return list;

        }
        public SelectList GetAllAccounts()
        {
            //populate list of Accounts
            var query = from b in db.Accounts
                        orderby b.AccountType
                        select b;

            List<BankAccount> allAccounts = query.ToList();

            SelectList allAccountslist = new SelectList(allAccounts, "BankAccountID", "Name");

            return allAccountslist;

        }
    }
}
