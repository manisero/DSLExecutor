using FluentAssertions;
using Manisero.DSLExecutor.Parser.SampleDSL.Tokens;
using Sprache;
using Xunit;

namespace Manisero.DSLExecutor.Parser.SampleDSL.Tests
{
    public class LiteralParserTests
    {
        private Literal Act(string input)
        {
            return Parsers.LiteralParser.Parse(input);
        }

        [Theory]
        [InlineData("\"a\"", "a")]
        [InlineData("  \"a\"  ", "a")]
        [InlineData("\"  a  \"", "  a  ")]
        [InlineData("\"ab\"", "ab")]
        [InlineData("\"a b\"", "a b")]
        [InlineData("\"1\"", "1")]
        [InlineData("\"\"", "")]
        public void parses_literal_without_escaped_characters(string input, string expectedValue)
        {
            var result = Act(input);

            result.Should().NotBeNull();
            result.Value.Should().Be(expectedValue);
        }
    }
}
