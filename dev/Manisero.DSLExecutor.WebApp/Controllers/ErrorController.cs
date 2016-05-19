using Microsoft.AspNet.Mvc;

namespace Manisero.DSLExecutor.WebApp.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
