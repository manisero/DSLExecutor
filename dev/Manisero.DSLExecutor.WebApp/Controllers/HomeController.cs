using Manisero.DSLExecutor.WebApp.Application;
using Microsoft.AspNet.Mvc;

namespace Manisero.DSLExecutor.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProcessDSL(DSLProcessorInput request)
        {
            var output = new DSLProcessor().Process(request);

            return Json(output);
        }
    }
}
