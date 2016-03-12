using Manisero.DSLExecutor.Domain.FunctionsDomain;

namespace Manisero.DSLExecutor.Tests.DSLExecutorTests
{
    // Add

    public class AddFunction : IFunction<int>
    {
        public int A { get; set; }
        public int B { get; set; }
    }

    public class AddFunctionHandler : IFunctionHandler<AddFunction, int>
    {
        public int Handle(AddFunction function)
        {
            return function.A + function.B;
        }
    }

    // Substract

    public class SubstractFunction : IFunction<int>
    {
        public int A { get; set; }
        public int B { get; set; }
    }

    public class SubstractFunctionHandler : IFunctionHandler<SubstractFunction, int>
    {
        public int Handle(SubstractFunction function)
        {
            return function.A - function.B;
        }
    }

    // AddAndStore

    public class LogFunction : IFunction<Void>
    {
        public string Log { get; set; }
    }

    public class LogFunctionHandler : IFunctionHandler<LogFunction, Void>
    {
        public Void Handle(LogFunction function)
        {
            LogStore.Log(function.Log);

            return Void.Value;
        }
    }
}
