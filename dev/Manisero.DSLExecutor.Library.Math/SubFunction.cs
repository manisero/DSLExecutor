using Manisero.DSLExecutor.Domain.FunctionsDomain;

namespace Manisero.DSLExecutor.Library.Math
{
    public class SubFunction : IFunction<int>
    {
        public int Minuend { get; set; }

        public int Subtrahend { get; set; }
    }

    public class SubFunctionHandler : IFunctionHandler<SubFunction, int>
    {
        public int Handle(SubFunction function)
        {
            return function.Minuend - function.Subtrahend;
        }
    }
}
