using System;
using System.Collections.Generic;
using FluentAssertions;
using Manisero.DSLExecutor.Domain.ExpressionsDomain;
using Manisero.DSLExecutor.Runtime.ExpressionExecution;
using Manisero.DSLExecutor.Runtime.ExpressionExecution.SpecificExpressionExecutors;
using Manisero.DSLExecutor.Runtime.FunctionExecution;
using Manisero.DSLExecutor.Tests.TestsDomain;
using NSubstitute;
using Xunit;

namespace Manisero.DSLExecutor.Tests.Runtime.ExpressionExecution.SpecificExpressionExecutors
{
    public class FunctionExpressionExecutorTests
    {
        private object Act(IFunctionExpression expression,
                           IExpressionExecutor expressionExecutor = null,
                           IFunctionExecutor functionExecutor = null)
        {
            var executor = new FunctionExpressionExecutor(new Lazy<IExpressionExecutor>(() => expressionExecutor),
                                                          functionExecutor ?? Substitute.For<IFunctionExecutor>());

            return executor.Execute(expression);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        public void returns_functionExecutor_result(int functionExecutorResult)
        {
            var expression = new FunctionExpression<FunctionWithoutParameters, int>();

            var functionExecutor = Substitute.For<IFunctionExecutor>();
            functionExecutor.Execute<FunctionWithoutParameters, int>(Arg.Any<FunctionWithoutParameters>())
                            .Returns(functionExecutorResult);

            var result = Act(expression, functionExecutor: functionExecutor);

            result.Should().Be(functionExecutorResult);
        }

        [Theory]
        [InlineData(0, null)]
        [InlineData(1, "a")]
        [InlineData(5, "b")]
        public void function_with_parameters___passes_function_with_filled_parameters_to_functionExecutor(int argument1Value, string argument2Value)
        {
            var expression = new FunctionExpression<FunctionWithParameters, int>
                {
                    ArgumentExpressions = new Dictionary<string, IExpression>
                        {
                            [nameof(FunctionWithParameters.Parameter1)] = new ConstantExpression<int>(),
                            [nameof(FunctionWithParameters.Parameter2)] = new ConstantExpression<string>()
                        }
                };

            var expressionExecutor = Substitute.For<IExpressionExecutor>();

            expressionExecutor.Execute(expression.ArgumentExpressions[nameof(FunctionWithParameters.Parameter1)])
                              .Returns(argument1Value);

            expressionExecutor.Execute(expression.ArgumentExpressions[nameof(FunctionWithParameters.Parameter2)])
                              .Returns(argument2Value);

            FunctionWithParameters executedFunction = null;

            var functionExecutor = Substitute.For<IFunctionExecutor>();
            functionExecutor.Execute<FunctionWithParameters, int>(Arg.Any<FunctionWithParameters>())
                            .Returns(callInfo =>
                                         {
                                             executedFunction = callInfo.Arg<FunctionWithParameters>();
                                             return 0;
                                         });

            Act(expression, expressionExecutor, functionExecutor);

            executedFunction.Should().NotBeNull();
            executedFunction.Parameter1.Should().Be(argument1Value);
            executedFunction.Parameter2.Should().Be(argument2Value);
        }

        [Fact]
        public void executes_ArgumentExpressions_in_order()
        {
            var expression = new FunctionExpression<FunctionWithParameters, int>
                {
                    ArgumentExpressions = new Dictionary<string, IExpression>
                        {
                            [nameof(FunctionWithParameters.Parameter2)] = new ConstantExpression<string>(),
                            [nameof(FunctionWithParameters.Parameter1)] = new ConstantExpression<int>()
                        }
                };

            var executionOrder = new List<int>();

            var expressionExecutor = Substitute.For<IExpressionExecutor>();

            expressionExecutor.Execute(expression.ArgumentExpressions[nameof(FunctionWithParameters.Parameter1)])
                              .Returns(_ =>
                                           {
                                               executionOrder.Add(0);
                                               return 0;
                                           });

            expressionExecutor.Execute(expression.ArgumentExpressions[nameof(FunctionWithParameters.Parameter2)])
                              .Returns(_ =>
                                           {
                                               executionOrder.Add(1);
                                               return null;
                                           });

            Act(expression, expressionExecutor: expressionExecutor);

            executionOrder.Should().Equal(0, 1);
        }
    }
}
