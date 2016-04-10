using System.Collections.Generic;
using FluentAssertions;
using Manisero.DSLExecutor.Domain.ExpressionsDomain;
using Manisero.DSLExecutor.Domain.FunctionsDomain;
using Manisero.DSLExecutor.Parser.Json.Tests.TestsDomain;
using Newtonsoft.Json;
using Xunit;

namespace Manisero.DSLExecutor.Parser.Json.Tests
{
    public class JsonParserTests
    {
        private IExpression Act(string json)
        {
            var parser = new JsonParser();

            return parser.Parse(json);
        }

        private string Serialize(IExpression expression)
        {
            var serializerSettings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                };

            return JsonConvert.SerializeObject(expression, Formatting.Indented, serializerSettings);
        }

        [Fact]
        public void ConstantExpression()
        {
            var expression = new ConstantExpression<int>
                {
                    Value = 3
                };

            var result = Act(Serialize(expression));

            result.ShouldBeEquivalentTo(expression);
        }

        [Fact]
        public void FunctionExpression()
        {
            var expression = new FunctionExpression<AddFunction, int>
                {
                    ArgumentExpressions = new Dictionary<string, IExpression>
                        {
                            [nameof(AddFunction.A)] = new ConstantExpression<int> { Value = 3 },
                            [nameof(AddFunction.B)] = new ConstantExpression<int> { Value = 5 }
                        }
                };

            var result = Act(Serialize(expression));

            result.ShouldBeEquivalentTo(expression);
        }

        [Fact]
        public void BatchExpression()
        {
            var expression = new BatchExpression<string>
                {
                    SideExpressions = new IExpression[]
                        {
                            new ConstantExpression<int> { Value = 3 },
                            new ConstantExpression<double> { Value = 5.0 }
                        },
                    ResultExpression = new ConstantExpression<string> { Value = "value" }
                };

            var result = Act(Serialize(expression));

            result.ShouldBeEquivalentTo(expression);
        }

        [Fact]
        public void complex_expression()
        {
            var expression = new BatchExpression<int>
                {
                    SideExpressions = new IExpression[]
                        {
                            new FunctionExpression<LogFunction, Void>
                                {
                                    ArgumentExpressions = new Dictionary<string, IExpression>
                                        {
                                            [nameof(LogFunction.Text)] = new ConstantExpression<string> { Value = "Calculating..." }
                                        }
                                }
                        },
                    ResultExpression = new FunctionExpression<SubFunction, int>
                        {
                            ArgumentExpressions = new Dictionary<string, IExpression>
                                {
                                    [nameof(SubFunction.A)] = new FunctionExpression<AddFunction, int>
                                        {
                                            ArgumentExpressions = new Dictionary<string, IExpression>
                                                {
                                                    [nameof(AddFunction.A)] = new ConstantExpression<int> { Value = 1 },
                                                    [nameof(AddFunction.B)] = new ConstantExpression<int> { Value = 2 }
                                                }
                                        },
                                    [nameof(SubFunction.B)] = new ConstantExpression<int> { Value = 3 }
                                }
                        }
                };

            var result = Act(Serialize(expression));

            result.ShouldBeEquivalentTo(expression);
        }

        [Fact]
        public void manual_json()
        {
            var json =
@"{
    $type: ""Manisero.DSLExecutor.Domain.ExpressionsDomain.ConstantExpression`1[[System.Int32, mscorlib]], Manisero.DSLExecutor"",
    Value: 3
}";

            var result = Act(json);

            result.Should().NotBeNull();
        }
    }
}
