namespace Manisero.DSLExecutor.Domain.FunctionsDomain
{
    public interface IFunctionHandler<TFunction, TResult>
        where TFunction : IFunction<TResult>
    {
        TResult Handle(TFunction function);
    }
}
