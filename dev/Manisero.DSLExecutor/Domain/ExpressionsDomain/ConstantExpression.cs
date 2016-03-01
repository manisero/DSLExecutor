using System;

namespace Manisero.DSLExecutor.Domain.ExpressionsDomain
{
    public interface IConstantExpression : IExpression
    {
        object Value { get; }
    }

    public class ConstantExpression<TValue> : IConstantExpression, IExpression<TValue>
    {
        public Type ResultType => typeof(TValue);

        public TValue Value { get; set; }

        object IConstantExpression.Value => Value;
    }
}
