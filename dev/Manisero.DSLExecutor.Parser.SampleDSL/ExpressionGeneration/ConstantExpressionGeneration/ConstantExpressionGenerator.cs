using System;
using System.Reflection;
using Manisero.DSLExecutor.Domain.ExpressionsDomain;
using Manisero.DSLExecutor.Parser.SampleDSL.Parsing.Tokens;

namespace Manisero.DSLExecutor.Parser.SampleDSL.ExpressionGeneration.ConstantExpressionGeneration
{
    public interface IConstantExpressionGenerator
    {
        IConstantExpression Generate(Literal literal);
    }

    public class ConstantExpressionGenerator : IConstantExpressionGenerator
    {
        private readonly Lazy<MethodInfo> _createConstantExpressionMethod;

        public ConstantExpressionGenerator()
        {
            _createConstantExpressionMethod = new Lazy<MethodInfo>(() => GetType().GetMethod(nameof(CreateConstantExpression),
                                                                                             BindingFlags.Instance | BindingFlags.NonPublic));
        }

        public IConstantExpression Generate(Literal literal)
        {
            try
            {
                var tResult = literal.Value != null
                                  ? literal.Value.GetType()
                                  : typeof(object);

                return (IConstantExpression)_createConstantExpressionMethod.Value
                                                                           .MakeGenericMethod(tResult)
                                                                           .Invoke(this,
                                                                                   new object[] { literal.Value });
            }
            catch (TargetInvocationException exception)
            {
                throw exception.InnerException;
            }
        }

        private ConstantExpression<TResult> CreateConstantExpression<TResult>(TResult value)
        {
            return new ConstantExpression<TResult>
                {
                    Value = value
                };
        }
    }
}
