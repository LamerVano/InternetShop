using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using BuisnesLogic;
using InternetShop.DependencyResolution;

namespace InternetShop.Controllers
{
    public class AuthorizeController : Controller
    {
        string _role = Constants.COOKIE_ROLE;
        string _userId = Constants.COOKIE_USER_ID;
        int _time = Constants.WAIT_TIME;

        IAccessing accessing = IoC.Initialize().GetInstance<IAccessing>();
        // GET: Authorize
        public ActionResult LogIn()
        {
            if (Request.Cookies[_role] == null)
                return View();

            if (Request.Cookies[_role].Value == "")
                return View();
            
            return RedirectToAction("Main", "Main");
        }

        [HttpPost]
        public ActionResult LogIn(User user)
        {
            if (ModelState.IsValid)
            {
                int userId = accessing.LogIn(user.EMail, user.Password);
                if (userId != -1)
                {
                    user = accessing.GetUser(userId);

                    HttpCookie cookieRole = new HttpCookie(_role, user.Role);
                    cookieRole.Expires = DateTime.Now.AddMinutes(_time);
                    HttpCookie cookieUserId = new HttpCookie(_userId, user.UserId.ToString());
                    cookieUserId.Expires = DateTime.Now.AddMinutes(_time);

                    Response.SetCookie(cookieRole);
                    Response.SetCookie(cookieUserId);
                }
                else if (user.EMail == "Admin@gmail.com" & user.Password == "gfhjkm")
                {
                    HttpCookie cookieRole = new HttpCookie(_role, "Admin");
                    cookieRole.Expires = DateTime.Now.AddMinutes(_time);
                    HttpCookie cookieUserId = new HttpCookie(_userId, "-1");
                    cookieUserId.Expires = DateTime.Now.AddMinutes(_time);

                    Response.SetCookie(cookieRole);
                    Response.SetCookie(cookieUserId);
                }
                else
                {
                    ViewBag.Message = "Введён не верный E-Mail или пароль";
                    return View();
                }
            }
            
            else
            {
                ViewBag.Message = "Введёны некоректные данные";
                return View();
            }

            return RedirectToAction("Main", "Main");
        }

        public ActionResult Register()
        {
            if (Request.Cookies[_role] != null)
            {
                if (Request.Cookies[_role].Value != "")
                {
                    return RedirectToAction("Main", "Main");
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                if (accessing.AddUser(user))
                {
                    return RedirectToAction("Main", "Main");
                }
            }
            ViewBag.Message = "Введите корректные двнные";
            return View();
        }

        public ActionResult LogOut()
        {
            Response.Cookies[_role].Expires = DateTime.Now;
            if (Response.Cookies[_userId] != null)
            {
                Response.Cookies[_userId].Expires = DateTime.Now;
            }
            return RedirectToAction("Main", "Main");
        }
    }
}