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
        public void parses_literal_without_escaped_characters(string input, string expectedValue)
        {
            var result = Parsers.LiteralParser.Parse(input);

            result.Should().NotBeNull();
            result.Value.Should().Be(expectedValue);
        }
    }
}
