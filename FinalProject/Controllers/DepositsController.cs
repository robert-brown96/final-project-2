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
    public class DepositsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Deposits
        public ActionResult Index()
        {
            return View(db.Deposits.ToList());
        }

        // GET: Deposits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deposit deposit = db.Deposits.Find(id);
            if (deposit == null)
            {
                return HttpNotFound();
            }
            return View(deposit);
        }

        // GET: Deposits/Create
        public ActionResult Create()
        {
            ViewBag.AllAccounts = GetAllAccounts();
            return View();
        }

        // POST: Deposits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DepositID,Date,Amount,Description,Comments")] Deposit deposit, Int32 BankAccountID)
        {

            //find selected account
            BankAccount SelectedAccount = db.Accounts.Find(BankAccountID);

            AccountUtitlities.GetDescription(deposit);

            //associate with deposit
            deposit.Account = SelectedAccount;

            //get balance
            Decimal Balance = SelectedAccount.Balance;

            Balance = Balance + deposit.Amount;
            SelectedAccount.Balance = Balance;
            if (ModelState.IsValid)
            {
                db.Deposits.Add(deposit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AllAccounts = GetAllAccounts(deposit);
            return View(deposit);
        }

        // GET: Deposits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deposit deposit = db.Deposits.Find(id);
            if (deposit == null)
            {
                return HttpNotFound();
            }
            ViewBag.AllAccounts = GetAllAccounts(deposit);
            return View(deposit);
        }

        // POST: Deposits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DepositID,Date,Amount,Description,Comments")] Deposit deposit, Int32 BankAccountID)
        {
            if (ModelState.IsValid)
            {
                Deposit deposittoChange = db.Deposits.Find(deposit.DepositID);

                //TODO: insert code for when amount AND bank account ID are being changed. Want the previous account
                //to be subtracted by the original transaction amount and the new account to be added by the new transaction 
                //amount
                if ((deposittoChange.Account.BankAccountID != BankAccountID) && (deposittoChange.Amount != deposit.Amount))
                {
                    BankAccount PreviousAccount = db.Accounts.Find(deposittoChange.Account.BankAccountID);
                    deposit.Account = PreviousAccount;
                    Decimal Balance = PreviousAccount.Balance;
                    //change deposit amount

                    Balance = Balance - deposittoChange.Amount;

                    //add new amount
                    Balance = Balance + deposit.Amount;

                    //update balance
                    PreviousAccount.Balance = Balance;

                    

                    //find selected account
                    BankAccount SelectedAccount = db.Accounts.Find(BankAccountID);
                    deposittoChange.Account = SelectedAccount;
                    Decimal NewAccountBalance = SelectedAccount.Balance;
                    //change balance
                    NewAccountBalance = NewAccountBalance + deposit.Amount;
                    SelectedAccount.Balance = NewAccountBalance;
                }

                if (deposittoChange.Account.BankAccountID != BankAccountID)
                {
                    BankAccount PreviousAccount = db.Accounts.Find(deposittoChange.Account.BankAccountID);

                    //associate with deposit
                    deposit.Account = PreviousAccount;

                    //change the balance of the previous account
                    Decimal Balance = PreviousAccount.Balance;
                    Balance = Balance - deposit.Amount;
                    PreviousAccount.Balance = Balance;


                    //find selected account
                    BankAccount SelectedAccount = db.Accounts.Find(BankAccountID);
                    deposittoChange.Account = SelectedAccount;
                    Decimal NewAccountBalance = SelectedAccount.Balance;
                    //change balance
                    NewAccountBalance = NewAccountBalance + deposit.Amount;
                    SelectedAccount.Balance = NewAccountBalance;
                }

                if (deposittoChange.Amount != deposit.Amount)
                {
                    BankAccount SelectedAccount = db.Accounts.Find(BankAccountID);
                    deposittoChange.Account = SelectedAccount;
                    Decimal Balance = SelectedAccount.Balance;

                    //Subtract by previous amount
                    Balance = Balance - deposittoChange.Amount;

                    //add new amount
                    Balance = Balance + deposit.Amount;

                    //update balance
                    SelectedAccount.Balance = Balance;
                    


                }

                //change all other fields
                deposittoChange.Amount = deposit.Amount;
                deposittoChange.Date = deposit.Date;
                deposittoChange.Description = deposit.Description;
                deposittoChange.Comments = deposit.Comments;

                db.Entry(deposit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AllAccounts = GetAllAccounts(deposit);
            return View(deposit);
        }

        // GET: Deposits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deposit deposit = db.Deposits.Find(id);
            if (deposit == null)
            {
                return HttpNotFound();
            }
            return View(deposit);
        }

        // POST: Deposits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Deposit deposit = db.Deposits.Find(id);
            db.Deposits.Remove(deposit);
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
        public SelectList GetAllAccounts(Deposit deposit)
        {
            //populate list of Accounts
            var query = from b in db.Accounts
                        orderby b.AccountType
                        select b;

            List<BankAccount> allAccounts = query.ToList();

            SelectList list = new SelectList(allAccounts, "BankAccountID", "Name", deposit.Account.BankAccountID);

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
