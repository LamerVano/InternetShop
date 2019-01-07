using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using BuisnesLogic;

namespace InternetShop.Controllers
{
    public class AuthorizeController : Controller
    {
        IAccessing accessing;
        // GET: Authorize
        public ActionResult LogIn()
        {
            if (Request.Cookies["UserId"] != null || Request.Cookies["Role"] != null)
                return View();
            else
                return RedirectToAction("Main", "Main");
        }

        [HttpPost]
        public ActionResult LogIn(User user)
        {
            int userId = accessing.LogIn(user.EMail, user.Password);
            if (userId != -1)
            {
                user = accessing.GetUser(userId);

                HttpCookie cookieRole = new HttpCookie("Role", user.Role);
                HttpCookie cookieUserId = new HttpCookie("UserId", user.UserId.ToString());

                Response.Cookies.Add(cookieRole);
                Response.Cookies.Add(cookieUserId);
            }
            else if(user.EMail == "Admin@gmail.com" & user.Password == "gfhjkm")
            {
                HttpCookie cookieRole = new HttpCookie("Role", "Admin";

                Response.Cookies.Add(cookieRole);
            }
            else
            {
                ViewBag.Message = "Введён не верный E-Mail или пароль";
                return View();
            }

            return RedirectToAction("Main", "Main");
        }
    }
}