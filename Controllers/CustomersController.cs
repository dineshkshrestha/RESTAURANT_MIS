using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RESTAURANT_MIS;

namespace RESTAURANT_MIS.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        private RestaurantEntities db = new RestaurantEntities();

        // GET: Customers
        public ActionResult Index()
        {
          
            return View(db.Customers.ToList());
        }
        
        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {

                ViewBag.Message = "Invalid Request";
                ViewBag.MessageType = "Error";
                return RedirectToAction("Index");

           //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customers customers = db.Customers.Find(id);
            if (customers == null)
            {
                TempData["Message"] = "Id is not available. Please enter a valid id.";
                TempData["MessageType"] = "Error";
               return RedirectToAction("Index");

            }
            return View(customers);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Customerid,Name,PhoneNumber,Address,Email,Status,CreatedBy,CreatedAt,UpdatedAt,UpdatedBy")] Customers customers)
        {

            if (ModelState.IsValid)
            {
                customers.CreatedAt = DateTime.Now.ToString();
                customers.CreatedBy = "samita";
                customers.Status = 1;


                db.Customers.Add(customers);
                db.SaveChanges();

                TempData["Message"] = "Customer created Successfully";
                TempData["MessageType"] = "Success";

                return RedirectToAction("Index");
            }

            return View(customers);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customers customers = db.Customers.Find(id);
            if (customers == null)
            {
                return HttpNotFound();
            }
            return View(customers);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Customerid,Name,PhoneNumber,Address,Email,Status,CreatedBy,CreatedAt,UpdatedAt,UpdatedBy")] Customers customers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customers);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customers customers = db.Customers.Find(id);
            if (customers == null)
            {
                return HttpNotFound();
            }
            return View(customers);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customers customers = db.Customers.Find(id);
            db.Customers.Remove(customers);
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
