using Manisero.DSLExecutor.Domain.FunctionsDomain;

namespace Manisero.DSLExecutor.Library.Math
{
    public class AddFunction : IFunction<int>
    {
        public int Addend1 { get; set; }

        public int Addend2 { get; set; }
    }

    public class AddFunctionHandler : IFunctionHandler<AddFunction, int>
    {
        public int Handle(AddFunction function)
        {
            return function.Addend1 + function.Addend2;
        }
    }
}
