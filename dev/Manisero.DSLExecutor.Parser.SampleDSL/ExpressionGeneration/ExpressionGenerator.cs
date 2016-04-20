using System;
using Manisero.DSLExecutor.Domain.ExpressionsDomain;
using Manisero.DSLExecutor.Parser.SampleDSL.Parsing.Tokens;

namespace Manisero.DSLExecutor.Parser.SampleDSL.ExpressionGeneration
{
    public interface IExpressionGenerator
    {
        IExpression Generate(IToken token);
    }

    public class ExpressionGenerator : IExpressionGenerator
    {
        public IExpression Generate(IToken token)
        {
            throw new NotImplementedException();
        }
    }
}
