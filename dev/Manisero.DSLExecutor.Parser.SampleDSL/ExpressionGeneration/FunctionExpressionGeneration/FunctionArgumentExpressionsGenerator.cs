using System;
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
        private readonly IFunctionArgumentExpressionGenerator _functionArgumentExpressionGenerator;

        public FunctionArgumentExpressionsGenerator(IFunctionArgumentExpressionGenerator functionArgumentExpressionGenerator)
        {
            _functionArgumentExpressionGenerator = functionArgumentExpressionGenerator;
        }

        public IDictionary<string, IExpression> Generate(IList<IFunctionArgumentToken> functionArgumentTokens, FunctionMetadata functionMetadata)
        {
            if (functionArgumentTokens.Count != functionMetadata.Parameters.Count)
            {
                throw new InvalidOperationException("Argument Tokens number does not match the function's parameters number.");
            }

            var result = new Dictionary<string, IExpression>();

            for (var i = 0; i < functionArgumentTokens.Count; i++)
            {
                var token = functionArgumentTokens[i];
                var parameter = functionMetadata.Parameters[i];

                var argumentExpression = _functionArgumentExpressionGenerator.Generate(token, parameter);

                result.Add(parameter.Name, argumentExpression);
            }

            return result;
        }
    }
}
