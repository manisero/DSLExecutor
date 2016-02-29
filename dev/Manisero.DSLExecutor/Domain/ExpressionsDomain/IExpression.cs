using System;

namespace Manisero.DSLExecutor.Domain.ExpressionsDomain
{
    public interface IExpression
    {
        Type ResultType { get; }
    }

    public interface IExpression<TResult> : IExpression
    {
    }
}
