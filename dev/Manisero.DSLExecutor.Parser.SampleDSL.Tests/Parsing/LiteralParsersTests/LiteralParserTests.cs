using FluentAssertions;
using Manisero.DSLExecutor.Parser.SampleDSL.Parsing;
using Manisero.DSLExecutor.Parser.SampleDSL.Parsing.Tokens;
using Sprache;
using Xunit;

namespace Manisero.DSLExecutor.Parser.SampleDSL.Tests.Parsing.LiteralParsersTests
{
    public class LiteralParserTests
    {
        private Literal Act(string input)
        {
            return LiteralParsers.LiteralParser.Parse(input);
        }

        [Theory]
        [InlineData("'a'", "a")]
        public void parses_string(string input, string expectedValue)
        {
            var result = Act(input);

            result.Value.Should().BeOfType<string>();
            result.Value.As<string>().Should().Be(expectedValue);
        }

        [Theory]
        [InlineData("1", 1)]
        [InlineData("123", 123)]
        [InlineData("001", 1)]
        public void parses_int(string input, int expectedValue)
        {
            var result = Act(input);

            result.Value.Should().BeOfType<int>();
            result.Value.As<int>().Should().Be(expectedValue);
        }
    }
}
