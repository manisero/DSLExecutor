using FluentAssertions;
using Manisero.DSLExecutor.Parser.SampleDSL.Parsing;
using Sprache;
using Xunit;

namespace Manisero.DSLExecutor.Parser.SampleDSL.Tests.Parsing.LiteralParsersTests
{
    public class IntParserTests
    {
        private int Act(string input)
        {
            return LiteralParsers.IntParser.Parse(input);
        }

        [Theory]
        [InlineData("1", 1)]
        [InlineData("123", 123)]
        [InlineData("001", 1)]
        public void parses_int(string input, int expectedValue)
        {
            var result = Act(input);
            
            result.Should().Be(expectedValue);
        }
    }
}
