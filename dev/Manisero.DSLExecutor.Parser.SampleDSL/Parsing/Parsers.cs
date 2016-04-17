using System.Collections.Generic;
using System.Linq;
using Manisero.DSLExecutor.Parser.SampleDSL.Parsing.Tokens;
using Sprache;

namespace Manisero.DSLExecutor.Parser.SampleDSL.Parsing
{
    public static class Parsers
    {
        // Literal
        private const char LiteralDelimiter = '\'';
        private const char EscapeMark = '\\';

        public static readonly Parser<char> LiteralDelimiterParser = Parse.Char(LiteralDelimiter);
        public static readonly Parser<char> EscapeMarkParser = Parse.Char(EscapeMark);
        public static readonly Parser<char> SpecialCharacterParser = LiteralDelimiterParser.Or(EscapeMarkParser);

        public static readonly Parser<char> RegularCharacterParser = Parse.AnyChar.Except(SpecialCharacterParser);
        public static readonly Parser<char> EscapedCharacterParser = (from escapeMark in EscapeMarkParser
                                                                      from value in SpecialCharacterParser
                                                                      select value);

        public static readonly Parser<string> LiteralValueParser = RegularCharacterParser.Or(EscapedCharacterParser).Many().Text();
        public static readonly Parser<Literal> LiteralParser = (from startQuote in LiteralDelimiterParser
                                                                from value in LiteralValueParser
                                                                from endQuote in LiteralDelimiterParser
                                                                select new Literal
                                                                    {
                                                                        Value = value
                                                                    }).Token();

        // FunctionCall
        private const char ArgumentListStart = '(';
        private const char ArgumentListEnd = ')';

        public static readonly Parser<string> FunctionNameParser = Parse.LetterOrDigit.AtLeastOnce().Text();
        public static readonly Parser<IEnumerable<IFunctionArgumentToken>> FunctionArgumentsParser = LiteralParser.Or<IFunctionArgumentToken>(Parse.Ref(() => FunctionCallParser))
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

        // TokenTree
        public static readonly Parser<TokenTree> TokenTreeParser = FunctionCallParser.AtLeastOnce()
                                                                                     .Select(x => new TokenTree
                                                                                         {
                                                                                             FunctionCalls = x.ToList()
                                                                                         })
                                                                                     .Token();
    }
}
