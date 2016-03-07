namespace Manisero.DSLExecutor.Domain.ExpressionsDomain
{
    public interface IConstantExpression : IExpression
    {
        object Value { get; }
    }

    public class ConstantExpression<TValue> : Expression<TValue>, IConstantExpression
    {
        public TValue Value { get; set; }

        object IConstantExpression.Value => Value;
    }
}
