using System;
using Manisero.DSLExecutor.Domain.ExpressionsDomain;

namespace Manisero.DSLExecutor.Runtime.SpecificExpressionExecutors
{
    public interface IFunctionExpressionExecutor
    {
        object Execute(IFunctionExpression expression);
    }

    public class FunctionExpressionExecutor : IFunctionExpressionExecutor
    {
        public object Execute(IFunctionExpression expression)
        {
            throw new NotImplementedException();
        }
    }
}
