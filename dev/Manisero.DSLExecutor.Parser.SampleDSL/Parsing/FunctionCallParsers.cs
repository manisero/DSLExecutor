using System.Collections.Generic;
using System.Linq;
using Manisero.DSLExecutor.Parser.SampleDSL.Parsing.Tokens;
using Sprache;

namespace Manisero.DSLExecutor.Parser.SampleDSL.Parsing
{
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
}
