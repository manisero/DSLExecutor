using FluentAssertions;
using Sprache;
using Xunit;

namespace Manisero.DSLExecutor.Parser.SampleDSL.Tests
{
    public class LiteralParserTests
    {
        [Theory]
        [InlineData("\"a\"", "a")]
        [InlineData("  \"a\"  ", "a")]
        [InlineData("\"  a  \"", "  a  ")]
        [InlineData("\"ab\"", "ab")]
        [InlineData("\"a b\"", "a b")]
        [InlineData("\"1\"", "1")]
        public void parses_literal_without_escaped_characters(string literal, string expectedResult)
        {
            var parser = (from startQuote in Parse.Char('"')
                          from content in Parse.CharExcept('"').Many().Text()
                          from endQuote in Parse.Char('"')
                          select content).Token();

            var result = parser.Parse(literal);

            result.Should().Be(expectedResult);
        }
    }
}
