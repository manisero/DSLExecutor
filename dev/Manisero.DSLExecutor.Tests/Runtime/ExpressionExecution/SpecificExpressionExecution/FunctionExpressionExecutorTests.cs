using System;
using System.Collections.Generic;
using FluentAssertions;
using Manisero.DSLExecutor.Domain.ExpressionsDomain;
using Manisero.DSLExecutor.Domain.FunctionsDomain;
using Manisero.DSLExecutor.Runtime.ExpressionExecution.SpecificExpressionExecution;
using Manisero.DSLExecutor.Runtime.ExpressionExecution.SpecificExpressionExecution.FunctionExpressionExecution;
using Manisero.DSLExecutor.Tests.TestsDomain;
using NSubstitute;
using Xunit;

namespace Manisero.DSLExecutor.Tests.Runtime.ExpressionExecution.SpecificExpressionExecution
{
    public class FunctionExpressionExecutorTests
    {
        private object Act(IFunctionExpression expression,
                           IFunctionParametersFiller functionParametersFiller = null,
                           IFunctionHandlerResolver functionHandlerResolver = null)
        {
            var executor = new FunctionExpressionExecutor(functionParametersFiller ?? Substitute.For<IFunctionParametersFiller>(),
                                                          functionHandlerResolver ?? Substitute.For<IFunctionHandlerResolver>());

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
        public void returns_functionHandler_result(int functionHandlerResult)
        {
            var expression = new FunctionExpression<FunctionWithoutParameters, int>();

            var functionHandler = Substitute.For<IFunctionHandler<FunctionWithoutParameters, int>>();
            functionHandler.Handle(Arg.Any<FunctionWithoutParameters>())
                           .Returns(functionHandlerResult);

            var functionHandlerResolver = Substitute.For<IFunctionHandlerResolver>();
            functionHandlerResolver.Resolve<FunctionWithoutParameters, int>()
                                   .Returns(functionHandler);

            var result = Act(expression, functionHandlerResolver: functionHandlerResolver);

            result.Should().Be(functionHandlerResult);
        }

        [Fact]
        public void functionHandler_null___exception()
        {
            var expression = new FunctionExpression<FunctionWithoutParameters, int>();

            var functionHandlerResolver = Substitute.For<IFunctionHandlerResolver>();
            functionHandlerResolver.Resolve<FunctionWithoutParameters, int>()
                                   .Returns((IFunctionHandler<FunctionWithoutParameters, int>)null);

            Action act = () => Act(expression, functionHandlerResolver: functionHandlerResolver);

            act.ShouldThrow<NotSupportedException>();
        }
    }
}
