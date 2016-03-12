using System.Collections.Generic;
using System.Threading;

namespace Manisero.DSLExecutor.Tests.DSLExecutorTests
{
    public static class LogStore
    {
        private static IDictionary<int, IList<string>> _threadLogs = new Dictionary<int, IList<string>>();

        public static IList<string> GetLog()
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;
            IList<string> log;
            
            if (!_threadLogs.TryGetValue(threadId, out log))
            {
                log = new List<string>();
                _threadLogs.Add(threadId, log);
            }

            return log;
        }

        public static void Log(string log)
        {
            GetLog().Add(log);
        }
    }
}
