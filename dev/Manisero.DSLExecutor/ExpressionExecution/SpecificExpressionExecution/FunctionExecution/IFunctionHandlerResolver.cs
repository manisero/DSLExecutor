using Manisero.DSLExecutor.Domain.FunctionsDomain;

namespace Manisero.DSLExecutor.ExpressionExecution.SpecificExpressionExecution.FunctionExecution
{
    public interface IFunctionHandlerResolver
    {
        IFunctionHandler<TFunction, TResult> Resolve<TFunction, TResult>()
            where TFunction : IFunction<TResult>;
    }
}
