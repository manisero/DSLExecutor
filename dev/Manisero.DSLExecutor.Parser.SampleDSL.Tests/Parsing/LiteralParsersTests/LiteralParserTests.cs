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

            result.Value.Should().Be(expectedValue);
        }
    }
}
