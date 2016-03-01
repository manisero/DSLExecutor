namespace Manisero.DSLExecutor.Runtime
{
    public interface IExpressionExecutorFactory
    {
        IExpressionExecutor Create();
    }

    public class ExpressionExecutorFactory : IExpressionExecutorFactory
    {
        public IExpressionExecutor Create()
        {
            return new ExpressionExecutor(new ConstantExpressionExecutor());
        }
    }
}
