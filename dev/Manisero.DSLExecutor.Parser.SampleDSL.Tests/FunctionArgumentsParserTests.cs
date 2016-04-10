using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Manisero.DSLExecutor.Parser.SampleDSL.Tokens;
using Sprache;
using Xunit;

namespace Manisero.DSLExecutor.Parser.SampleDSL.Tests
{
    public class FunctionArgumentsParserTests
    {
        private IEnumerable<IFunctionArgumentToken> Act(string input)
        {
            return Parsers.FunctionArgumentsParser.Value.Parse(input);
        }

        [Theory]
        [InlineData("\"a\"", "a")]
        [InlineData("\"1\"", "1")]
        public void parses_literal_argument(string input, string expectedArgumentValue)
        {
            var result = Act(input);

            result.Should().NotBeNull();
            result.Select(x => ((Literal)x).Value).ShouldAllBeEquivalentTo(new[] { expectedArgumentValue });
        }

        [Theory]
        [InlineData("f()", "f")]
        [InlineData("f(\"1\" \"2\")", "f")]
        public void parses_function_call_argument(string input, string expectedFunctionName)
        {
            var result = Act(input);

            result.Should().NotBeNull();
            result.Select(x => ((FunctionCall)x).FunctionName).ShouldAllBeEquivalentTo(new[] { expectedFunctionName });
        }

        [Theory]
        [InlineData("\"1\" f()", "1", "f")]
        public void parses_multiple_arguments(string input, string expectedArgumentValue, string expectedFunctionName)
        {
            var result = Act(input);

            result.Should().NotBeNull();
            ((Literal)result.ElementAt(0)).Value.Should().Be(expectedArgumentValue);
            ((FunctionCall)result.ElementAt(1)).FunctionName.Should().Be(expectedFunctionName);
        }
    }
}
