using System;
using System.Collections.Generic;

namespace Manisero.DSLExecutor.Domain.ExpressionsDomain
{
    public class BatchExpression<TResult> : IExpression<TResult>
    {
        public Type ResultType => typeof(TResult);

        public ICollection<IExpression> SideExpressions { get; set; }

        public IExpression<TResult> ResultExpression { get; set; }
    }
}
