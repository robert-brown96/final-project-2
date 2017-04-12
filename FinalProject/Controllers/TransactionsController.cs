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

//Noah Recommendations: Make transactions each their own controller, and thus own models
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
            //NOTE: THIS CODE WAS ADDED
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
                //TODO: insert transfer logic
            }
            
            if (ModelState.IsValid)
            {
                db.Transactions.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //NOTE: THIS CODE WAS ADDED
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
            //NOTE: THIS CODE WAS ADDED
            ViewBag.AllAccounts = GetAllAccounts(transaction);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TransactionID,Date,Amount,type,Comments")] Transaction transaction, Int32 BankAccountID)  //rename incoming ID
        {
            if (ModelState.IsValid)
            {
                //NOTE: THIS CODE WAS ADDED
                //find associated transaction

                Transaction transactiontoChange = db.Transactions.Find(transaction.TransactionID);

                //***********************************************************************************************
                //NOTE: Balance should be impacted if we change the bank account associated  with the transaction
                //and the amount of a transaction associated with a bank acccount. 
                //***********************************************************************************************
                //change Account if necessary 
                if (transactiontoChange.Accounts.BankAccountID != BankAccountID)
                {
                    BankAccount PreviousAccount = db.Accounts.Find(transactiontoChange.Accounts.BankAccountID);


                    //associate with transaction
                    transaction.Accounts = PreviousAccount;

                    Decimal Balance = PreviousAccount.Balance;
                    if (transaction.type == Types.Withdraw)
                    {
                        Balance = Balance + transaction.Amount;
                        PreviousAccount.Balance = Balance;
                    }
                    else if (transaction.type == Types.Deposit)
                    {
                        Balance = Balance - transaction.Amount;
                        PreviousAccount.Balance = Balance;
                    }
                    else
                    {
                        //TODO: insert transfer logic
                    }

                    //find selected account
                    BankAccount SelectedAccount = db.Accounts.Find(BankAccountID);
                    transactiontoChange.Accounts = SelectedAccount;

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
                        //TODO: insert transfer logic
                    }

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
            //NOTE: THIS CODE WAS ADDED
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

        //NOTE: THIS CODE WAS ADDED
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
        //NOTE: THIS CODE WAS ADDED
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
