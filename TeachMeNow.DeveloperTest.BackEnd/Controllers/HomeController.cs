using System.Web.Mvc;

namespace TeachMeNow.DeveloperTest.BackEnd.Controllers {
    public class HomeController: Controller {
        public ActionResult Index() {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}