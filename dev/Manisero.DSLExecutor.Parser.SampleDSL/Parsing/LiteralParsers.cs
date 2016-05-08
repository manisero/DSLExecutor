using Manisero.DSLExecutor.Parser.SampleDSL.Parsing.Tokens;
using Sprache;

namespace Manisero.DSLExecutor.Parser.SampleDSL.Parsing
{
    public static class LiteralParsers
    {
        public static class StringParsers
        {
            private const char Delimiter = '\'';
            private const char EscapeMark = '\\';

            public static readonly Parser<char> DelimiterParser = Parse.Char(Delimiter);
            public static readonly Parser<char> EscapeMarkParser = Parse.Char(EscapeMark);
            public static readonly Parser<char> SpecialCharacterParser = DelimiterParser.Or(EscapeMarkParser);
            public static readonly Parser<char> RegularCharacterParser = Parse.AnyChar.Except(SpecialCharacterParser);

            public static readonly Parser<char> EscapedCharacterParser = from escapeMark in EscapeMarkParser
                                                                         from value in SpecialCharacterParser
                                                                         select value;

            public static readonly Parser<string> ValueParser = RegularCharacterParser.Or(EscapedCharacterParser).Many().Text();

            public static readonly Parser<string> StringParser = (from startQuote in DelimiterParser
                                                                  from value in ValueParser
                                                                  from endQuote in DelimiterParser
                                                                  select value).Token();
        }

        public static readonly Parser<int> IntParser = Parse.Digit.AtLeastOnce().Text().Select(int.Parse).Token();

        public static readonly Parser<Literal> LiteralParser = IntParser.Select(x => (object)x)
                                                                        .Or(StringParsers.StringParser.Select(x => (object)x))
                                                                        .Select(x => new Literal
                                                                            {
                                                                                Value = x
                                                                            });
    }
}
