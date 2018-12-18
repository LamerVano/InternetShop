using BuisnesLogic;
using Common;
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
        private static IContainer _container = DependencyResolution.IoC.Initialize();
        private Accessing _accessing = (Accessing)_container.GetInstance<IAccessing>();
        // GET: Products
        [HttpGet]
        public ActionResult Main(string id)
        {
            return View(_accessing.GetProducts(Int32.Parse(id)));
        }

        [HttpGet]
        public ActionResult CreateProducts(string id)
        {
            return View(new Product() { CategoryId = Int32.Parse(id) });
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
    }
}