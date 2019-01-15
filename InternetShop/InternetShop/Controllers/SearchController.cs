using BuisnesLogic;
using InternetShop.DependencyResolution;
using System.Web.Mvc;

namespace InternetShop.Controllers
{
    public class SearchController : Controller
    {
        IAccessing _accessing = IoC.Initialize().GetInstance<IAccessing>();
        // GET: Search
        public ActionResult Result()
        {
            return RedirectToAction("Main", "Main");
        }

        [HttpPost]
        public ActionResult Result(string search)
        {
            if (search != null)
            {
                return View(_accessing.FindAll(search));
            }
            return RedirectToAction("Main", "Main");
        }
    }
}