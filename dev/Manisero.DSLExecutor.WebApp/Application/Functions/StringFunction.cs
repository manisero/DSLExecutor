using Manisero.DSLExecutor.Domain.FunctionsDomain;

namespace Manisero.DSLExecutor.WebApp.Application.Functions
{
    public class StringFunction : IFunction<string>
    {
        public object Value { get; set; }
    }

    public class StringFunctionHandler : IFunctionHandler<StringFunction, string>
    {
        public string Handle(StringFunction function)
        {
            return function.Value?.ToString();
        }
    }
}
