using System;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                // Set ViewBag values
                ViewBag.Message = "Successfully conected to database";
                ViewBag.MyName = "MVC";
            }
            catch (Exception ex)
            {

                ViewBag.Message = $"Error in connecting to database: {ex.Message}";
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}