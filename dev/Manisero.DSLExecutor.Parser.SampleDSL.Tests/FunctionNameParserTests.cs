using System;
using FluentAssertions;
using Sprache;
using Xunit;

namespace Manisero.DSLExecutor.Parser.SampleDSL.Tests
{
    public class FunctionNameParserTests
    {
        [Theory]
        [InlineData("a", "a")]
        [InlineData("a1", "a1")]
        public void parses_function_name(string input, string expectedResult)
        {
            var result = Parsers.FunctionNameParser.Parse(input);

            result.Should().Be(expectedResult);
        }

        [Fact]
        public void rejects_empty_input()
        {
            Action act = () => Parsers.FunctionNameParser.Parse("");

            act.ShouldThrow<Exception>();
        }
    }
}
