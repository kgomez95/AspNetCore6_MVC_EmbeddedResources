using Microsoft.AspNetCore.Mvc;

namespace Web.Master.Controllers.Controllers
{
    public class ProjectsController : Controller
    {
        public ProjectsController()
        {

        }

        public virtual IActionResult Index()
        {
            ViewBag.Title = "Página Master Proyectos";
            return View();
        }
    }
}
