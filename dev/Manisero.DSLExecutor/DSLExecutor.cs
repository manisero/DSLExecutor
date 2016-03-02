using System;
using Manisero.DSLExecutor.Domain.ExpressionsDomain;
using Manisero.DSLExecutor.Runtime.ExpressionExecution;

namespace Manisero.DSLExecutor
{
    public interface IDSLExecutor
    {
        object ExecuteExpression(IExpression expression);
    }

    public class DSLExecutor : IDSLExecutor
    {
        private readonly Lazy<IExpressionExecutor> _expressionExecutor = new Lazy<IExpressionExecutor>(InitializeExpressionExecutor);

        public object ExecuteExpression(IExpression expression)
        {
            return _expressionExecutor.Value.Execute(expression);
        }

        private static IExpressionExecutor InitializeExpressionExecutor()
        {
            return new ExpressionExecutorFactory().Create();
        }
    }
}
