using BuisnesLogic;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccesLayer;
using Common;

namespace InternetShop.Controllers
{
    public class MainController : Controller
    {
        private static IContainer _container = DependencyResolution.IoC.Initialize();
        private Accessing _accessing = (Accessing) _container.GetInstance<IAccessing>();
        // GET: Main
        public ActionResult Main()
        {
            return View(_accessing.GetAllCategories());
        }

        [HttpGet]
        public ActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCategory(Category category)
        {
            if (!_accessing.AddCategory(category.Name))
            {
                ViewBag.Message = "Не Добавлено";
            }
            return View();
        }
    }
}