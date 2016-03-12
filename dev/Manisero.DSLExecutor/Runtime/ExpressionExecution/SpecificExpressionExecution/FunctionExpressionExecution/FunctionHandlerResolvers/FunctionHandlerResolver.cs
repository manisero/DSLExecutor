using System;
using Manisero.DSLExecutor.Domain.FunctionsDomain;

namespace Manisero.DSLExecutor.Runtime.ExpressionExecution.SpecificExpressionExecution.FunctionExpressionExecution.FunctionHandlerResolvers
{
    public class FunctionHandlerResolver : IFunctionHandlerResolver
    {
        public IFunctionHandler<TFunction, TResult> Resolve<TFunction, TResult>() where TFunction : IFunction<TResult>
        {
            throw new NotImplementedException();
        }
    }
}
