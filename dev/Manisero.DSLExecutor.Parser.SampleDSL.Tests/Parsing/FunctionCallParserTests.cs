using System.Linq;
using FluentAssertions;
using Manisero.DSLExecutor.Parser.SampleDSL.Parsing;
using Manisero.DSLExecutor.Parser.SampleDSL.Parsing.Tokens;
using Sprache;
using Xunit;

namespace Manisero.DSLExecutor.Parser.SampleDSL.Tests.Parsing
{
    public class FunctionCallParserTests
    {
        private FunctionCall Act(string input)
        {
            return Parsers.FunctionCallParser.Parse(input);
        }

        [Theory]
        [InlineData("f()", "f")]
        [InlineData("function()", "function")]
        public void parses_name_and_empty_arguments(string input, string expectedFunctionName)
        {
            var result = Act(input);

            result.Should().NotBeNull();
            result.FunctionName.Should().Be(expectedFunctionName);
            result.Arguments.Should().NotBeNull();
            result.Arguments.Should().BeEmpty();
        }

        [Theory]
        [InlineData("f('a')", "a")]
        [InlineData("f('1')", "1")]
        public void parses_literal_argument(string input, string expectedArgumentValue)
        {
            var result = Act(input);
            
            result.Arguments.Select(x => ((Literal)x).Value).ShouldAllBeEquivalentTo(new[] { expectedArgumentValue });
        }

        [Theory]
        [InlineData("f1(f2())", "f2")]
        [InlineData("f(function('a'))", "function")]
        public void parses_function_call_argument(string input, string expectedFunctionName)
        {
            var result = Act(input);
            
            result.Arguments.Select(x => ((FunctionCall)x).FunctionName).ShouldAllBeEquivalentTo(new[] { expectedFunctionName });
        }

        [Theory]
        [InlineData("f1('a' f2())", "a", "f2")]
        public void parses_multiple_arguments(string input, string expectedArgumentValue, string expectedFunctionName)
        {
            var result = Act(input);
            
            ((Literal)result.Arguments.ElementAt(0)).Value.Should().Be(expectedArgumentValue);
            ((FunctionCall)result.Arguments.ElementAt(1)).FunctionName.Should().Be(expectedFunctionName);
        }
    }
}
