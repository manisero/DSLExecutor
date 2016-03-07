using System;

namespace Manisero.DSLExecutor.Domain.ExpressionsDomain
{
    public interface IExpression
    {
        Type ResultType { get; }
    }

    public abstract class Expression<TResult> : IExpression
    {
        public Type ResultType => typeof(TResult);
    }
}
