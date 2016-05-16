using FluentAssertions;
using Manisero.DSLExecutor.Parser.SampleDSL.Parsing;
using Sprache;
using Xunit;

namespace Manisero.DSLExecutor.Parser.SampleDSL.Tests.Parsing.LiteralParsersTests
{
    public class BoolParserTests
    {
        private bool Act(string input)
        {
            return LiteralParsers.BoolParser.Parse(input);
        }

        [Theory]
        [InlineData("true", true)]
        [InlineData("false", false)]
        public void parses_bool(string input, bool expectedValue)
        {
            var result = Act(input);
            
            result.Should().Be(expectedValue);
        }
    }
}
