using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuisnesLogic;

namespace InternetShop.Controllers
{
    public class BusketController : Controller
    {
        IAccessing accessing;
        // GET: Busket
        public ActionResult Index()
        {
            if(Request.Cookies["UserId"] != null)
            {
                int userId = Int32.Parse(Request.Cookies["UserId"].Value);

                return View(accessing.GetBusket(userId));
            }
            return View();
        }
    }
}