using BuisnesLogic;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccesLayer;
using Common;
using Logger;
using InternetShop.DependencyResolution;

namespace InternetShop.Controllers
{
    public class MainController : Controller
    {
        private Logger.Logger log = new Logger.Logger(typeof(MainController));
        private Accessing _accessing = (Accessing) IoC.Initialize().GetInstance<IAccessing>();
        // GET: Main
        public ActionResult Main()
        {
            log.Debug("Get-req return View.Main");
            return View(_accessing.GetAllCategories());
        }

        [HttpGet]
        public ActionResult CreateCategory()
        {
            log.Debug("Get-req return View.CreateCategory");
            return View();
        }

        [HttpPost]
        public ActionResult CreateCategory(Category category)
        {
            log.Debug("Post-req");
            if (ModelState.IsValid)
            {
                log.Debug("Model Valid");
                if (_accessing.AddCategory(category.Name))
                {
                    log.Debug("Category Add");
                    return View();
                }
                log.Debug("Category not Add");
            }
            log.Debug("Model not Valid");

            ViewBag.Message = "Не добавлено";
            log.Debug("return View.CreateCategory");
            return View();
        }
    }
}