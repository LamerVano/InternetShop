using BuisnesLogic;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccesLayer;

namespace InternetShop.Controllers
{
    public class MainController : Controller
    {
        private static IContainer _container = DependencyResolution.IoC.Initialize();
        private Accessing _accessing = (Accessing) _container.GetInstance<IAccessing>();
        // GET: Main
        public ActionResult Main()
        {
            return View(_accessing.GetCategories());
        }
        
        public PartialViewResult Categories()
        {
            return PartialView(_accessing.GetCategories());
        }
    }
}