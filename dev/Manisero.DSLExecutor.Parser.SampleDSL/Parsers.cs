using System.Collections.Generic;
using System.Linq;
using Manisero.DSLExecutor.Parser.SampleDSL.Tokens;
using Sprache;

namespace Manisero.DSLExecutor.Parser.SampleDSL
{
    public static class Parsers
    {
        public static readonly Parser<Literal> LiteralParser = (from startQuote in Parse.Char('"')
                                                                from value in Parse.CharExcept('"').Many().Text()
                                                                from endQuote in Parse.Char('"')
                                                                select new Literal
                                                                    {
                                                                        Value = value
                                                                    }).Token();

        public static readonly Parser<string> FunctionNameParser = Parse.LetterOrDigit.AtLeastOnce().Text().Token();

        public static readonly Parser<IEnumerable<IFunctionArgumentToken>> FunctionArgumentsParser = (from literals in LiteralParser.Many()
                                                                                                      select literals).Token();

        public static readonly Parser<FunctionCall> FunctionCallParser = (from name in FunctionNameParser
                                                                          from argumentsStart in Parse.Char('(')
                                                                          from arguments in FunctionArgumentsParser
                                                                          from argumentsEnd in Parse.Char(')')
                                                                          select new FunctionCall
                                                                              {
                                                                                  FunctionName = name,
                                                                                  Arguments = arguments.ToList()
                                                                              }).Token();
    }
}
