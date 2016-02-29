using System;
using Manisero.DSLExecutor.Domain.ExpressionsDomain;

namespace Manisero.DSLExecutor
{
    public interface IDSLExecutor
    {
        TResult ExecuteExpression<TResult>(IExpression<TResult> expression);
    }

    public class DSLExecutor : IDSLExecutor
    {
        public TResult ExecuteExpression<TResult>(IExpression<TResult> expression)
        {
            throw new NotImplementedException();
        }
    }
}
