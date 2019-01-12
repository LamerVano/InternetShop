using BuisnesLogic;
using Common;
using InternetShop.DependencyResolution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InternetShop.Controllers
{
    public class ProfileController : Controller
    {
        string _role = Constants.COOKIE_ROLE;
        string _admin = Constants.ADMIN;
        string _userId = Constants.COOKIE_USER_ID;
        string _aprove = Constants.APROVE;
        string _deliver = Constants.DELIVER;
        string _paid = Constants.PAID;

        IAccessing _accessing = IoC.Initialize().GetInstance<IAccessing>();
        // GET: Profile
        public ActionResult User()
        {
            if (Request.Cookies[_userId] != null)
            {
                string cookie = Request.Cookies[_userId].Value;
                int userId;
                if (Int32.TryParse(cookie, out userId))
                {
                    if (userId > 0)
                        return View(_accessing.GetUser(userId));
                }
            }
            return RedirectToAction("Main", "Main");
        }

        public ActionResult Edit()
        {
            if (Request.Cookies[_userId] != null)
            {
                string cookie = Request.Cookies[_userId].Value;
                int userId;
                if (Int32.TryParse(cookie, out userId))
                {
                    if (userId > 0)
                        return View(_accessing.GetUser(userId));
                }
            }
            return RedirectToAction("Main", "Main");
        }
        [HttpPost]
        public ActionResult Edit(User user, string newPassword)
        {
            if(newPassword != null & newPassword != "")
            {
                user.Password = newPassword;
            }
            if (ModelState.IsValid)
            {
                if (_accessing.EditUser(user))
                {
                    return RedirectToAction("User");
                }
                else
                {
                    ViewBag.Message = "Chhanges not Save";
                }
            }
            else
            {
                ViewBag.Message = "Data not Valid";
            }
            return View(user);
        }
    }
}