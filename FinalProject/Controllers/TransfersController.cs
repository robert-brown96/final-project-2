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
    public class TransfersController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Transfers
        public ActionResult Index()
        {
            return View(db.Transfers.ToList());
        }

        // GET: Transfers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transfer transfer = db.Transfers.Find(id);
            if (transfer == null)
            {
                return HttpNotFound();
            }
            return View(transfer);
        }

        // GET: Transfers/Create
        public ActionResult Create()
        {
            ViewBag.AllAccounts = GetAllAccounts();
            return View();
        }

        // POST: Transfers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TransferID,Date,Amount,Description,Comments")] Transfer transfer, Int32 BankAccountID, Int32 SecondBankAccountID)
        {
            //find selected account
            BankAccount SelectedAccount = db.Accounts.Find(BankAccountID);
            BankAccount TransfertoAccount = db.Accounts.Find(SecondBankAccountID);


            //add a description
            transfer.Description = "Transfer from" + SelectedAccount.Name + "to" + TransfertoAccount.Name + "on" + transfer.Date;

            //associate with transaction
            transfer.Account = SelectedAccount;
            transfer.Account = TransfertoAccount;

            Decimal Balance = SelectedAccount.Balance;
            Decimal TransfertoBalance = TransfertoAccount.Balance;

            Balance = Balance - transfer.Amount;
            SelectedAccount.Balance = Balance;
            TransfertoBalance = TransfertoBalance + transfer.Amount;
            TransfertoAccount.Balance = TransfertoBalance;


            if (ModelState.IsValid)
            {
                db.Transfers.Add(transfer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AllAccounts = GetAllAccounts(transfer);
            return View(transfer);
        }

        // GET: Transfers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transfer transfer = db.Transfers.Find(id);
            if (transfer == null)
            {
                return HttpNotFound();
            }
            return View(transfer);
        }

        // POST: Transfers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TransferID,Date,Amount,Description,Comments")] Transfer transfer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transfer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(transfer);
        }

        // GET: Transfers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transfer transfer = db.Transfers.Find(id);
            if (transfer == null)
            {
                return HttpNotFound();
            }
            return View(transfer);
        }

        // POST: Transfers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transfer transfer = db.Transfers.Find(id);
            db.Transfers.Remove(transfer);
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

        public SelectList GetAllAccounts(Transfer transfer)
        {
            //populate list of Accounts
            var query = from b in db.Accounts
                        orderby b.AccountType
                        select b;

            List<BankAccount> allAccounts = query.ToList();

            SelectList list = new SelectList(allAccounts, "BankAccountID", "Name", transfer.Account.BankAccountID);

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
