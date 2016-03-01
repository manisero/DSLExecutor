using FluentAssertions;
using Manisero.DSLExecutor.Domain.ExpressionsDomain;
using Manisero.DSLExecutor.Runtime.SpecificExpressionExecutors;
using Xunit;

namespace Manisero.DSLExecutor.Tests.Runtime.SpecificExpressionExecutors
{
    public class ConstantExpressionExecutorTests
    {
        private object Act(IConstantExpression expression)
        {
            var executor = new ConstantExpressionExecutor();

            return executor.Execute(expression);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        public void returns_expression_Value(int expressionValue)
        {
            var expression = new ConstantExpression<int> { Value = expressionValue };

            var result = Act(expression);

            result.Should().Be(expressionValue);
        }
    }
}
