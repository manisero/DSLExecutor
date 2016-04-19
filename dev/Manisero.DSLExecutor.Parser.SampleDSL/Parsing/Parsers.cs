using System.Collections.Generic;
using System.Linq;
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

    public static class FunctionCallParsers
    {
        private const char ArgumentListStart = '(';
        private const char ArgumentListEnd = ')';

        public static readonly Parser<string> FunctionNameParser = Parse.LetterOrDigit.AtLeastOnce().Text();

        public static readonly Parser<IEnumerable<IFunctionArgumentToken>> FunctionArgumentsParser = LiteralParsers.LiteralParser
                                                                                                                   .Or<IFunctionArgumentToken>(Parse.Ref(() => FunctionCallParser))
                                                                                                                   .Many();

        public static readonly Parser<FunctionCall> FunctionCallParser = (from name in FunctionNameParser
                                                                          from argumentsStart in Parse.Char(ArgumentListStart)
                                                                          from arguments in Parse.Ref(() => FunctionArgumentsParser)
                                                                          from argumentsEnd in Parse.Char(ArgumentListEnd)
                                                                          select new FunctionCall
                                                                              {
                                                                                  FunctionName = name,
                                                                                  Arguments = arguments.ToList()
                                                                              }).Token();
    }

    public static class TokenTreeParsers
    {
        public static readonly Parser<TokenTree> TokenTreeParser = FunctionCallParsers.FunctionCallParser
                                                                                      .AtLeastOnce()
                                                                                      .Select(x => new TokenTree
                                                                                          {
                                                                                              FunctionCalls = x.ToList()
                                                                                          })
                                                                                      .Token();
    }
}
