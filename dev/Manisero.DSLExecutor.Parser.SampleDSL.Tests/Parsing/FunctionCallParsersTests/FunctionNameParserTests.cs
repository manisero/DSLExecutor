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
        [InlineData("_")]
        [InlineData("-")]
        public void rejects_characters_othen_than_letter_or_digit(string input)
        {
            Action act = () => Act(input);

            act.ShouldThrow<Exception>();
        }

        [Theory]
        [InlineData("1a")]
        [InlineData("345fun")]
        public void rejects_digit_as_first_character(string input)
        {
            Action act = () => Act(input);

            act.ShouldThrow<Exception>();
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
