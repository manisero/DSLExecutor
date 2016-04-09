using FluentAssertions;
using Sprache;
using Xunit;

namespace Manisero.DSLExecutor.Parser.SampleDSL.Tests
{
    public class Literal : IFunctionArgumentToken
    {
        public string Value { get; set; }
    }

    public class LiteralParserContainer
    {
        public static readonly Parser<Literal> LiteralParser = (from startQuote in Parse.Char('"')
                                                                from value in Parse.CharExcept('"').Many().Text()
                                                                from endQuote in Parse.Char('"')
                                                                select new Literal
                                                                    {
                                                                        Value = value
                                                                    }).Token();
    }

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
            var result = LiteralParserContainer.LiteralParser.Parse(input);

            result.Should().NotBeNull();
            result.Value.Should().Be(expectedValue);
        }
    }
}
