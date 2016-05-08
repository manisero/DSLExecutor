using System;
using FluentAssertions;
using Manisero.DSLExecutor.Parser.SampleDSL.Parsing;
using Sprache;
using Xunit;

namespace Manisero.DSLExecutor.Parser.SampleDSL.Tests.Parsing
{
    public class FunctionNameParserTests
    {
        private string Act(string input)
        {
            return FunctionCallParsers.FunctionNameParser.Parse(input);
        }

        [Theory]
        [InlineData("a", "a")]
        [InlineData("a1", "a1")]
        public void parses_function_name(string input, string expectedResult)
        {
            var result = Act(input);

            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("a(", "a")]
        public void ignores_opening_bracket(string input, string expectedResult)
        {
            var result = Act(input);

            result.Should().Be(expectedResult);
        }

        [Fact]
        public void rejects_empty_input()
        {
            Action act = () => Act("");

            act.ShouldThrow<Exception>();
        }
    }
}
