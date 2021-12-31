using Microsoft.AspNetCore.Mvc;

namespace Web.Master.Controllers.Controllers
{
    public class UsersController : Controller
    {
        public UsersController()
        {

        }

        public virtual IActionResult Index()
        {
            ViewBag.Title = "Página Master Usuarios";
            return View();
        }
    }
}
