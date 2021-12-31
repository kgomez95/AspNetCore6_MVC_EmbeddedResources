using Microsoft.AspNetCore.Mvc;
using Web.Master.Controllers.Controllers;

namespace Web.Controllers
{
    [Route("Projects")]
    public class CustomProjectsController : ProjectsController
    {
        public CustomProjectsController()
        {

        }

        public override IActionResult Index()
        {
            ViewBag.Title = "Página Customizada Proyectos";
            return View();
        }
    }
}