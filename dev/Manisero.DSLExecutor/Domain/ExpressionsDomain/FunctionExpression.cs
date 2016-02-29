using System;
using System.Collections.Generic;
using Manisero.DSLExecutor.Domain.FunctionsDomain;

namespace Manisero.DSLExecutor.Domain.ExpressionsDomain
{
    public class FunctionExpression<TFunction, TResult> : IExpression<TResult>
        where TFunction : IFunction<TResult>
    {
        public Type ResultType => typeof(TResult);

        public Type FunctionType => typeof(TFunction);

        public IDictionary<string, IExpression> ArgumentExpressions { get; set; }
    }
}
