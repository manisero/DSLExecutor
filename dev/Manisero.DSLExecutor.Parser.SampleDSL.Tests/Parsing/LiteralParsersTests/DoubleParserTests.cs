using FluentAssertions;
using Manisero.DSLExecutor.Parser.SampleDSL.Parsing;
using Sprache;
using Xunit;

namespace Manisero.DSLExecutor.Parser.SampleDSL.Tests.Parsing.LiteralParsersTests
{
    public class DoubleParserTests
    {
        private double Act(string input)
        {
            return LiteralParsers.DoubleParsers.DoubleParser.Parse(input);
        }

        [Theory]
        [InlineData("1.1", 1.1)]
        [InlineData("12.34", 12.34)]
        [InlineData("001.002", 1.002)]
        public void parses_double(string input, double expectedValue)
        {
            var result = Act(input);

            result.Should().Be(expectedValue);
        }
    }
}
