using Manisero.DSLExecutor.Domain.FunctionsDomain;

namespace Manisero.DSLExecutor.Runtime.ExpressionExecution.SpecificExpressionExecution.FunctionExpressionExecution
{
    public interface IFunctionHandlerResolver
    {
        IFunctionHandler<TFunction, TResult> Resolve<TFunction, TResult>()
            where TFunction : IFunction<TResult>;
    }
}
