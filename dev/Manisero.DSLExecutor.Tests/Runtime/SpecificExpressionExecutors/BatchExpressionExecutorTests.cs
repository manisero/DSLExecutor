using System;
using Manisero.DSLExecutor.Domain.ExpressionsDomain;
using Manisero.DSLExecutor.Runtime;
using Manisero.DSLExecutor.Runtime.SpecificExpressionExecutors;
using Xunit;

namespace Manisero.DSLExecutor.Tests.Runtime.SpecificExpressionExecutors
{
    public class BatchExpressionExecutorTests
    {
        private object Execute(IBatchExpression expression, IExpressionExecutor expressionExecutor)
        {
            var executor = new BatchExpressionExecutor(new Lazy<IExpressionExecutor>(() => expressionExecutor));

            return executor.Execute(expression);
        }

        [Fact]
        public void executes_SideExpressions_in_order()
        {
            
        }

        [Fact]
        public void returns_ResultExpression_result()
        {

        }

        [Fact]
        public void executes_SideExpressions_before_ResultExpression()
        {

        }
    }
}
