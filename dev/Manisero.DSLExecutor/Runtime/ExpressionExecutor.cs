﻿using System;
using Manisero.DSLExecutor.Domain.ExpressionsDomain;

namespace Manisero.DSLExecutor.Runtime
{
    public interface IExpressionExecutor
    {
        object Execute(IExpression expression);
    }

    public class ExpressionExecutor : IExpressionExecutor
    {
        private readonly IConstantExpressionExecutor _constantExpressionExecutor;

        public ExpressionExecutor(IConstantExpressionExecutor constantExpressionExecutor)
        {
            _constantExpressionExecutor = constantExpressionExecutor;
        }

        public object Execute(IExpression expression)
        {
            var constantExpression = expression as IConstantExpression;

            if (constantExpression != null)
            {
                return _constantExpressionExecutor.Execute(constantExpression);
            }

            throw new NotSupportedException($"Not supported expression type: {expression.GetType().FullName}.");
        }
    }
}
