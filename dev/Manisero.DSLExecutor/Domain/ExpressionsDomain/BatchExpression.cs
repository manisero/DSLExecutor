using System;
using System.Collections.Generic;

namespace Manisero.DSLExecutor.Domain.ExpressionsDomain
{
    public interface IBatchExpression : IExpression
    {
        IEnumerable<IExpression> SideExpressions { get; }

        IExpression ResultExpression { get; }
    }

    public class BatchExpression<TResult> : IBatchExpression, IExpression<TResult>
    {
        public Type ResultType => typeof(TResult);

        public IEnumerable<IExpression> SideExpressions { get; set; }

        public IExpression<TResult> ResultExpression { get; set; }

        IExpression IBatchExpression.ResultExpression => ResultExpression;
    }
}
