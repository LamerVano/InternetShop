using BuisnesLogic;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccesLayer;
using Common;
using InternetShop.DependencyResolution;

namespace InternetShop.Controllers
{
    public class MainController : Controller
    {
        string _role = Constants.COOKIE_ROLE;
        string _moder = Constants.MODER;
        string _admin = Constants.ADMIN;
        
        IAccessing _accessing = IoC.Initialize().GetInstance<IAccessing>();
        // GET: Main
        public ActionResult Main()
        {
            return View(_accessing.GetAllCategories());
        }
        public ActionResult Categories()
        {
            return View(_accessing.GetAllCategories());
        }

        [HttpGet]
        public ActionResult CreateCategory()
        {
            if (Request.Cookies[_role] != null)
            {
                if (Request.Cookies[_role].Value == _admin || Request.Cookies[_role].Value == _moder)
                {

                    return View();
                }
            }
            return Redirect(Request.UrlReferrer.AbsolutePath);
        }

        [HttpPost]
        public ActionResult CreateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                if (_accessing.AddCategory(category.Name))
                {
                    return View();
                }
            }

            ViewBag.Message = "Не добавлено";
            return View();
        }

        public ActionResult Edit(string id)
        {
            if(Request.Cookies[_role] != null)
            {
                if(Request.Cookies[_role].Value == _admin || Request.Cookies[_role].Value == _moder)
                {
                    return View(_accessing.GetCategory(Int32.Parse(id)));
                }
            }
            return Redirect(Request.UrlReferrer.AbsolutePath);
        }

        [HttpPost]
        public ActionResult Edit(Category category, string Name)
        {
            if (ModelState.IsValid)
            {
                if (_accessing.EditCategory(category))
                {
                    ViewBag.Message = "Succes Saving";
                    return View(category);
                }
                else
                {
                    ViewBag.Message = "Unsucces Saving";
                }
            }
            else
            {
                ViewBag.Message = "Data not Valid";
            }
            return View(category);
        }

        public ActionResult Delete(string id)
        {
            if (Request.Cookies[_role] != null)
            {
                if (Request.Cookies[_role].Value == _admin || Request.Cookies[_role].Value == _moder)
                {
                    return View(_accessing.GetCategory(Int32.Parse(id)));
                }
            }
            return Redirect(Request.UrlReferrer.AbsolutePath);
        }

        [HttpPost]
        public ActionResult Delete(Category category)
        {
            if (_accessing.DelCategory(category))
            {
                return RedirectToAction("Main");
            }

            ViewBag.Message = "Product wasn't delete";

            return View(category);
        }
    }
}