using Manisero.DSLExecutor.Domain.FunctionsDomain;

namespace Manisero.DSLExecutor.Tests.TestsDomain
{
    public class FunctionWithParameters : IFunction<int>
    {
        public int Parameter1 { get; set; }

        public int Parameter2 { get; set; }
    }
}
