using System;
using FluentAssertions;
using Manisero.DSLExecutor.Domain.ExpressionsDomain;
using Manisero.DSLExecutor.ExpressionExecution;
using Manisero.DSLExecutor.ExpressionExecution.SpecificExpressionExecution;
using Manisero.DSLExecutor.Tests.TestsDomain;
using NSubstitute;
using Xunit;

namespace Manisero.DSLExecutor.Tests.ExpressionExecution
{
    public class ExpressionExecutorTests
    {
        private object Act(IExpression expression,
                           IConstantExpressionExecutor constantExpressionExecutor = null,
                           IFunctionExpressionExecutor functionExpressionExecutor = null,
                           IBatchExpressionExecutor batchExpressionExecutor = null)
        {
            var expressionExecutor = new ExpressionExecutor(constantExpressionExecutor, functionExpressionExecutor, batchExpressionExecutor);

            return expressionExecutor.Execute(expression);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        public void ConstantExpression___invokes_constantExpressionExecutor(int expressionResult)
        {
            var expression = new ConstantExpression<int>();

            var constantExpressionExecutor = Substitute.For<IConstantExpressionExecutor>();
            constantExpressionExecutor.Execute(expression)
                                      .Returns(expressionResult);

            var result = Act(expression, constantExpressionExecutor: constantExpressionExecutor);

            result.Should().Be(expressionResult);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        public void FunctionExpression___invokes_functionExpressionExecutor(int expressionResult)
        {
            var expression = new FunctionExpression<FunctionWithoutParameters, int>();

            var functionExpressionExecutor = Substitute.For<IFunctionExpressionExecutor>();
            functionExpressionExecutor.Execute(expression)
                                      .Returns(expressionResult);

            var result = Act(expression, functionExpressionExecutor: functionExpressionExecutor);

            result.Should().Be(expressionResult);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        public void BatchExpression___invokes_batchExpressionExecutor(int expressionResult)
        {
            var expression = new BatchExpression<int>();

            var batchExpressionExecutor = Substitute.For<IBatchExpressionExecutor>();
            batchExpressionExecutor.Execute(expression)
                                   .Returns(expressionResult);

            var result = Act(expression, batchExpressionExecutor: batchExpressionExecutor);

            result.Should().Be(expressionResult);
        }

        [Fact]
        public void unsupported_expression___exception()
        {
            Action act = () => Act(new EmptyExpression());

            act.ShouldThrow<NotSupportedException>();
        }
    }
}
