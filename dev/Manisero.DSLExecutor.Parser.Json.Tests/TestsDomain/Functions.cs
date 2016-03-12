using Manisero.DSLExecutor.Domain.FunctionsDomain;

namespace Manisero.DSLExecutor.Parser.Json.Tests.TestsDomain
{
    public class AddFunction : IFunction<int>
    {
        public int A { get; set; }
        public int B { get; set; }
    }

    public class SubstractFunction : IFunction<int>
    {
        public int A { get; set; }
        public int B { get; set; }
    }
}
