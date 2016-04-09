using System.Linq;
using FluentAssertions;
using Manisero.DSLExecutor.Parser.SampleDSL.Tokens;
using Sprache;
using Xunit;

namespace Manisero.DSLExecutor.Parser.SampleDSL.Tests
{
    public class FunctionCallParserTests
    {
        private FunctionCall Act(string input)
        {
            return Parsers.FunctionCallParser.Parse(input);
        }

        [Theory]
        [InlineData("function()", "function")]
        public void parses_name_and_empty_arguments(string input, string expectedName)
        {
            var result = Act(input);

            result.Should().NotBeNull();
            result.FunctionName.Should().Be(expectedName);
            result.Arguments.Should().BeEmpty();
        }

        [Theory]
        [InlineData("f(\"1\" \"2\")", new[] { "1", "2" })]
        public void parses_literal_arguments(string input, string[] expectedArgumentValues)
        {
            var result = Act(input);

            result.Should().NotBeNull();
            result.Arguments.Select(x => ((Literal)x).Value).ShouldAllBeEquivalentTo(expectedArgumentValues);
        }
    }
}
