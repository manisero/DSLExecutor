using System;
using Manisero.DSLExecutor.Domain.ExpressionsDomain;
using Manisero.DSLExecutor.Parser.SampleDSL.Parsing.Tokens;
using Manisero.DSLExecutor.Utilities;

namespace Manisero.DSLExecutor.Parser.SampleDSL.ExpressionGeneration.FunctionExpressionGeneration
{
    public interface IFunctionArgumentExpressionGenerator
    {
        IExpression Generate(IFunctionArgumentToken functionArgumentToken, FunctionParameterMetadata functionParameterMetadata);
    }

    public class FunctionArgumentExpressionGenerator : IFunctionArgumentExpressionGenerator
    {
        private readonly Lazy<IExpressionGenerator> _expressionGeneratorFactory;

        public FunctionArgumentExpressionGenerator(Lazy<IExpressionGenerator> expressionGeneratorFactory)
        {
            _expressionGeneratorFactory = expressionGeneratorFactory;
        }

        public IExpression Generate(IFunctionArgumentToken functionArgumentToken, FunctionParameterMetadata functionParameterMetadata)
        {
            var argumentExpression = _expressionGeneratorFactory.Value.Generate(functionArgumentToken);

            if (!functionParameterMetadata.Type.IsAssignableFrom(argumentExpression.ResultType))
            {
                throw new InvalidOperationException($"Result type of argument token for '{functionParameterMetadata.Name}' parameter is invalid. Expected: '{functionParameterMetadata.Type}' or its child. Actual: '{argumentExpression.ResultType}'.");
            }

            return argumentExpression;
        }
    }
}
