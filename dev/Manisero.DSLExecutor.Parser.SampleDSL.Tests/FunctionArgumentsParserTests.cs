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
            return Parsers.FunctionArgumentsParser.Parse(input);
        }

        [Theory]
        [InlineData("\"1\" \"2\"", new[] { "1", "2" })]
        public void parses_literal_arguments(string input, string[] expectedArgumentValues)
        {
            var result = Act(input);

            result.Should().NotBeNull();
            result.Select(x => ((Literal)x).Value).ShouldAllBeEquivalentTo(expectedArgumentValues);
        }
    }
}
