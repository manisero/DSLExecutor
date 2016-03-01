using System;
using Manisero.DSLExecutor.Domain.FunctionsDomain;

namespace Manisero.DSLExecutor.Runtime.FunctionExecution
{
    public interface IFunctionExecutor
    {
        object Execute(IFunction function);
    }

    public class FunctionExecutor : IFunctionExecutor
    {
        public object Execute(IFunction function)
        {
            throw new NotImplementedException();
        }
    }
}
