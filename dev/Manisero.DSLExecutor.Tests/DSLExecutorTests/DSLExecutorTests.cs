using FluentAssertions;
using Manisero.DSLExecutor.Domain.ExpressionsDomain;
using Xunit;

namespace Manisero.DSLExecutor.Tests.DSLExecutorTests
{
    public class DSLExecutorTests
    {
        private object Act(IExpression expression)
        {
            var dslExecutor = new DSLExecutor(null);

            return dslExecutor.ExecuteExpression(expression);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        public void constant_expression(int expressionValue)
        {
            var expression = new ConstantExpression<int> { Value = expressionValue};

            var result = Act(expression);

            result.Should().Be(expressionValue);
        }

        // TODO: Add more tests
    }
}
