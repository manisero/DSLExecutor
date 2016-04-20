using System.Collections.Generic;

namespace Manisero.DSLExecutor.Parser.SampleDSL.Parsing.Tokens
{
    public class TokenTree : IToken
    {
        public IList<FunctionCall> FunctionCalls { get; set; }
    }
}
