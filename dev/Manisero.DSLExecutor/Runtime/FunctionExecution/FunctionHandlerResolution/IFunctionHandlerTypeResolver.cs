using System;

namespace Manisero.DSLExecutor.Runtime.FunctionExecution.FunctionHandlerResolution
{
    public interface IFunctionHandlerTypeResolver
    {
        Type Resolve(Type functionType);
    }
}
