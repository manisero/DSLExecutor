using System;
using System.Collections.Generic;
using Manisero.DSLExecutor.Domain.FunctionsDomain;

namespace Manisero.DSLExecutor.Runtime.ExpressionExecution.SpecificExpressionExecution.FunctionExpressionExecution.FunctionHandlerResolvers
{
    public class DictionarySourcedActivatorUsingFunctionHandlerResolver : IFunctionHandlerResolver
    {
        private readonly IDictionary<Type, Type> _functionTypeToHandlerTypeMap;

        public DictionarySourcedActivatorUsingFunctionHandlerResolver(IDictionary<Type, Type> functionTypeToHandlerTypeMap)
        {
            _functionTypeToHandlerTypeMap = functionTypeToHandlerTypeMap;
        }

        public IFunctionHandler<TFunction, TResult> Resolve<TFunction, TResult>() where TFunction : IFunction<TResult>
        {
            Type handlerType;

            if (!_functionTypeToHandlerTypeMap.TryGetValue(typeof(TFunction), out handlerType))
            {
                return null;
            }

            return Activator.CreateInstance(handlerType) as IFunctionHandler<TFunction, TResult>;
        }
    }
}
