using FluentAssertions;
using Manisero.DSLExecutor.Parser.SampleDSL.Parsing;
using Manisero.DSLExecutor.Parser.SampleDSL.Parsing.Tokens;
using Sprache;
using Xunit;

namespace Manisero.DSLExecutor.Parser.SampleDSL.Tests.Parsing
{
    public class LiteralParserTests
    {
        private Literal Act(string input)
        {
            return Parsers.LiteralParser.Parse(input);
        }

        public void parses_empty_literal()
        {
            var result = Act("\"\"");

            result.Should().NotBeNull();
            result.Value.Should().Be("");
        }

        [Theory]
        [InlineData("'a'", "a")]
        [InlineData("'ab'", "ab")]
        [InlineData("'1'", "1")]
        [InlineData("'1.2'", "1.2")]
        public void parses_literal_without_escaped_characters(string input, string expectedValue)
        {
            var result = Act(input);
            
            result.Value.Should().Be(expectedValue);
        }

        [Theory]
        [InlineData("' a '", " a ")]
        [InlineData("'  a  '", "  a  ")]
        [InlineData("'a b'", "a b")]
        public void keeps_spaces_within_literal(string input, string expectedValue)
        {
            var result = Act(input);
            
            result.Value.Should().Be(expectedValue);
        }

        [Theory]
        [InlineData(" 'a' ", "a")]
        [InlineData("  'a'  ", "a")]
        public void ignores_spaces_around_literal(string input, string expectedValue)
        {
            var result = Act(input);
            
            result.Value.Should().Be(expectedValue);
        }

        [Theory]
        [InlineData("'\''", "'")]
        public void parses_literal_with_escaped_characters(string input, string expectedValue)
        {
            var result = Act(input);

            result.Value.Should().Be(expectedValue);
        }
    }
}
