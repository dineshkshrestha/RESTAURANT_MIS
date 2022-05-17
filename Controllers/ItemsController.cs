using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RESTAURANT_MIS.Controllers
{
    public class ItemsController : Controller
    {

        private RestaurantEntities db = new RestaurantEntities();

        
        // GET: Items
        public ActionResult Index()
        {
            return View(db.ITEMS.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ITEMS item)
        {
            if (ModelState.IsValid)
            {

              //  item.Status = 1;
                item.CreatedBy = "Admin";
                item.CreatedAt = DateTime.Now.ToString();

                db.ITEMS.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }



        public ActionResult Details(int id)
        {
            ITEMS item = db.ITEMS.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }


        public ActionResult Edit(int id)
        {
            ITEMS item = db.ITEMS.Find(id);


            //ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CustomerName", item.CustomerId);

            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }








    }
}