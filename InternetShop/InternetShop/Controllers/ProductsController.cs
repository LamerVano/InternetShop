﻿using BuisnesLogic;
using Common;
using InternetShop.DependencyResolution;
using System;
using System.Web.Mvc;
using System.Web;

namespace InternetShop.Controllers
{
    public class ProductsController : Controller
    {
        string _role = Constants.COOKIE_ROLE;
        string _moder = Constants.MODER;
        string _admin = Constants.ADMIN;
        string _userId = Constants.COOKIE_USER_ID;
        
        IAccessing _accessing = IoC.Initialize().GetInstance<IAccessing>();
        // GET: Products
        [HttpGet]
        public ActionResult Main(string id)
        {
            int categoryId;

            if(Int32.TryParse(id, out categoryId))
            {
                return View(_accessing.GetProducts(categoryId));
            }
            else
            {
                return RedirectToAction("Main", "Main");
            }
        }
        [HttpPost]
        public ActionResult Main(string id, string count)
        {
            int productCount, productId;

            if (Int32.TryParse(id, out productId) & Int32.TryParse(count, out productCount))
            {
                Product product = _accessing.GetProduct(productId);

                if (Request.Cookies[_role] != null & Request.Cookies[_userId] != null)
                {
                    if (Request.Cookies[_role].Value != "" & Request.Cookies[_userId].Value != "")
                    {
                        if (_accessing.AddOrder(Int32.Parse(Request.Cookies[_userId].Value), productId, productCount))
                        {
                            ViewBag.Message = "Added";
                        }
                        else
                        {
                            ViewBag.Message = "Sorry We got touble and Product not Add";
                        }
                    }
                }
            }
            else
            {
                ViewBag.Message = "Not valid data";
            }
            
            return RedirectToAction("Main", new { id = _accessing.GetCategoryId(productId) });
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
            {
                return RedirectToAction("Main", "Main");
            }
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

            ViewBag.Message = "Not Added";
            return View();
        }

        public ActionResult EditProduct(string id)
        {
            if (Request.Cookies[_role] != null & id != null & id != "")
            {
                if (Request.Cookies[_role].Value == _moder || Request.Cookies[_role].Value == _admin)
                {
                    return View(_accessing.GetProduct(Int32.Parse(id)));
                }
            }
            if (Request.UrlReferrer != null)
            {
                return Redirect(Request.UrlReferrer.AbsolutePath);
            }
            else
            {
                return RedirectToAction("Main", "Main");
            }
        }

        [HttpPost]
        public ActionResult EditProduct(Product product, string Name, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    product.ImggType = "." + image.FileName.Split('.')[1];

                    string fileName = product.ProductId + "." + image.FileName.Split('.')[1];
                    string path = Server.MapPath("~/Content/Products/");

                    image.SaveAs(path + fileName);
                }
                if (_accessing.EditProducts(product))
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
            int productId;

            if (Int32.TryParse(id, out productId))
            {
                return View(_accessing.GetProduct(productId));
            }
            else
            {
                return RedirectToAction("Main", "Main");
            }
        }
    }
}