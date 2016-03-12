using System;
using System.Collections.Generic;
using FluentAssertions;
using Manisero.DSLExecutor.Domain.ExpressionsDomain;
using Xunit;

namespace Manisero.DSLExecutor.Tests.DSLExecutorTests
{
    public class DSLExecutorTests
    {
        private object Act(IExpression expression)
        {
            var functionTypeToHandlerTypeMap = new Dictionary<Type, Type>
                {
                    [typeof(AddFunction)] = typeof(AddFunctionHandler),
                    [typeof(SubstractFunction)] = typeof(SubstractFunctionHandler),
                    [typeof(LogFunction)] = typeof(LogFunctionHandler)
                };

            var dslExecutor = new DSLExecutor(functionTypeToHandlerTypeMap);

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

        [Theory]
        [InlineData(0, 3)]
        [InlineData(1, 5)]
        [InlineData(5, 8)]
        public void function_expression(int a, int b)
        {
            var expression = new FunctionExpression<AddFunction, int>
                {
                    ArgumentExpressions = new Dictionary<string, IExpression>
                        {
                            [nameof(AddFunction.A)] = new ConstantExpression<int> { Value = a },
                            [nameof(AddFunction.B)] = new ConstantExpression<int> { Value = b }
                        }
                };

            var result = Act(expression);

            result.Should().Be(a + b);
        }

        [Theory]
        [InlineData(0, 3, 5)]
        [InlineData(1, 5, 10)]
        [InlineData(5, 8, 15)]
        public void function_expression_nested(int a, int b, int c)
        {
            var expression = new FunctionExpression<SubstractFunction, int>
                {
                    ArgumentExpressions = new Dictionary<string, IExpression>
                        {
                            [nameof(SubstractFunction.A)] = new FunctionExpression<AddFunction, int>
                                {
                                    ArgumentExpressions = new Dictionary<string, IExpression>
                                        {
                                            [nameof(AddFunction.A)] = new ConstantExpression<int> { Value = a },
                                            [nameof(AddFunction.B)] = new ConstantExpression<int> { Value = b }
                                        }
                                },
                            [nameof(SubstractFunction.B)] = new ConstantExpression<int> { Value = c }
                        }
                };

            var result = Act(expression);

            result.Should().Be(a + b - c);
        }

        [Theory]
        [InlineData("log1", "log2", 3)]
        public void batch_expression(string log1, string log2, int constant)
        {
            var expression = new BatchExpression<int>
                {
                    SideExpressions = new IExpression[]
                        {
                            new FunctionExpression<LogFunction, Domain.FunctionsDomain.Void>
                                {
                                    ArgumentExpressions = new Dictionary<string, IExpression>
                                        {
                                            [nameof(LogFunction.Log)] = new ConstantExpression<string> { Value = log1 }
                                        }
                                },
                            new FunctionExpression<LogFunction, Domain.FunctionsDomain.Void>
                                {
                                    ArgumentExpressions = new Dictionary<string, IExpression>
                                        {
                                            [nameof(LogFunction.Log)] = new ConstantExpression<string> { Value = log2 }
                                        }
                                }
                        },
                    ResultExpression = new ConstantExpression<int> { Value = constant }
                };

            var result = Act(expression);

            result.Should().Be(constant);
            LogStore.GetLog().Should().Equal(log1, log2);
        }
    }
}
