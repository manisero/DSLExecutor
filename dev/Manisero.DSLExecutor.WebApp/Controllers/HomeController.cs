﻿using Microsoft.AspNet.Mvc;

namespace Manisero.DSLExecutor.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
