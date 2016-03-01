using Manisero.DSLExecutor.Domain.ExpressionsDomain;
using Manisero.DSLExecutor.Runtime;
using Xunit;

namespace Manisero.DSLExecutor.Tests
{
    public class Test
    {
        [Fact]
        public void FailingTest()
        {
            Assert.Equal(true, false);
        }

        [Fact]
        public void PassingTest()
        {
            Assert.Equal(true, true);
        }

        [Fact]
        public void ClassFromOtherProject()
        {
            var expression = new ConstantExpression<int>
            {
                Value = 1
            };

            var constantExpressionExecutor = new ConstantExpressionExecutor();

            var result = constantExpressionExecutor.Execute(expression);

            Assert.Equal(1, result);
        }
    }
}
