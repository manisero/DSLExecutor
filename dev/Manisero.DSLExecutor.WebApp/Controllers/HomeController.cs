using System;
using System.Collections.Generic;
using Manisero.DSLExecutor.Domain.ExpressionsDomain;
using Microsoft.AspNet.Mvc;

namespace Manisero.DSLExecutor.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            var expressionResult = new DSLExecutor(new Dictionary<Type, Type>()).ExecuteExpression(new ConstantExpression<string> { Value = "test" });

            ViewData["Message"] = expressionResult; // "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
