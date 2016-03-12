using System;
using Manisero.DSLExecutor.Domain.FunctionsDomain;

namespace Manisero.DSLExecutor.Runtime.FunctionExecution.FunctionHandlerResolution.FunctionHandlerResolvers
{
    public class ActivatorUsingFunctionHandlerResolver : IFunctionHandlerResolver
    {
        public IFunctionHandler<TFunction, TResult> Resolve<THandler, TFunction, TResult>()
            where THandler : IFunctionHandler<TFunction, TResult>
            where TFunction : IFunction<TResult>
        {
            return Activator.CreateInstance<THandler>();
        }
    }
}
