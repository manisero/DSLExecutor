using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Manisero.DSLExecutor.Parser.SampleDSL.Parsing;
using Manisero.DSLExecutor.Parser.SampleDSL.Parsing.Tokens;
using Sprache;
using Xunit;

namespace Manisero.DSLExecutor.Parser.SampleDSL.Tests.Parsing
{
    public class FunctionArgumentsParserTests
    {
        private IEnumerable<IFunctionArgumentToken> Act(string input)
        {
            return Parsers.FunctionArgumentsParser.Value.Parse(input);
        }

        [Fact]
        public void parses_empty_arguments()
        {
            var result = Act("");

            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Theory]
        [InlineData("'a'", "a")]
        [InlineData("'1'", "1")]
        public void parses_literal_argument(string input, string expectedArgumentValue)
        {
            var result = Act(input);
            
            result.Select(x => ((Literal)x).Value).ShouldAllBeEquivalentTo(new[] { expectedArgumentValue });
        }

        [Theory]
        [InlineData("f()", "f")]
        [InlineData("f('1' '2')", "f")]
        public void parses_function_call_argument(string input, string expectedFunctionName)
        {
            var result = Act(input);
            
            result.Select(x => ((FunctionCall)x).FunctionName).ShouldAllBeEquivalentTo(new[] { expectedFunctionName });
        }

        [Theory]
        [InlineData("'1' f()", "1", "f")]
        public void parses_multiple_arguments(string input, string expectedArgumentValue, string expectedFunctionName)
        {
            var result = Act(input);
            
            ((Literal)result.ElementAt(0)).Value.Should().Be(expectedArgumentValue);
            ((FunctionCall)result.ElementAt(1)).FunctionName.Should().Be(expectedFunctionName);
        }
    }
}
