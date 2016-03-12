using System;
using System.Collections.Generic;

namespace Manisero.DSLExecutor.Runtime.FunctionExecution.FunctionHandlerResolution.FunctionHandlerTypeResolvers
{
    public class DictionarySourcedFunctionHandlerTypeResolver : IFunctionHandlerTypeResolver
    {
        private readonly IDictionary<Type, Type> _functionTypeToHandlerTypeMap;

        public DictionarySourcedFunctionHandlerTypeResolver(IDictionary<Type, Type> functionTypeToHandlerTypeMap)
        {
            _functionTypeToHandlerTypeMap = functionTypeToHandlerTypeMap;
        }

        public Type Resolve(Type functionType)
        {
            Type handlerType;

            if (!_functionTypeToHandlerTypeMap.TryGetValue(functionType, out handlerType))
            {
                return null;
            }

            return handlerType;
        }
    }
}
