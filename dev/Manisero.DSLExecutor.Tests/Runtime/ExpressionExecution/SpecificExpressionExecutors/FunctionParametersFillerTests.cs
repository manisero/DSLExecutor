using System;
using System.Collections.Generic;
using Manisero.DSLExecutor.Domain.ExpressionsDomain;
using Manisero.DSLExecutor.Runtime.ExpressionExecution;
using Manisero.DSLExecutor.Runtime.ExpressionExecution.SpecificExpressionExecutors;
using Manisero.DSLExecutor.Tests.TestsDomain;
using NSubstitute;
using Xunit;
using FluentAssertions;

namespace Manisero.DSLExecutor.Tests.Runtime.ExpressionExecution.SpecificExpressionExecutors
{
    public class FunctionParametersFillerTests
    {
        private void Act<TFunction>(TFunction function, IDictionary<string, IExpression> argumentExpressions, IExpressionExecutor expressionExecutor = null)
        {
            var filler = new FunctionParametersFiller(new Lazy<IExpressionExecutor>(() => expressionExecutor));

            filler.Fill(function, argumentExpressions);
        }

        [Theory]
        [InlineData(0, null)]
        [InlineData(1, "a")]
        [InlineData(5, "b")]
        public void function_with_parameters___passes_function_with_filled_parameters_to_functionExecutor(int argument1Value, string argument2Value)
        {
            var function = new FunctionWithParameters();
            var argumentExpressions = new Dictionary<string, IExpression>
                {
                    [nameof(FunctionWithParameters.Parameter1)] = new ConstantExpression<int>(),
                    [nameof(FunctionWithParameters.Parameter2)] = new ConstantExpression<string>()
                };

            var expressionExecutor = Substitute.For<IExpressionExecutor>();

            expressionExecutor.Execute(argumentExpressions[nameof(FunctionWithParameters.Parameter1)])
                              .Returns(argument1Value);

            expressionExecutor.Execute(argumentExpressions[nameof(FunctionWithParameters.Parameter2)])
                              .Returns(argument2Value);

            Act(function, argumentExpressions, expressionExecutor);
            
            function.Parameter1.Should().Be(argument1Value);
            function.Parameter2.Should().Be(argument2Value);
        }

        [Fact]
        public void executes_argumentExpressions_in_order()
        {
            var function = new FunctionWithParameters();
            var argumentExpressions = new Dictionary<string, IExpression>
                {
                    [nameof(FunctionWithParameters.Parameter2)] = new ConstantExpression<string>(),
                    [nameof(FunctionWithParameters.Parameter1)] = new ConstantExpression<int>()
                };

            var expressionExecutor = Substitute.For<IExpressionExecutor>();
            var executionOrder = new List<int>();

            expressionExecutor.Execute(argumentExpressions[nameof(FunctionWithParameters.Parameter1)])
                              .Returns(_ =>
                                           {
                                               executionOrder.Add(0);
                                               return 0;
                                           });

            expressionExecutor.Execute(argumentExpressions[nameof(FunctionWithParameters.Parameter2)])
                              .Returns(_ =>
                                           {
                                               executionOrder.Add(1);
                                               return null;
                                           });

            Act(function, argumentExpressions, expressionExecutor);

            executionOrder.Should().Equal(0, 1);
        }
    }
}
