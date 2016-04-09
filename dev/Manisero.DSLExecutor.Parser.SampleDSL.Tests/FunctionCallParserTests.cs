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
        [InlineData("add(\"1\" \"2\")", "add", new[] { "1", "2" })]
        public void parses_function_call(string input, string expectedName, string[] expectedArguments)
        {
            var result = Act(input);

            result.Should().NotBeNull();
            result.FunctionName.Should().Be(expectedName);
            result.Arguments.ShouldAllBeEquivalentTo(expectedArguments);
        }
    }
}
