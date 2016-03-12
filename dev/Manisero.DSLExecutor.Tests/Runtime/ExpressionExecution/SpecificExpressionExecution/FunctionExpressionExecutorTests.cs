using System.Collections.Generic;
using FluentAssertions;
using Manisero.DSLExecutor.Domain.ExpressionsDomain;
using Manisero.DSLExecutor.Runtime.ExpressionExecution.SpecificExpressionExecution;
using Manisero.DSLExecutor.Runtime.ExpressionExecution.SpecificExpressionExecution.FunctionExpressionExecution;
using Manisero.DSLExecutor.Runtime.FunctionExecution;
using Manisero.DSLExecutor.Tests.TestsDomain;
using NSubstitute;
using Xunit;

namespace Manisero.DSLExecutor.Tests.Runtime.ExpressionExecution.SpecificExpressionExecution
{
    public class FunctionExpressionExecutorTests
    {
        private object Act(IFunctionExpression expression,
                           IFunctionParametersFiller functionParametersFiller = null,
                           IFunctionExecutor functionExecutor = null)
        {
            var executor = new FunctionExpressionExecutor(functionParametersFiller ?? Substitute.For<IFunctionParametersFiller>(),
                                                          functionExecutor ?? Substitute.For<IFunctionExecutor>());

            return executor.Execute(expression);
        }

        [Fact]
        public void fills_function_parameters()
        {
            var expression = new FunctionExpression<FunctionWithParameters, int>
                {
                    ArgumentExpressions = new Dictionary<string, IExpression>()
                };

            var functionParametersFiller = Substitute.For<IFunctionParametersFiller>();
            var parametersFilled = false;

            functionParametersFiller.When(x => x.Fill(Arg.Any<FunctionWithParameters>(), expression.ArgumentExpressions))
                                    .Do(_ => parametersFilled = true);

            Act(expression, functionParametersFiller: functionParametersFiller);

            parametersFilled.Should().BeTrue();
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
    }
}
