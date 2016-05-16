using System.Globalization;
using System.Linq;
using Manisero.DSLExecutor.Parser.SampleDSL.Parsing.Tokens;
using Sprache;

namespace Manisero.DSLExecutor.Parser.SampleDSL.Parsing
{
    public static class LiteralParsers
    {
        public static readonly Parser<int> IntParser = Parse.Digit.AtLeastOnce().Text().Select(int.Parse).Token();

        public static class DoubleParsers
        {
            private const char Separator = '.';
            private static readonly CultureInfo Culture = CultureInfo.InvariantCulture;

            public static readonly Parser<double> DoubleParser = (from integerPart in Parse.Digit.AtLeastOnce()
                                                                  from separator in Parse.Char(Separator).Select(x => new[] { x })
                                                                  from fractionalPart in Parse.Digit.AtLeastOnce()
                                                                  select new string(integerPart.Concat(separator)
                                                                                               .Concat(fractionalPart)
                                                                                               .ToArray())
                                                                 ).Select(x => double.Parse(x, Culture))
                                                                  .Token();
        }

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

        public static readonly Parser<object> NullParser = Parse.String("null").Select(x => (object)null).Token();

        public static readonly Parser<Literal> LiteralParser = NullParser.Or(DoubleParsers.DoubleParser.Select(x => (object)x))
                                                                         .Or(IntParser.Select(x => (object)x))
                                                                         .Or(StringParsers.StringParser.Select(x => (object)x))
                                                                         .Select(x => new Literal
                                                                             {
                                                                                 Value = x
                                                                             });
    }
}
