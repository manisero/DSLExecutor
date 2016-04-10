using Manisero.DSLExecutor.Domain.FunctionsDomain;

namespace Manisero.DSLExecutor.Parser.Json.Tests.TestsDomain
{
    public class AddFunction : IFunction<int>
    {
        public int A { get; set; }
        public int B { get; set; }
    }

    public class SubFunction : IFunction<int>
    {
        public int A { get; set; }
        public int B { get; set; }
    }

    public class LogFunction : IFunction<Void>
    {
        public string Text { get; set; }
    }
}
