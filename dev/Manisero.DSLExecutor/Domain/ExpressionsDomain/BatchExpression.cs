using System.Collections.Generic;

namespace Manisero.DSLExecutor.Domain.ExpressionsDomain
{
    public interface IBatchExpression : IExpression
    {
        IEnumerable<IExpression> SideExpressions { get; }

        IExpression ResultExpression { get; }
    }

    public class BatchExpression<TResult> : Expression<TResult>, IBatchExpression
    {
        public IEnumerable<IExpression> SideExpressions { get; set; }

        public Expression<TResult> ResultExpression { get; set; }

        IExpression IBatchExpression.ResultExpression => ResultExpression;
    }
}
