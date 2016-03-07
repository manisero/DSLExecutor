using System;
using System.Collections.Generic;
using Manisero.DSLExecutor.Domain.FunctionsDomain;

namespace Manisero.DSLExecutor.Domain.ExpressionsDomain
{
    public interface IFunctionExpression : IExpression
    {
        Type FunctionType { get; }

        IDictionary<string, IExpression> ArgumentExpressions { get; }
    }

    public class FunctionExpression<TFunction, TResult> : Expression<TResult>, IFunctionExpression
        where TFunction : IFunction<TResult>
    {
        public Type FunctionType => typeof(TFunction);

        public IDictionary<string, IExpression> ArgumentExpressions { get; set; }
    }
}
