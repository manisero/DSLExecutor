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
            //new ConstantExpressionExecutor()
        }
    }
}
