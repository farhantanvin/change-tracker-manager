using GHIApplication.Models;
using System.Web.Mvc;

namespace GHIApplication.Controllers
{
    public class DashboardController : Controller
    {
        readonly GHIDBContext db = new GHIDBContext();

        public ActionResult Dashboard()
        {
            ViewBag.Title = "GHITL - Dashboard";
            if (Session["userName"] == null)
            {
                return RedirectToAction("Logout", "Home");
            }

            return View();
        }

    }
}
