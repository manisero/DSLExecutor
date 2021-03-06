﻿using FluentAssertions;
using Manisero.DSLExecutor.Parser.SampleDSL.Parsing;
using Manisero.DSLExecutor.Parser.SampleDSL.Parsing.Tokens;
using Sprache;
using Xunit;

namespace Manisero.DSLExecutor.Parser.SampleDSL.Tests.Parsing.LiteralParsersTests
{
    public class LiteralParserTests
    {
        private Literal Act(string input)
        {
            return LiteralParsers.LiteralParser.Parse(input);
        }

        [Fact]
        public void parses_null()
        {
            var result = Act("null");

            result.Value.Should().BeNull();
        }

        [Theory]
        [InlineData("true", true)]
        public void parses_bool(string input, bool expectedValue)
        {
            var result = Act(input);

            result.Value.Should().BeOfType<bool>();
            result.Value.As<bool>().Should().Be(expectedValue);
        }

        [Theory]
        [InlineData("1.1", 1.1)]
        public void parses_double(string input, double expectedValue)
        {
            var result = Act(input);

            result.Value.Should().BeOfType<double>();
            result.Value.As<double>().Should().Be(expectedValue);
        }

        [Theory]
        [InlineData("1", 1)]
        public void parses_int(string input, int expectedValue)
        {
            var result = Act(input);

            result.Value.Should().BeOfType<int>();
            result.Value.As<int>().Should().Be(expectedValue);
        }

        [Theory]
        [InlineData("'a'", "a")]
        public void parses_string(string input, string expectedValue)
        {
            var result = Act(input);

            result.Value.Should().BeOfType<string>();
            result.Value.As<string>().Should().Be(expectedValue);
        }
    }
}
