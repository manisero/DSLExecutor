using Manisero.DSLExecutor.Domain.FunctionsDomain;

namespace Manisero.DSLExecutor.WebApp.Application.Functions
{
    public class LogFunction : IFunction<Void>
    {
        public object Log { get; set; }
    }

    public class LogFunctionHandler : IFunctionHandler<LogFunction, Void>
    {
        public Void Handle(LogFunction function)
        {
            RequestLog.Log(function.Log?.ToString());

            return Void.Value;
        }
    }
}
