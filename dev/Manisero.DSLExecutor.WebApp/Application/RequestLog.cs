using System.Collections.Generic;
using Microsoft.AspNet.Http;

namespace Manisero.DSLExecutor.WebApp.Application
{
    public static class RequestLog
    {
        private const string LOG_KEY = "log";

        public static IHttpContextAccessor HttpContextAccessor { private get; set; }

        public static void Log(string log)
        {
            GetLog().Add(log);
        }

        public static ICollection<string> Get()
        {
            return GetLog();
        }

        private static IList<string> GetLog()
        {
            object result;

            if (!HttpContextAccessor.HttpContext.Items.TryGetValue(LOG_KEY, out result))
            {
                result = new List<string>();

                HttpContextAccessor.HttpContext.Items.Add(LOG_KEY, result);
            }

            return (IList<string>)result;
        }
    }
}
