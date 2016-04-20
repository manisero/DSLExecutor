using System.Collections.Generic;
using Manisero.DSLExecutor.Domain.ExpressionsDomain;
using Manisero.DSLExecutor.Parser.SampleDSL.Parsing.Tokens;

namespace Manisero.DSLExecutor.Parser.SampleDSL.ExpressionGeneration.FunctionExpressionGeneration
{
    public interface IFunctionArgumentExpressionsGenerator
    {
        IDictionary<string, IExpression> Generate(IList<IFunctionArgumentToken> functionArgumentTokens, FunctionMetadata functionMetadata);
    }

    public class FunctionArgumentExpressionsGenerator : IFunctionArgumentExpressionsGenerator
    {
        public IDictionary<string, IExpression> Generate(IList<IFunctionArgumentToken> functionArgumentTokens, FunctionMetadata functionMetadata)
        {
            var result = new Dictionary<string, IExpression>();

            for (var i = 0; i < functionArgumentTokens.Count; i++)
            {
                var token = functionArgumentTokens[i];
                var parameter = functionMetadata.Parameters[i];


            }

            return result;
        }
    }
}
