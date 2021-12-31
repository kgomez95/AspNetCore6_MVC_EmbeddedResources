using Microsoft.AspNetCore.Mvc;

namespace Web.Master.Controllers.Controllers
{
    public class ReportsController : Controller
    {
        public ReportsController()
        {

        }

        public virtual IActionResult Index()
        {
            ViewBag.Title = "Página Master Reportes";
            return View();
        }
    }
}
