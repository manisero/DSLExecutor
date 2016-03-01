using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Manisero.DSLExecutor.Domain.ExpressionsDomain;
using Manisero.DSLExecutor.Runtime;
using Manisero.DSLExecutor.Runtime.SpecificExpressionExecutors;
using Manisero.DSLExecutor.Tests.TestsDomain;
using NSubstitute;
using Xunit;

namespace Manisero.DSLExecutor.Tests.Runtime.SpecificExpressionExecutors
{
    public class BatchExpressionExecutorTests
    {
        private object Act(IBatchExpression expression, IExpressionExecutor expressionExecutor)
        {
            var executor = new BatchExpressionExecutor(new Lazy<IExpressionExecutor>(() => expressionExecutor));

            return executor.Execute(expression);
        }

        [Fact]
        public void executes_SideExpressions_in_order()
        {
            var expression = new BatchExpression<int>
                {
                    SideExpressions = new[] { new EmptyExpression(), new EmptyExpression(), new EmptyExpression() }
                };

            var executionOrder = new List<int>();

            var expressionExecutor = Substitute.For<IExpressionExecutor>();

            for (var i = 0; i < 3; i++)
            {
                var order = i;

                expressionExecutor.Execute(expression.SideExpressions.ElementAt(i))
                                  .Returns(_ =>
                                               {
                                                   executionOrder.Add(order);
                                                   return 0;
                                               });
            }

            Act(expression, expressionExecutor);

            executionOrder.Should().Equal(0, 1, 2);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        public void returns_ResultExpression_result(int resultExpressionResult)
        {
            var expression = new BatchExpression<int>
                {
                    ResultExpression = new EmptyExpression<int>()
                };

            var expressionExecutor = Substitute.For<IExpressionExecutor>();
            expressionExecutor.Execute(expression.ResultExpression)
                              .Returns(x => resultExpressionResult);

            var result = Act(expression, expressionExecutor);

            result.Should().Be(resultExpressionResult);
        }

        [Fact]
        public void executes_SideExpressions_before_ResultExpression()
        {
            var expression = new BatchExpression<int>
                {
                    SideExpressions = new[] { new EmptyExpression() },
                    ResultExpression = new EmptyExpression<int>()
                };

            var executionOrder = new List<int>();

            var expressionExecutor = Substitute.For<IExpressionExecutor>();

            expressionExecutor.Execute(expression.SideExpressions.First())
                              .Returns(_ =>
                                           {
                                               executionOrder.Add(0);
                                               return 0;
                                           });

            expressionExecutor.Execute(expression.ResultExpression)
                              .Returns(_ =>
                                           {
                                               executionOrder.Add(1);
                                               return 0;
                                           });

            Act(expression, expressionExecutor);

            executionOrder.Should().Equal(0, 1);
        }
    }
}
