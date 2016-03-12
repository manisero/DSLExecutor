using System;
using Manisero.DSLExecutor.Domain.ExpressionsDomain;
using Manisero.DSLExecutor.Runtime.ExpressionExecution.SpecificExpressionExecution;

namespace Manisero.DSLExecutor.Runtime.ExpressionExecution
{
    public interface IExpressionExecutor
    {
        object Execute(IExpression expression);
    }

    public class ExpressionExecutor : IExpressionExecutor
    {
        private readonly IConstantExpressionExecutor _constantExpressionExecutor;
        private readonly IFunctionExpressionExecutor _functionExpressionExecutor;
        private readonly IBatchExpressionExecutor _batchExpressionExecutor;

        public ExpressionExecutor(IConstantExpressionExecutor constantExpressionExecutor,
                                  IFunctionExpressionExecutor functionExpressionExecutor,
                                  IBatchExpressionExecutor batchExpressionExecutor)
        {
            _constantExpressionExecutor = constantExpressionExecutor;
            _functionExpressionExecutor = functionExpressionExecutor;
            _batchExpressionExecutor = batchExpressionExecutor;
        }

        public object Execute(IExpression expression)
        {
            var constantExpression = expression as IConstantExpression;

            if (constantExpression != null)
            {
                return _constantExpressionExecutor.Execute(constantExpression);
            }

            var functionExpression = expression as IFunctionExpression;

            if (functionExpression != null)
            {
                return _functionExpressionExecutor.Execute(functionExpression);
            }

            var batchExpression = expression as IBatchExpression;

            if (batchExpression != null)
            {
                return _batchExpressionExecutor.Execute(batchExpression);
            }

            throw new NotSupportedException($"Not supported expression type: {expression.GetType().FullName}.");
        }
    }
}
