using System;
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
        public IConstantExpression Generate(Literal literal)
        {
            throw new NotImplementedException();
        }
    }
}
