using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AssetManagementSystem.Models;
using AssetManagementSystem.Services;

namespace AssetManagementSystem.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly TransactionService _transactionService;

        public TransactionsController(ApplicationDbContext applicationDbContext, TransactionService transactionService)
        {
            _applicationDbContext = applicationDbContext;
            _transactionService = transactionService;
        }
      
        // GET: Transactions
        public ActionResult Index()
        {
            var transactions = _applicationDbContext.Transactions.Include(t => t.Employee).Include(t => t.Item);
            return View(transactions.ToList());
        }

        // GET: Transactions/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = _applicationDbContext.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: Transactions/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(_applicationDbContext.Employees, "Id", "Name");
            ViewBag.ItemId = new SelectList(_applicationDbContext.Items, "Id", "ProductName");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Transaction transaction)
        {
           
            if (ModelState.IsValid)
            {
                _transactionService.AddTransaction(transaction);
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(_applicationDbContext.Employees, "Id", "Name", transaction.EmployeeId);
            ViewBag.ItemId = new SelectList(_applicationDbContext.Items, "Id", "ProductName", transaction.ItemId);
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = _applicationDbContext.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(_applicationDbContext.Employees, "Id", "Name", transaction.EmployeeId);
            ViewBag.ItemId = new SelectList(_applicationDbContext.Items, "Id", "ProductName", transaction.ItemId);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                _applicationDbContext.Entry(transaction).State = EntityState.Modified;
                _applicationDbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(_applicationDbContext.Employees, "Id", "Name", transaction.EmployeeId);
            ViewBag.ItemId = new SelectList(_applicationDbContext.Items, "Id", "ProductName", transaction.ItemId);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = _applicationDbContext.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Transaction transaction = _applicationDbContext.Transactions.Find(id);
            _applicationDbContext.Transactions.Remove(transaction);
            _applicationDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _applicationDbContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
