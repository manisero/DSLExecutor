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

        public static readonly Parser<FunctionArguments> FunctionArgumentsParser = null;

        public static readonly Parser<FunctionCall> FunctionCallParser = null;
    }
}
