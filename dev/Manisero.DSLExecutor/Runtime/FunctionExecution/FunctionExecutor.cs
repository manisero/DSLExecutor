using System;
using Manisero.DSLExecutor.Domain.FunctionsDomain;
using Manisero.DSLExecutor.Runtime.FunctionExecution.FunctionHandlerResolution;

namespace Manisero.DSLExecutor.Runtime.FunctionExecution
{
    public interface IFunctionExecutor
    {
        object Execute(IFunction function);
    }

    public class FunctionExecutor : IFunctionExecutor
    {
        private readonly IFunctionHandlerTypeResolver _functionHandlerTypeResolver;
        private readonly IFunctionHandlerResolver _functionHandlerResolver;

        public FunctionExecutor(IFunctionHandlerTypeResolver functionHandlerTypeResolver,
                                IFunctionHandlerResolver functionHandlerResolver)
        {
            _functionHandlerTypeResolver = functionHandlerTypeResolver;
            _functionHandlerResolver = functionHandlerResolver;
        }

        public object Execute(IFunction function)
        {
            var handlerType = _functionHandlerTypeResolver.Resolve(function.GetType());

            if (handlerType == null)
            {
                throw new NotSupportedException($"Could not resolve {nameof(IFunctionExecutor)} for '{function.GetType()}' function.");
            }

            throw new NotImplementedException();
        }

        public TResult ExecuteGeneric<THandler, TFunction, TResult>(TFunction function)
            where THandler : IFunctionHandler<TFunction, TResult>
            where TFunction : IFunction<TResult>
        {
            var handler = _functionHandlerResolver.Resolve<THandler, TFunction, TResult>();

            return handler.Handle(function);
        }
    }
}
