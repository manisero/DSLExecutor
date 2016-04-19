using Manisero.DSLExecutor.Parser.SampleDSL.Parsing.Tokens;
using Sprache;

namespace Manisero.DSLExecutor.Parser.SampleDSL.Parsing
{
    public static class LiteralParsers
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

        public static readonly Parser<Literal> LiteralParser = (from startQuote in DelimiterParser
                                                                from value in ValueParser
                                                                from endQuote in DelimiterParser
                                                                select new Literal
                                                                    {
                                                                        Value = value
                                                                    }).Token();
    }
}
