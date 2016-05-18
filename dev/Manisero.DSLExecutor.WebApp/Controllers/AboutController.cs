using Microsoft.AspNet.Mvc;

namespace Manisero.DSLExecutor.WebApp.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
