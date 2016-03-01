using System;
using Manisero.DSLExecutor.Domain.ExpressionsDomain;

namespace Manisero.DSLExecutor.Runtime.SpecificExpressionExecutors
{
    public class BatchExpressionExecutor
    {
        private readonly Lazy<IExpressionExecutor> _expressionExecutorFactory;

        public BatchExpressionExecutor(Lazy<IExpressionExecutor> expressionExecutorFactory)
        {
            _expressionExecutorFactory = expressionExecutorFactory;
        }

        public object Execute(IBatchExpression expression)
        {
            foreach (var sideExpression in expression.SideExpressions)
            {
                _expressionExecutorFactory.Value.Execute(sideExpression);
            }

            return _expressionExecutorFactory.Value.Execute(expression.ResultExpression);
        }
    }
}
