using System;
using System.Linq;
using FluentAssertions;
using Manisero.DSLExecutor.Parser.SampleDSL.Parsing;
using Manisero.DSLExecutor.Parser.SampleDSL.Parsing.Tokens;
using Sprache;
using Xunit;

namespace Manisero.DSLExecutor.Parser.SampleDSL.Tests.Parsing
{
    public class TokenTreeParserTests
    {
        private TokenTree Act(string input)
        {
            return Parsers.TokenTreeParser.Parse(input);
        }

        [Theory]
        [InlineData("f()", new[] { "f" })]
        [InlineData("f1() f2()", new[] { "f1", "f2" })]
        [InlineData("f1() f2() f3()", new[] { "f1", "f2", "f3" })]
        [InlineData("f1(f() 'a') f2()", new[] { "f1", "f2" })]
        public void parses_token_tree(string input, string[] expectedFunctionNames)
        {
            var result = Act(input);

            result.Should().NotBeNull();
            result.FunctionCalls.Should().NotBeNull();
            result.FunctionCalls.Select(x => x.FunctionName).ShouldAllBeEquivalentTo(expectedFunctionNames);
        }
        
        [Fact]
        public void rejects_empty_input()
        {
            Action act = () => Act("");

            act.ShouldThrow<Exception>();
        }
    }
}
