using System;
using System.Collections.Generic;
using System.Linq;
using Manisero.DSLExecutor.Parser.SampleDSL.Parsing.Tokens;
using Sprache;

namespace Manisero.DSLExecutor.Parser.SampleDSL.Parsing
{
    public static class Parsers
    {
        public static readonly Parser<Literal> LiteralParser = (from startQuote in Parse.Char('\'')
                                                                from value in Parse.CharExcept('\'').Many().Text()
                                                                from endQuote in Parse.Char('\'')
                                                                select new Literal
                                                                    {
                                                                        Value = value
                                                                    }).Token();

        public static readonly Parser<string> FunctionNameParser = Parse.LetterOrDigit.AtLeastOnce().Text().Token();

        public static readonly Lazy<Parser<IEnumerable<IFunctionArgumentToken>>> FunctionArgumentsParser = new Lazy<Parser<IEnumerable<IFunctionArgumentToken>>>(() => LiteralParser.Or<IFunctionArgumentToken>(FunctionCallParser)
                                                                                                                                                                                    .Many()
                                                                                                                                                                                    .Token());

        public static readonly Parser<FunctionCall> FunctionCallParser = (from name in FunctionNameParser
                                                                          from argumentsStart in Parse.Char('(')
                                                                          from arguments in FunctionArgumentsParser.Value
                                                                          from argumentsEnd in Parse.Char(')')
                                                                          select new FunctionCall
                                                                              {
                                                                                  FunctionName = name,
                                                                                  Arguments = arguments.ToList()
                                                                              }).Token();

        public static readonly Parser<TokenTree> TokenTreeParser = FunctionCallParser.AtLeastOnce()
                                                                                     .Select(x => new TokenTree
                                                                                         {
                                                                                             FunctionCalls = x.ToList()
                                                                                         })
                                                                                     .Token();
    }
}
