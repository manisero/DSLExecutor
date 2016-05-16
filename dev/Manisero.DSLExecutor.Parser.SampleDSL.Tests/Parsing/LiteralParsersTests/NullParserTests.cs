using FluentAssertions;
using Manisero.DSLExecutor.Parser.SampleDSL.Parsing;
using Sprache;
using Xunit;

namespace Manisero.DSLExecutor.Parser.SampleDSL.Tests.Parsing.LiteralParsersTests
{
    public class NullParserTests
    {
        private object Act(string input)
        {
            return LiteralParsers.NullParser.Parse(input);
        }

        [Fact]
        public void parses_null()
        {
            var result = Act("null");
            
            result.Should().Be(null);
        }
    }
}
