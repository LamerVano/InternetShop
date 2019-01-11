using BuisnesLogic;
using Common;
using InternetShop.DependencyResolution;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InternetShop.Controllers
{
    public class ProductsController : Controller
    {
        string _role = Constants.COOKIE_ROLE;
        string _moder = Constants.MODER;
        string _admin = Constants.ADMIN;
        string _userId = Constants.COOKIE_USER_ID;


        static readonly Logger.Logger log = new Logger.Logger(typeof(MainController));
        IAccessing _accessing = IoC.Initialize().GetInstance<IAccessing>();
        // GET: Products
        [HttpGet]
        public ActionResult Main(string id)
        {
            if(id != null & id != "")
            {
                return View(_accessing.GetProducts(Int32.Parse(id)));
            }
            else
            {
                return RedirectToAction("Main", "Main");
            }
        }
        [HttpPost]
        public ActionResult Main(string id, string count)
        {
            Product product = _accessing.GetProduct(Int32.Parse(id));
            if (Request.Cookies[_role] != null & Request.Cookies[_userId] != null)
            {
                if (Request.Cookies[_role].Value != "" & Request.Cookies[_userId].Value != "")
                {
                    if (_accessing.AddOrder(Int32.Parse(Request.Cookies[_userId].Value), product.ProductId, Int32.Parse(count)))
                    {
                        ViewBag.Message = "Added";
                    }
                    else
                    {
                        ViewBag.Message = "Sorry We got touble and Product not Add";
                    }
                }
            }
            return RedirectToAction("Main");
        }

        [HttpGet]
        public ActionResult CreateProducts(string id)
        {
            if (Request.Cookies[_role] != null)
            {
                if (Request.Cookies[_role].Value == _admin || Request.Cookies[_role].Value == _moder)
                {
                    return View(new Product() { CategoryId = Int32.Parse(id) });
                }
            }
            if (Request.UrlReferrer != null || id != null & id != "")
            {
                return Redirect(Request.UrlReferrer.AbsolutePath);
            }
            else
                return RedirectToAction("Main", "Main");
        }

        [HttpPost]
        public ActionResult CreateProducts(Product product, string id)
        {
            if (ModelState.IsValid)
            {
                if (_accessing.AddProducts(product))
                {
                    return View();
                }
            }

            ViewBag.Message = "Не добавлено";
            return View();
        }

        public ActionResult EditProduct(string id)
        {
            if (Request.Cookies[_role] != null)
                if (Request.Cookies[_role].Value == _moder || Request.Cookies[_role].Value == _admin)
                {
                    return View(_accessing.GetProduct(Int32.Parse(id)));
                }
            if (Request.UrlReferrer != null || id != null & id != "")
            {
                return Redirect(Request.UrlReferrer.AbsolutePath);
            }
            else
                return RedirectToAction("Main", "Main");
        }

        [HttpPost]
        public ActionResult EditProduct(Product product, string Name)
        {
            if (ModelState.IsValid)
            {
                if(_accessing.EditProducts(product))
                {
                    return RedirectToAction("Main", new { id = product.CategoryId });
                }
                ViewBag.Message = "New State was't Save";
            }
            else
            {
                ViewBag.Message = "Not Valid data";
            }
            return View(product);
        }
        public ActionResult DelProduct(string id)
        {
            if (Request.Cookies[_role] != null)
                if (Request.Cookies[_role].Value == _moder || Request.Cookies[_role].Value == _admin)
                {

                    return View(_accessing.GetProduct(Int32.Parse(id)));
                }
            if (Request.UrlReferrer != null || id != null & id != "")
            {
                return Redirect(Request.UrlReferrer.AbsolutePath);
            }
            else
                return RedirectToAction("Main", "Main");
        }

        [HttpPost]
        public ActionResult DelProduct(Product product, string Name)
        {
            if (_accessing.DelProducts(product))
            {
                return RedirectToAction("Main", new { id = product.CategoryId });
            }
            ViewBag.Message = "Product wasn't delete";
            return View(product);
        }

        public ActionResult AboutProduct(string id)
        {
            if (id != null & id != "")
            {
                return View(_accessing.GetProduct(Int32.Parse(id)));
            }
            else
            {
                return RedirectToAction("Main", "Main");
            }
        }
    }
}