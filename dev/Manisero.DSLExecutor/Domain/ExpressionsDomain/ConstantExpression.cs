using System;

namespace Manisero.DSLExecutor.Domain.ExpressionsDomain
{
    public class ConstantExpression<TValue> : IExpression<TValue>
    {
        public Type ResultType => typeof(TValue);

        public TValue Value { get; set; }
    }
}
