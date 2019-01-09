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
    public class AdministrateController : Controller
    {
        string _role = Constants.COOKIE_ROLE;
        string _admin = Constants.ADMIN;

        IAccessing _accessing = IoC.Initialize().GetInstance<IAccessing>();

        // GET: Administrate
        public ActionResult AllUsers()
        {
            if(Request.Cookies[_role] != null)
            {
                if(Request.Cookies[_role].Value == _admin)
                {
                    return View(_accessing.GetAllUsers());
                }
            }
            return RedirectToAction("Main", "Main");
        }

        public ActionResult AddUser()
        {
            if (Request.Cookies[_role] != null)
            {
                if (Request.Cookies[_role].Value == _admin)
                {
                    return View();
                }
            }
            return RedirectToAction("Main", "Main");
        }
        [HttpPost]
        public ActionResult AddUser(User user)
        {
            if(ModelState.IsValid)
            {
                if(_accessing.AddUser(user))
                {
                    return RedirectToAction("AllUsers");
                }
                else
                {
                    ViewBag.Message = "User not Added";
                }
            }
            else
            {
                ViewBag.Message = "Data not Valid";
            }
            return View(user);
        }

        public ActionResult EditUser(string id)
        {
            if (Request.Cookies[_role] != null)
            {
                if (Request.Cookies[_role].Value == _admin)
                {
                    return View(_accessing.GetUser(Int32.Parse(id)));
                }
            }
            return RedirectToAction("Main", "Main");
        }
        [HttpPost]
        public ActionResult EditUser(User user)
        {
            if (ModelState.IsValid)
            {
                if (_accessing.EditUser(user))
                {
                    return RedirectToAction("AllUsers");
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


        public ActionResult DelUser(string id)
        {
            if (Request.Cookies[_role] != null)
            {
                if (Request.Cookies[_role].Value == _admin)
                {
                    return View(_accessing.GetUser(Int32.Parse(id)));
                }
            }
            return RedirectToAction("Main", "Main");
        }
        [HttpPost]
        public ActionResult DelUser(User user)
        {
            if (_accessing.DelUser(user))
            {
                return RedirectToAction("AllUsers");
            }
            else
            {
                ViewBag.Message = "User not Delet";
            }
            return View(user);
        }

        public ActionResult AboutUsers(string id)
        {
            return View(_accessing.GetUser(Int32.Parse(id)));
        }

        public ActionResult UserOrders(string id)
        {
            if (Request.Cookies[_role] != null)
            {
                if (Request.Cookies[_role].Value == _admin)
                {
                    return View(_accessing.GetUserOrders(Int32.Parse(id)));
                }
            }
            return RedirectToAction("Main", "Main");
        }
    }
    
}