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
    public class StockTransactionsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: StockTransactions
        public ActionResult Index()
        {
            return View(db.StockOrders.ToList());
        }

        // GET: StockTransactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockTransaction stockTransaction = db.StockOrders.Find(id);
            if (stockTransaction == null)
            {
                return HttpNotFound();
            }
            return View(stockTransaction);
        }

        // GET: StockTransactions/Create
        public ActionResult Create()
        {
            ViewBag.AllStocks = GetAllStocks();
            return View();
        }

        // POST: StockTransactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StockTransactionID,Shares,Order,Price")] StockTransaction stockTransaction, Int32 StockID)
        {

            Stock SelectedStock = db.Stocks.Find(StockID);

            //associate with transaction
            stockTransaction.Stock = SelectedStock;

            //assign price
            stockTransaction.Price = GetQuote.GetStock(SelectedStock.Symbol).PreviousClose;
            

            if (ModelState.IsValid)
            {
                


                db.StockOrders.Add(stockTransaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AllStocks = GetAllStocks(stockTransaction);
            return View(stockTransaction);
        }

        // GET: StockTransactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockTransaction stockTransaction = db.StockOrders.Find(id);
            if (stockTransaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.AllStocks = GetAllStocks(stockTransaction);
            return View(stockTransaction);
        }

        // POST: StockTransactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StockTransactionID,Shares,Order,Price")] StockTransaction stockTransaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stockTransaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AllStocks = GetAllStocks(stockTransaction);
            return View(stockTransaction);
        }

        // GET: StockTransactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockTransaction stockTransaction = db.StockOrders.Find(id);
            if (stockTransaction == null)
            {
                return HttpNotFound();
            }
            return View(stockTransaction);
        }

        // POST: StockTransactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StockTransaction stockTransaction = db.StockOrders.Find(id);
            db.StockOrders.Remove(stockTransaction);
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


        //select list for all stocks
        public SelectList GetAllStocks(StockTransaction transaction)
        {
            //populate list
            var query = from s in db.Stocks
                        orderby s.Symbol
                        select s;
            List<Stock> allStocks = query.ToList();

            SelectList list = new SelectList(allStocks, "StockID", "Name", transaction.Stock.StockID);

            return list;
        }

        public SelectList GetAllStocks()
        {
            //populate list
            var query = from s in db.Stocks
                        orderby s.Symbol
                        select s;
            List<Stock> allStocks = query.ToList();

            SelectList allStockslist = new SelectList(allStocks, "StockID", "Name");

            return allStockslist;

        }
    }
}
