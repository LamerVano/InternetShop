using System;
using System.Web.Mvc;
using BuisnesLogic;
using Common;
using InternetShop.DependencyResolution;

namespace InternetShop.Controllers
{
    public class OrderController : Controller
    {
        string _role = Constants.COOKIE_ROLE;
        string _userId = Constants.COOKIE_USER_ID;

        IAccessing _accessing = IoC.Initialize().GetInstance<IAccessing>();
        // GET: Busket
        public ActionResult Order()
        {
            if(Request.Cookies[_userId] != null)
            {
                string cookie = Request.Cookies[_userId].Value;
                int userId;
                if (Int32.TryParse(cookie, out userId))
                {
                    if (userId > 0)
                        return View(_accessing.GetUser(userId));
                }
            }
            return View();
        }

        public ActionResult AddOrder(string id, int count)
        {
            Product product = _accessing.GetProduct(Int32.Parse(id));
            if (Request.Cookies[_role] != null & Request.Cookies[_userId] != null & id != null & id != "")
            {
                if(Request.Cookies[_role].Value != "" & Request.Cookies[_userId].Value != "")
                {
                    if (_accessing.AddOrder(Int32.Parse(Request.Cookies[_userId].Value), product.ProductId, count))
                    {
                        ViewBag.Message = "Added";
                    }
                    else
                    {
                        ViewBag.Message = "Sorry We got touble and Product not Add";
                    }
                }
            }
            
            return Redirect(Request.UrlReferrer.AbsolutePath);
        }
        public ActionResult AboutProduct(string id)
        {
            if (id != null & id != "")
            {
                return View(_accessing.GetProduct(Int32.Parse(id)));
            }
            return RedirectToAction("Main", "Main");
        }
    }
}