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

namespace FinalProject.Controllers
{
    public class SavingTransactionsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: SavingTransactions
        public ActionResult Index()
        {
            return View(db.SavingTransactions.ToList());
        }

        // GET: SavingTransactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SavingTransactions savingTransactions = db.SavingTransactions.Find(id);
            if (savingTransactions == null)
            {
                return HttpNotFound();
            }
            return View(savingTransactions);
        }

        // GET: SavingTransactions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SavingTransactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SavingTransactionsID,Date,Amount,Comments")] SavingTransactions savingTransactions)
        {
            if (ModelState.IsValid)
            {
                db.SavingTransactions.Add(savingTransactions);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(savingTransactions);
        }

        // GET: SavingTransactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SavingTransactions savingTransactions = db.SavingTransactions.Find(id);
            if (savingTransactions == null)
            {
                return HttpNotFound();
            }
            return View(savingTransactions);
        }

        // POST: SavingTransactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SavingTransactionsID,Date,Amount,Comments")] SavingTransactions savingTransactions)
        {
            if (ModelState.IsValid)
            {
                db.Entry(savingTransactions).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(savingTransactions);
        }

        // GET: SavingTransactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SavingTransactions savingTransactions = db.SavingTransactions.Find(id);
            if (savingTransactions == null)
            {
                return HttpNotFound();
            }
            return View(savingTransactions);
        }

        // POST: SavingTransactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SavingTransactions savingTransactions = db.SavingTransactions.Find(id);
            db.SavingTransactions.Remove(savingTransactions);
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
    }
}
