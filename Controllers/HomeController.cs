using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RESTAURANT_MIS.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        RestaurantEntities db = new RestaurantEntities();

        public ActionResult Index()
        {

            TempData["Message"] = "Welcome to ASP.NET MVC!";
            ViewData["MessageType"] = "Success";

            ViewBag.TotalCustomer = db.Customers.Count();
           
            ViewBag.TotalOrders = db.Orders.Count();
            ViewBag.TotalItems=db.ITEMS.Count();
            return View();
        }

        public JsonResult GetItemsDetails()
        {
            List<ITEMS> items = db.ITEMS.ToList();

            List<dynamic> datas = new List<dynamic>();
            foreach(var item in items)
            {
                datas.Add(new {
                    item.Name,
                        item.Price
                });

            }
            return new JsonResult(){
                Data=datas,
                JsonRequestBehavior=JsonRequestBehavior.AllowGet
            };
        }




        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();                                                                                                                                                                                                                              
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}