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
        private static readonly Logger.Logger log = new Logger.Logger(typeof(MainController));
        private static IContainer _container = DependencyResolution.IoC.Initialize();
        private Accessing _accessing = (Accessing)_container.GetInstance<IAccessing>();
        // GET: Products
        [HttpGet]
        public ActionResult Main(string id)
        {
            log.Debug("Get-req return View.Main");
            return View(_accessing.GetProducts(Int32.Parse(id)));
        }

        [HttpGet]
        public ActionResult CreateProducts(string id)
        {
            log.Debug("Get-req return View.CreateProduct");
            return View(new Product() { CategoryId = Int32.Parse(id) });
        }

        [HttpPost]
        public ActionResult CreateProducts(Product product, string id)
        {
            log.Debug("Post-req");
            if (ModelState.IsValid)
            {
                log.Debug("Model Valid");
                if (_accessing.AddProducts(product))
                {
                    log.Debug("Product Add");
                    return View();
                }
                log.Debug("Product not Add");
            }
            log.Debug("Model not Valid");

            ViewBag.Message = "Не добавлено";
            log.Debug("return View.CreateProducts");
            return View();
        }
    }
}