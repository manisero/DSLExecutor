using Manisero.DSLExecutor.Domain.ExpressionsDomain;
using Manisero.DSLExecutor.Runtime;
using Xunit;

namespace Manisero.DSLExecutor.Tests.Runtime
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
        public void returns_expression_value(int value)
        {
            var expression = new ConstantExpression<int>
                {
                    Value = value
                };

            var result = Act(expression);

            Assert.Equal(value, result);
        }
    }
}
