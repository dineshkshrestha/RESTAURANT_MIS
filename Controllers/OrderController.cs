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
        private string host = "";
        private string port = "";
        private string Username = "";
        private string password = "";


        
        // GET: Order
        public ActionResult Index()
        {

           


            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name");
            ViewBag.ItemId = new SelectList(db.ITEMS, "Itemid", "Name");
            ViewBag.TableId = new SelectList(db.Tables, "Tableid", "TableName");
            return View(db.Orders.Where(a => a.Status != 3).ToList());
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
                resultData.Add(new
                {
                    item.Price,
                    item.Name,
                    type = "success",
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
                Data = resultData,
                ContentType = "application/json",
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };

        }

        [HttpPost]
        public JsonResult SaveOrder(int customerId, int tableId, int ItemId, float Qty, float Price, float Total)
        {
            List<dynamic> resultData = new List<dynamic>();
            try
            {

                var orderId = db.Orders.OrderByDescending(x=>x.Orderid).First().Orderid;
           


                Orders order = new Orders()
                {
                    Orderid=orderId+1,
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

                string To = "kitchen@gmail.com";
                string CC = "billCounter@chickenstation.com";


                var item = db.ITEMS.Find(order.ItemId);
                var table = db.Tables.Find(order.TableId);

                string Message = "<h1>New Order Alert </h1> <br><br><h1>Order Name: "+item.Name+" </h1> <h1>Order Quantity: "+order.Quantity+"</h1>";
                string Subject = "Order of "+item.Name +" In table: "+ table.TableName;


                SendEmail(To, CC, Message, Subject);



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
                    message = "Error in saving your order: Error: " + ex.Message
                });
            }
            return new JsonResult()
            {
                Data = resultData,
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
                MailAddress senderMail = new MailAddress("RestaurantMIS@ChickenStation.com", "Restaurant MIS");
                MailAddress receiverMail = new MailAddress(ToEmail);
                MailAddress ccEmail = new MailAddress(Cc);

                MailMessage mail = new MailMessage();
                mail.From = senderMail;
                mail.To.Add(receiverMail);
                mail.CC.Add(ccEmail);

                mail.Priority = MailPriority.High;
                mail.Subject = Subject;
                mail.Body = Message;
                mail.IsBodyHtml = true;

                smtpClient.UseDefaultCredentials = true;
                smtpClient.Credentials = new NetworkCredential("6b4303c46cd6eb", "66adb48fe8f4d2");
           //     smtpClient.Credentials = new NetworkCredential(username, password);
                smtpClient.EnableSsl = true;
                smtpClient.Port = 2525;
                smtpClient.Host = "smtp.mailtrap.io";
            //    smtpClient.Host = host;
                smtpClient.Timeout = 0;
                smtpClient.Send(mail);
                smtpClient.Dispose();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }



        }


        //public ActionResult SendNotice(int id)
        //{


        //    var notice = db.Notice.Find(id);


        //    SmtpClient smtpClient = new SmtpClient();
        //    MailAddress senderMail = new MailAddress("RestaurantMIS@ChickenStation.com", "Restaurant MIS");
         

        //    MailMessage mail = new MailMessage();
        //    mail.From = senderMail;


        //    var customersList = db.Customers.Where(q=>q.Address=="Kathamndu" && q.Status==1 && q.Name.Contains("Shrestha") ).ToList();

        //    foreach(var cList in customersList)
        //    {
        //        MailAddress receiverMail = new MailAddress(cList.Email);
        //        mail.To.Add(receiverMail);
        //    }

        //    //var teachers = db.Teachers.Where()..ToList();



           
        //    //MailAddress ccEmail = new MailAddress(Cc);
           
        //    //mail.CC.Add(ccEmail);




        //    mail.Priority = MailPriority.High;
        //    mail.Subject = notice.Subject;
        //    mail.Body = notice.Message;
        //    mail.IsBodyHtml = true;

        //    smtpClient.UseDefaultCredentials = true;
        //    smtpClient.Credentials = new NetworkCredential("6b4303c46cd6eb", "66adb48fe8f4d2");
        //    //     smtpClient.Credentials = new NetworkCredential(username, password);
        //    smtpClient.EnableSsl = true;
        //    smtpClient.Port = 2525;
        //    smtpClient.Host = "smtp.mailtrap.io";
        //    //    smtpClient.Host = host;
        //    smtpClient.Timeout = 0;
        //    smtpClient.Send(mail);
        //    smtpClient.Dispose();


        //    return View();
        //}









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
            Orders orders = db.Orders.Find(testid);

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
            ViewBag.ItemId = new SelectList(db.ITEMS, "Itemid", "Name", orders.ItemId);
            ViewBag.TableId = new SelectList(db.Tables, "Tableid", "TableName", orders.TableId);

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