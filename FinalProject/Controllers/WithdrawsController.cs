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
    public class WithdrawsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Withdraws
        public ActionResult Index()
        {
            return View(db.Withdrawals.ToList());
        }

        // GET: Withdraws/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Withdraw withdraw = db.Withdrawals.Find(id);
            if (withdraw == null)
            {
                return HttpNotFound();
            }
            return View(withdraw);
        }

        // GET: Withdraws/Create
        public ActionResult Create()
        {
            ViewBag.AllAccounts = GetAllAccounts();
            return View();
        }

        // POST: Withdraws/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WithdrawID,Date,Amount,Description,Comments")] Withdraw withdraw, Int32 BankAccountID)
        {
            BankAccount SelectedAccount = db.Accounts.Find(BankAccountID);


            //associate with transaction
            withdraw.Account = SelectedAccount;

            Decimal Balance = SelectedAccount.Balance;
            Balance = Balance - withdraw.Amount;
            SelectedAccount.Balance = Balance;
            if (ModelState.IsValid)
            {
                AccountUtitlities.GetDescription(withdraw);
                db.Withdrawals.Add(withdraw);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AllAccounts = GetAllAccounts(withdraw);
            return View(withdraw);
        }

        // GET: Withdraws/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Withdraw withdraw = db.Withdrawals.Find(id);
            if (withdraw == null)
            {
                return HttpNotFound();
            }
            ViewBag.AllAccounts = GetAllAccounts(withdraw);
            return View(withdraw);
        }

        // POST: Withdraws/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WithdrawID,Date,Amount,Description,Comments")] Withdraw withdraw, Int32 BankAccountID)
        {
            if (ModelState.IsValid)
            {
                Withdraw withdrawtoChange = db.Withdrawals.Find(withdraw.WithdrawID);

                //TODO: insert code for when amount AND bank account ID are being changed. Want the previous account
                //to be subtracted by the original transaction amount and the new account to be added by the new transaction 
                //amount
                if ((withdrawtoChange.Account.BankAccountID != BankAccountID) && (withdrawtoChange.Amount != withdraw.Amount))
                {
                    BankAccount PreviousAccount = db.Accounts.Find(withdrawtoChange.Account.BankAccountID);
                    withdraw.Account = PreviousAccount;
                    Decimal Balance = PreviousAccount.Balance;
                    //change withdraw amount

                    Balance = Balance + withdrawtoChange.Amount;

                    //add new amount
                    Balance = Balance - withdraw.Amount;

                    //update balance
                    PreviousAccount.Balance = Balance;

                   

                    //find selected account
                    BankAccount SelectedAccount = db.Accounts.Find(BankAccountID);
                    withdrawtoChange.Account = SelectedAccount;
                    Decimal NewAccountBalance = SelectedAccount.Balance;
                    //change balance
                    NewAccountBalance = NewAccountBalance - withdraw.Amount;
                    SelectedAccount.Balance = NewAccountBalance;
                }

                if (withdrawtoChange.Account.BankAccountID != BankAccountID)
                {
                    BankAccount PreviousAccount = db.Accounts.Find(withdrawtoChange.Account.BankAccountID);

                    //associate with withdraw
                    withdraw.Account = PreviousAccount;

                    //change the balance of the previous account
                    Decimal Balance = PreviousAccount.Balance;
                    Balance = Balance + withdraw.Amount;
                    PreviousAccount.Balance = Balance;


                    //find selected account
                    BankAccount SelectedAccount = db.Accounts.Find(BankAccountID);
                    withdrawtoChange.Account = SelectedAccount;
                    Decimal NewAccountBalance = SelectedAccount.Balance;
                    //change balance
                    NewAccountBalance = NewAccountBalance + withdraw.Amount;
                    SelectedAccount.Balance = NewAccountBalance;
                }

                if (withdrawtoChange.Amount != withdraw.Amount)
                {
                    BankAccount SelectedAccount = db.Accounts.Find(BankAccountID);
                    withdrawtoChange.Account = SelectedAccount;
                    Decimal Balance = SelectedAccount.Balance;

                    //Subtract by previous amount
                    Balance = Balance + withdrawtoChange.Amount;

                    //add new amount
                    Balance = Balance - withdraw.Amount;

                    //update balance
                    SelectedAccount.Balance = Balance;

                    //update field

                }




                //change all other fields
                withdrawtoChange.Amount = withdraw.Amount;
                withdrawtoChange.Date = withdraw.Date;
                withdrawtoChange.Description = withdraw.Description;
                withdrawtoChange.Comments = withdraw.Comments;
                db.Entry(withdraw).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AllAccounts = GetAllAccounts(withdraw);
            return View(withdraw);
        }

        // GET: Withdraws/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Withdraw withdraw = db.Withdrawals.Find(id);
            if (withdraw == null)
            {
                return HttpNotFound();
            }
            return View(withdraw);
        }

        // POST: Withdraws/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Withdraw withdraw = db.Withdrawals.Find(id);
            db.Withdrawals.Remove(withdraw);
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

      
        public SelectList GetAllAccounts(Withdraw withdraw)
        {
            //populate list of Accounts
            var query = from b in db.Accounts
                        orderby b.AccountType
                        select b;

            List<BankAccount> allAccounts = query.ToList();

            SelectList list = new SelectList(allAccounts, "BankAccountID", "Name", withdraw.Account.BankAccountID);

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
