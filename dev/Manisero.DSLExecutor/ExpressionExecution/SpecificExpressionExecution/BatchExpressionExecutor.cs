using System;
using Manisero.DSLExecutor.Domain.ExpressionsDomain;

namespace Manisero.DSLExecutor.ExpressionExecution.SpecificExpressionExecution
{
    public interface IBatchExpressionExecutor
    {
        object Execute(IBatchExpression expression);
    }

    public class BatchExpressionExecutor : IBatchExpressionExecutor
    {
        private readonly Lazy<IExpressionExecutor> _expressionExecutorFactory;

        public BatchExpressionExecutor(Lazy<IExpressionExecutor> expressionExecutorFactory)
        {
            _expressionExecutorFactory = expressionExecutorFactory;
        }

        public object Execute(IBatchExpression expression)
        {
            if (expression.SideExpressions != null)
            {
                foreach (var sideExpression in expression.SideExpressions)
                {
                    _expressionExecutorFactory.Value.Execute(sideExpression);
                }
            }

            return _expressionExecutorFactory.Value.Execute(expression.ResultExpression);
        }
    }
}
