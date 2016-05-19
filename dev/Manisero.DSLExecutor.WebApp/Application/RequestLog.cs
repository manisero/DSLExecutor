using System.Collections.Generic;

namespace Manisero.DSLExecutor.WebApp.Application
{
    public static class RequestLog
    {
        private static readonly IList<string> _logs = new List<string>();

        public static void Log(string log)
        {
            _logs.Add(log);
        }

        public static ICollection<string> GetLog()
        {
            return _logs;
        }
    }
}
