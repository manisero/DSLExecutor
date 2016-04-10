using System;
using Manisero.DSLExecutor.Domain.ExpressionsDomain;
using Manisero.DSLExecutor.Parser.SampleDSL.Parsing.Tokens;

namespace Manisero.DSLExecutor.Parser.SampleDSL.ExpressionGeneration
{
    public interface IExpressionGenerator
    {
        IExpression Generate(TokenTree tokenTree);
    }

    public class ExpressionGenerator
    {
        public IExpression Generate(TokenTree tokenTree)
        {
            throw new NotImplementedException();
        }
    }
}
