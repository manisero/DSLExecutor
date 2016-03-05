﻿using Manisero.DSLExecutor.Domain.ExpressionsDomain;

namespace Manisero.DSLExecutor.Runtime.ExpressionExecution.SpecificExpressionExecutors
{
    public interface IConstantExpressionExecutor
    {
        object Execute(IConstantExpression expression);
    }

    public class ConstantExpressionExecutor : IConstantExpressionExecutor
    {
        public object Execute(IConstantExpression expression)
        {
            return expression.Value;
        }
    }
}