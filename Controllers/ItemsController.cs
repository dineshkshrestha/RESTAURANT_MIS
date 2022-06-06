using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RESTAURANT_MIS.Controllers
{
    [Authorize]
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
        public ActionResult Create(ITEMS item, HttpPostedFileBase file)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        var Extension = Path.GetExtension(file.FileName);
                      //  var originalName = Path.GetFileName(file.FileName);
                        var NewFileName = "item_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + Extension;
                        var path = Path.Combine(Server.MapPath("~/Content/ItemImages"), NewFileName);
                        file.SaveAs(path);
                        item.Image = NewFileName;
                        // filename= generatedFilename+extension
                        // filename= attachment_20220530091512111.extension

                    }
                    //  item.Status = 1;
                    item.CreatedBy = "Admin";
                    item.CreatedAt = DateTime.Now.ToString();

                    db.ITEMS.Add(item);
                    db.SaveChanges();
                    TempData["Message"] = "Item created Successfully";
                    TempData["MessageType"] = "Success";
                    return RedirectToAction("Index");
                }
              
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Unable to create new item. Error: "+ex.Message;
                TempData["MessageType"] = "Error";
                return View();
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