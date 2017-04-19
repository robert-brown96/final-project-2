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
using Microsoft.AspNet.Identity;

namespace FinalProject.Controllers
{
    public class StockPortfoliosController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: StockPortfolios
        public ActionResult Index()
        {
            return View(db.Portfolios.ToList());
        }

        // GET: StockPortfolios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockPortfolio stockPortfolio = db.Portfolios.Find(id);
            if (stockPortfolio == null)
            {
                return HttpNotFound();
            }
            return View(stockPortfolio);
        }

        // GET: StockPortfolios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StockPortfolios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StockPortfolioID,AccountNumber,CashBalance,Name,InitialDeposit")] StockPortfolio stockPortfolio)
        {
            //put initial deposit in cash balance
            stockPortfolio.CreateStockPortfolio();

            //assign user
            stockPortfolio.User.Id = User.Identity.GetUserId();

            //assign account number
            stockPortfolio.AccountNumber = AccountUtitlities.SetAccountNumber(db);

            //assign defualt name if left blank
            if (stockPortfolio.Name == null)
            {
                stockPortfolio.Name = AccountUtitlities.NullName(stockPortfolio);
            }


            if (ModelState.IsValid)
            {
                db.Portfolios.Add(stockPortfolio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stockPortfolio);
        }

        // GET: StockPortfolios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockPortfolio stockPortfolio = db.Portfolios.Find(id);
            if (stockPortfolio == null)
            {
                return HttpNotFound();
            }
            return View(stockPortfolio);
        }

        // POST: StockPortfolios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StockPortfolioID,AccountNumber,CashBalance,Name")] StockPortfolio stockPortfolio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stockPortfolio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stockPortfolio);
        }

        // GET: StockPortfolios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockPortfolio stockPortfolio = db.Portfolios.Find(id);
            if (stockPortfolio == null)
            {
                return HttpNotFound();
            }
            return View(stockPortfolio);
        }

        // POST: StockPortfolios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StockPortfolio stockPortfolio = db.Portfolios.Find(id);
            db.Portfolios.Remove(stockPortfolio);
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
