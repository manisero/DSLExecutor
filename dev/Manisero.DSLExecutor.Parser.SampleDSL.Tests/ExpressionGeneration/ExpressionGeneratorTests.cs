using FluentAssertions;
using Manisero.DSLExecutor.Domain.ExpressionsDomain;
using Manisero.DSLExecutor.Domain.FunctionsDomain;
using Manisero.DSLExecutor.Parser.SampleDSL.ExpressionGeneration;
using Manisero.DSLExecutor.Parser.SampleDSL.Parsing.Tokens;
using Manisero.DSLExecutor.Parser.SampleDSL.Tests.TestsDomain;
using Xunit;

namespace Manisero.DSLExecutor.Parser.SampleDSL.Tests.ExpressionGeneration
{
    public class ExpressionGeneratorTests
    {
        private IBatchExpression Act(TokenTree tokenTree, IFunctionExpressionGenerator functionExpressionGenerator = null)
        {
            var generator = new ExpressionGenerator(functionExpressionGenerator);

            return generator.Generate(tokenTree);
        }

        [Fact]
        public void single_FunctionCall___BatchExpression_with_ResultExpression_only()
        {
            var tokenTree = new TokenTree
                {
                    FunctionCalls = new[]
                        {
                            new FunctionCall
                                {
                                    FunctionName = nameof(EmptyFunction)
                                }
                        }
                };

            var result = Act(tokenTree);

            result.Should().NotBeNull();
            result.SideExpressions.Should().BeEmpty();
            result.ResultExpression.As<FunctionExpression<EmptyFunction, Void>>()
                  .ArgumentExpressions.Should().BeEmpty();
        }
    }
}
