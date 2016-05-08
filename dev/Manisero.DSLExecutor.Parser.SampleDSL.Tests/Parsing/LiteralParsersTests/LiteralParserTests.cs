using FluentAssertions;
using Manisero.DSLExecutor.Parser.SampleDSL.Parsing;
using Sprache;
using Xunit;

namespace Manisero.DSLExecutor.Parser.SampleDSL.Tests.Parsing
{
    public class LiteralParserTests
    {
        private string Act(string input)
        {
            return LiteralParsers.StringParsers.StringParser.Parse(input);
        }

        public void parses_empty_string()
        {
            var result = Act("\"\"");

            result.Should().NotBeNull();
            result.Should().Be("");
        }

        [Theory]
        [InlineData("'a'", "a")]
        [InlineData("'ab'", "ab")]
        [InlineData("'1'", "1")]
        [InlineData("'1.2'", "1.2")]
        public void parses_string_without_escaped_characters(string input, string expectedValue)
        {
            var result = Act(input);
            
            result.Should().Be(expectedValue);
        }

        [Theory]
        [InlineData("' a '", " a ")]
        [InlineData("'  a  '", "  a  ")]
        [InlineData("'a b'", "a b")]
        public void keeps_spaces_within_string(string input, string expectedValue)
        {
            var result = Act(input);
            
            result.Should().Be(expectedValue);
        }

        [Theory]
        [InlineData(" 'a' ", "a")]
        [InlineData("  'a'  ", "a")]
        public void ignores_spaces_around_string(string input, string expectedValue)
        {
            var result = Act(input);
            
            result.Should().Be(expectedValue);
        }

        [Theory]
        [InlineData("'\\\\'", "\\")]
        [InlineData("'\\''", "'")]
        [InlineData("'a\\\\b\\'c'", "a\\b'c")]
        public void parses_string_with_escaped_characters(string input, string expectedValue)
        {
            var result = Act(input);

            result.Should().Be(expectedValue);
        }
    }
}
