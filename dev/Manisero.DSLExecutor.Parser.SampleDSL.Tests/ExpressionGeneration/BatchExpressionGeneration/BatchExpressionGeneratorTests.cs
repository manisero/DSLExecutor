using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Manisero.DSLExecutor.Domain.ExpressionsDomain;
using Manisero.DSLExecutor.Parser.SampleDSL.ExpressionGeneration.BatchExpressionGeneration;
using Manisero.DSLExecutor.Parser.SampleDSL.ExpressionGeneration.FunctionExpressionGeneration;
using Manisero.DSLExecutor.Parser.SampleDSL.Parsing.Tokens;
using Manisero.DSLExecutor.Parser.SampleDSL.Tests.TestsDomain;
using NSubstitute;
using Xunit;

namespace Manisero.DSLExecutor.Parser.SampleDSL.Tests.ExpressionGeneration.BatchExpressionGeneration
{
    public class BatchExpressionGeneratorTests
    {
        private IBatchExpression Act(TokenTree tokenTree, IFunctionExpressionGenerator functionExpressionGenerator = null)
        {
            var generator = new BatchExpressionGenerator(functionExpressionGenerator);

            return generator.Generate(tokenTree);
        }

        [Fact]
        public void single_FunctionCall___ResultExpression_only()
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

            var functionExpression = new FunctionExpression<EmptyFunction, Domain.FunctionsDomain.Void>();

            var functionExpressionGenerator = Substitute.For<IFunctionExpressionGenerator>();
            functionExpressionGenerator.Generate(tokenTree.FunctionCalls[0])
                                       .Returns(functionExpression);

            var result = Act(tokenTree, functionExpressionGenerator);

            result.Should().NotBeNull();
            result.SideExpressions.Should().BeEmpty();
            result.ResultExpression.Should().BeSameAs(functionExpression);
        }

        [Fact]
        public void multiple_FunctionCalls___SideExpressions_and_ResultExpression()
        {
            var functionExpressions = new List<Tuple<FunctionCall, IFunctionExpression>>
                {
                    new Tuple<FunctionCall, IFunctionExpression>(new FunctionCall { FunctionName = nameof(EmptyFunction) },
                                                                 new FunctionExpression<EmptyFunction, Domain.FunctionsDomain.Void>()),
                    new Tuple<FunctionCall, IFunctionExpression>(new FunctionCall { FunctionName = nameof(EmptyFunction) },
                                                                 new FunctionExpression<EmptyFunction, Domain.FunctionsDomain.Void>()),
                    new Tuple<FunctionCall, IFunctionExpression>(new FunctionCall { FunctionName = nameof(EmptyFunction) },
                                                                 new FunctionExpression<EmptyFunction, Domain.FunctionsDomain.Void>())
                };

            var tokenTree = new TokenTree
                {
                    FunctionCalls = functionExpressions.Select(x => x.Item1).ToList()
                };

            var functionExpressionGenerator = Substitute.For<IFunctionExpressionGenerator>();

            foreach (var expression in functionExpressions)
            {
                functionExpressionGenerator.Generate(expression.Item1).Returns(expression.Item2);
            }

            var result = Act(tokenTree, functionExpressionGenerator);

            result.Should().NotBeNull();
            result.SideExpressions.ShouldAllBeEquivalentTo(functionExpressions.Take(2).Select(x => x.Item2).ToList());
            result.ResultExpression.Should().BeSameAs(functionExpressions.Last().Item2);
        }
    }
}
