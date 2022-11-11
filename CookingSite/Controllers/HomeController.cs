using System.Web.Mvc;

namespace CookingSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Redirect("/Category/Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "A site for culinary recipes, divided by category and with comments.";
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}