using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RESTAURANT_MIS.Controllers
{
    public class OrderController : Controller
    {

        private RestaurantEntities db = new RestaurantEntities();

        // GET: Order
        public ActionResult Index()
        {
            return View(db.Orders.Where(a=>a.Status!=3).ToList());
        }



        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "Customerid", "Name");
            ViewBag.ItemId = new SelectList(db.ITEMS, "Itemid", "Name");
            ViewBag.TableId = new SelectList(db.Tables, "Tableid", "TableName");
            return View();
        }


        [HttpPost]
        public ActionResult Create(Orders order)
        {
            return View();
        }



        public ActionResult Details(int testid)
        {
          Orders orders= db.Orders.Find(testid);

            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }



        public ActionResult Edit(int id)
        {
            Orders orders = db.Orders.Find(id);

            if (orders == null)
            {
                return HttpNotFound();
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "Customerid", "Name", orders.CustomerId);
            ViewBag.ItemId = new SelectList(db.ITEMS, "Itemid", "Name",orders.ItemId);
            ViewBag.TableId = new SelectList(db.Tables, "Tableid", "TableName",orders.TableId);

            //ViewBag.CustomerId = new SelectList(db.Customers, "Customerid", "Name", orders.CustomerId);
            //ViewBag.ItemId = new SelectList(db.ITEMS, "Itemid", "Name", orders.ItemId);
            //ViewBag.TableId = new SelectList(db.Tables, "Tableid", "TableName", orders.TableId);

            return View(orders);
        }


        [HttpPost]
        public ActionResult Edit(Orders orders)
        {
            if (ModelState.IsValid)
            {
                //orders.UpdatedDate = DateTime.Now.ToString();
                
                db.Entry(orders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "Customerid", "Name", orders.CustomerId);
            ViewBag.ItemId = new SelectList(db.ITEMS, "Itemid", "Name", orders.ItemId);
            ViewBag.TableId = new SelectList(db.Tables, "Tableid", "TableName", orders.TableId);

            return View(orders);
        }



        //[HttpPost]
        //public ActionResult Delete(int id)
        //{
        //    Orders order = db.Orders.Find(id);
        //    if (order == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    order.Status = 3;
        //    db.Entry(order).State = EntityState.Modified;
        //    db.SaveChanges();
        //    return RedirectToAction("Index");

        //}

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Orders order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.Entry(order).State = EntityState.Deleted;
            db.SaveChanges();    
            return RedirectToAction("Index");
        }








    }
}