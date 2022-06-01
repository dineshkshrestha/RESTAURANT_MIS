using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

            string To = "dinesh11shrestha@gmail.com";
            string CC = "dinesh.shrestha@civilbank.com.np";
            string Message = "<h1>This is sample email</h1> <br><br><h1>Hello World</h1>";
            string Subject = "Sample Email";


            SendEmail(To, CC, Message, Subject);


            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name");
            ViewBag.ItemId = new SelectList(db.ITEMS, "Itemid", "Name");
            ViewBag.TableId = new SelectList(db.Tables, "Tableid", "TableName");
            return View(db.Orders.Where(a=>a.Status!=3).ToList());
        }

        [HttpPost]
        public JsonResult GetItemPrice(int ItemId)
        {
            var item = db.ITEMS.Find(ItemId);
            return Json(item.Price);
        }


        [HttpPost]
        public JsonResult GetOrderPrice(int ItemId)
        {
            List<dynamic> resultData = new List<dynamic>();
            var item = db.ITEMS.Find(ItemId);
            if (item != null)
            {
                resultData.Add(new {
                    item.Price,
                    item.Name,
                    type="success",
                    message = "Item Found"
                });
            }
            else
            {
                resultData.Add(new
                {
                    type = "error",
                    message = "Item Not Found"
                });
            }

            return new JsonResult()
            {
                Data=resultData,
                ContentType = "application/json",
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };

        }

            [HttpPost]
        public JsonResult SaveOrder(int customerId,int tableId,int ItemId,float Qty, float Price, float Total )
        {
            List<dynamic> resultData = new List<dynamic>();
            try
            {
                Orders order = new Orders()
                {
                    CustomerId = customerId,
                    TableId = tableId,
                    ItemId = ItemId,
                    Quantity = Qty,
                    Price = Price,
                    Total = Total,
                    Status = 1,
                    CreatedAt = DateTime.Now.ToString(),
                    CreatedBy = "dinesh"
                };

                db.Orders.Add(order);
                db.SaveChanges();


                resultData.Add(new
                {
                    type = "success",
                    message = "Order Saved successfully."
                });
            }
            catch (Exception ex)
            {
                resultData.Add(new
                {
                    type = "error",
                    message = "Error in saving your order: Error: "+ex.Message
                });
            }
            return new JsonResult()
            {
                Data=resultData,
                ContentType = "application/json",
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
        }

        private static void SendEmail(string ToEmail, string Cc, string Message, string Subject)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient();
               // MailAddress senderMail = new MailAddress("dinesh111shrestha@gmail.com", "Restaurant MIS");
             //   MailAddress senderMail = new MailAddress("dinesh@synergy.com.np", "Restaurant MIS");
                MailAddress senderMail = new MailAddress("test@synergy.com.np", "Restaurant MIS");
                MailAddress receiverMail = new MailAddress(ToEmail);
                MailAddress ccEmail = new MailAddress(Cc);

                MailMessage mail = new MailMessage();
                mail.From = senderMail;
                mail.To.Add(receiverMail);
             //   mail.CC.Add(ccEmail);
                mail.Priority = MailPriority.High;
                mail.Subject = Subject;
                mail.Body = Message;
                mail.IsBodyHtml = true;

                smtpClient.UseDefaultCredentials = true;
              //  smtpClient.Credentials = new NetworkCredential("dinesh111shrestha@gmail.com", "");
              //  smtpClient.Credentials = new NetworkCredential("dinesh@synergy.com.np", "w4Rn@{*Oe!;_");
                smtpClient.Credentials = new NetworkCredential("6b4303c46cd6eb", "66adb48fe8f4d2");
                smtpClient.EnableSsl = true;
                //smtpClient.Port = 587;
              //  smtpClient.Host = "smtp.gmail.com";
              //  smtpClient.Port = 465;
               smtpClient.Port = 2525;
             //   smtpClient.Host = "mail.synergy.com.np";
                smtpClient.Host = "smtp.mailtrap.io";
                smtpClient.Timeout = 0;
                smtpClient.Send(mail);
                smtpClient.Dispose();


            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
           }

          

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