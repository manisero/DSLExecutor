using FluentAssertions;
using Manisero.DSLExecutor.Domain.ExpressionsDomain;
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

            return JsonConvert.SerializeObject(expression, serializerSettings);
        }

        [Fact]
        public void constant_expression()
        {
            var expression = new ConstantExpression<int>
                {
                    Value = 3
                };

            var result = Act(Serialize(expression));

            result.ShouldBeEquivalentTo(expression);
        }
    }
}
