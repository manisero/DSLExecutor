using FluentAssertions;
using Manisero.DSLExecutor.Domain.ExpressionsDomain;
using Manisero.DSLExecutor.Domain.FunctionsDomain;
using Manisero.DSLExecutor.Parser.SampleDSL.ExpressionGeneration.FunctionExpressionGeneration;
using Manisero.DSLExecutor.Parser.SampleDSL.Parsing.Tokens;
using Manisero.DSLExecutor.Parser.SampleDSL.Tests.TestsDomain;
using Manisero.DSLExecutor.Utilities;
using Xunit;

namespace Manisero.DSLExecutor.Parser.SampleDSL.Tests.ExpressionGeneration.FunctionExpressionGeneration
{
    public class FunctionExpressionGeneratorTests
    {
        private IFunctionExpression Act(FunctionCall functionCall,
                                        IFunctionTypeResolver functionTypeResolver = null,
                                        IFunctionMetadataProvider functionMetadataProvider = null,
                                        IFunctionArgumentExpressionsGenerator functionArgumentExpressionsGenerator = null)
        {
            var generator = new FunctionExpressionGenerator(functionTypeResolver, functionMetadataProvider, functionArgumentExpressionsGenerator);

            return generator.Generate(functionCall);
        }

        [Fact]
        public void EmptyFunction___EmptyFunctionExpression_with_no_arguments()
        {
            var functionCall = new FunctionCall
                {
                    FunctionName = nameof(EmptyFunction)
                };

            var result = Act(functionCall);

            result.Should().NotBeNull();
            result.Should().BeOfType<FunctionExpression<EmptyFunction, Void>>();
            result.ArgumentExpressions.Should().BeEmpty();
        }
    }
}
