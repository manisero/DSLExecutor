using Manisero.DSLExecutor.Domain.ExpressionsDomain;
using Manisero.DSLExecutor.Parser.SampleDSL.Parsing.Tokens;

namespace Manisero.DSLExecutor.Parser.SampleDSL.ExpressionGeneration
{
    public interface IFunctionExpressionGenerator
    {
        IFunctionExpression Generate(FunctionCall functionCall);
    }

    public class FunctionExpressionGenerator : IFunctionExpressionGenerator
    {
        public IFunctionExpression Generate(FunctionCall functionCall)
        {
            return null;
        }
    }
}
