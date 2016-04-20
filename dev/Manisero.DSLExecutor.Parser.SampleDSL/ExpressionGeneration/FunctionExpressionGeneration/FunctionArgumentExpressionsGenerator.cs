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
        private readonly Lazy<IExpressionGenerator> _expressionGeneratorFactory;

        public FunctionArgumentExpressionsGenerator(Lazy<IExpressionGenerator> expressionGeneratorFactory)
        {
            _expressionGeneratorFactory = expressionGeneratorFactory;
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

                var argumentExpression = _expressionGeneratorFactory.Value.Generate(token);

                if (!parameter.Type.IsAssignableFrom(argumentExpression.ResultType))
                {
                    throw new InvalidOperationException($"Result Type of Argument Token for '{parameter.Name}' parameter is invalid. Expected: '{parameter.Type}' or its child. Actual: '{argumentExpression.ResultType}'.");
                }

                result.Add(parameter.Name, argumentExpression);
            }

            return result;
        }
    }
}
