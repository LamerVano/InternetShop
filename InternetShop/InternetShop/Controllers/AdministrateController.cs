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
        string _aprove = Constants.APROVE;
        string _deliver = Constants.DELIVER;
        string _paid = Constants.PAID;

        IAccessing _accessing = IoC.Initialize().GetInstance<IAccessing>();

        // GET: Administrate
        public ActionResult AllUsers()
        {
            if (Request.Cookies[_role] != null)
            {
                if (Request.Cookies[_role].Value == _admin)
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
            if (ModelState.IsValid)
            {
                if (_accessing.AddUser(user))
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
            if (Request.Cookies[_role] != null & id != null & id != "")
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
            if (Request.Cookies[_role] != null & id != null & id != "")
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

        public ActionResult AboutUser(string id)
        {
            if (id != null & id != "")
            {
                return View(_accessing.GetUser(Int32.Parse(id)));
            }
            else
            {
                return RedirectToAction("Main", "Main");
            }
        }        

        public ActionResult EditOrder(string id)
        {
            if (Request.Cookies[_role] != null & id != null & id != "")
            {
                if (Request.Cookies[_role].Value == _admin)
                {
                    return View(_accessing.GetOrder(Int32.Parse(id)));
                }
            }
            return RedirectToAction("Main", "Main");

        }
        [HttpPost]
        public ActionResult EditOrder(Order order)
        {
            if (OrderIsValid(order))
            {
                if (_accessing.EditOrder(order))
                {
                    ViewBag.Message = "Change succes";
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
            order.Product = _accessing.GetProduct(order.ProductId);
            return View(order);
        }

        private bool OrderIsValid(Order order)
        {
            if(order.Count < 1 || order.OrderId < 1 || order.ProductId < 1)
            {
                return false;
            }
            else if(order.Status != _aprove & order.Status != _deliver & order.Status != _paid)
            {
                return false;
            }
            return true;
        }

        public ActionResult AboutProduct(string id)
        {
            if (id != null & id != "")
            {
                return View(_accessing.GetOrder(Int32.Parse(id)));
            }
            else
            {
                return RedirectToAction("Main", "Main");
            }
        }
       
        public ActionResult Orders(string id)
        {
            if (Request.Cookies[_role] != null & id != null & id != "")
            {
                if (Request.Cookies[_role].Value == _admin)
                {
                    return View(_accessing.GetUserOrders(Int32.Parse(id)));
                }
            }
            return View(_accessing.GetOrders());
        }
    }
    
}