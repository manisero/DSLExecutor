using Manisero.DSLExecutor.Domain.FunctionsDomain;

namespace Manisero.DSLExecutor.Runtime.FunctionExecution.FunctionHandlerResolution
{
    public interface IFunctionHandlerResolver
    {
        IFunctionHandler<TFunction, TResult> Resolve<THandler, TFunction, TResult>()
            where THandler : IFunctionHandler<TFunction, TResult>
            where TFunction : IFunction<TResult>;
    }
}
