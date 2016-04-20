using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Manisero.DSLExecutor.Domain.ExpressionsDomain;
using Manisero.DSLExecutor.Parser.SampleDSL.ExpressionGeneration;
using Manisero.DSLExecutor.Parser.SampleDSL.ExpressionGeneration.BatchExpressionGeneration;
using Manisero.DSLExecutor.Parser.SampleDSL.Parsing.Tokens;
using Manisero.DSLExecutor.Parser.SampleDSL.Tests.TestsDomain;
using NSubstitute;
using Xunit;

namespace Manisero.DSLExecutor.Parser.SampleDSL.Tests.ExpressionGeneration.BatchExpressionGeneration
{
    public class BatchExpressionGeneratorTests
    {
        private IBatchExpression Act(TokenTree tokenTree, IExpressionGenerator expressionGenerator = null)
        {
            var generator = new BatchExpressionGenerator(new Lazy<IExpressionGenerator>(() => expressionGenerator));

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

            var expressionGenerator = Substitute.For<IExpressionGenerator>();
            expressionGenerator.Generate(tokenTree.FunctionCalls[0])
                                       .Returns(functionExpression);

            var result = Act(tokenTree, expressionGenerator);

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

            var expressionGenerator = Substitute.For<IExpressionGenerator>();

            foreach (var expression in functionExpressions)
            {
                expressionGenerator.Generate(expression.Item1).Returns(expression.Item2);
            }

            var result = Act(tokenTree, expressionGenerator);

            result.Should().NotBeNull();
            result.SideExpressions.ShouldAllBeEquivalentTo(functionExpressions.Take(2).Select(x => x.Item2).ToList());
            result.ResultExpression.Should().BeSameAs(functionExpressions.Last().Item2);
        }
    }
}
