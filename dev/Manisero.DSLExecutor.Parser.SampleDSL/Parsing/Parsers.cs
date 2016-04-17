using System;
using System.Collections.Generic;
using System.Linq;
using Manisero.DSLExecutor.Parser.SampleDSL.Parsing.Tokens;
using Sprache;

namespace Manisero.DSLExecutor.Parser.SampleDSL.Parsing
{
    public static class Parsers
    {
        // Literal
        private const char EscapingCharacter = '\\';
        private const char LiteralDelimiter = '\'';

        public static readonly Parser<char> EscapingCharacterParser = Parse.Char(EscapingCharacter);
        public static readonly Parser<char> LiteralDelimiterParser = Parse.Char(LiteralDelimiter);
        public static readonly Parser<char> SpecialCharacterParser = EscapingCharacterParser.Or(LiteralDelimiterParser);
        //public static readonly Parser<char> RegularCharacterParser = Parse.AnyChar. EscapingCharacterParser.Or(LiteralDelimiterParser);
        public static readonly Parser<string> LiteralValueParser = Parse.CharExcept('\'').Many().Text();

        public static readonly Parser<Literal> LiteralParser = (from startQuote in LiteralDelimiterParser
                                                                from value in LiteralValueParser
                                                                from endQuote in LiteralDelimiterParser
                                                                select new Literal
                                                                    {
                                                                        Value = value
                                                                    }).Token();

        // FunctionCall
        public static readonly Parser<string> FunctionNameParser = Parse.LetterOrDigit.AtLeastOnce().Text();

        public static readonly Lazy<Parser<IEnumerable<IFunctionArgumentToken>>> FunctionArgumentsParser = new Lazy<Parser<IEnumerable<IFunctionArgumentToken>>>(() => LiteralParser.Or<IFunctionArgumentToken>(FunctionCallParser)
                                                                                                                                                                                    .Many());

        public static readonly Parser<FunctionCall> FunctionCallParser = (from name in FunctionNameParser
                                                                          from argumentsStart in Parse.Char('(')
                                                                          from arguments in FunctionArgumentsParser.Value
                                                                          from argumentsEnd in Parse.Char(')')
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
