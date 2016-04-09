using System.Collections.Generic;

namespace Manisero.DSLExecutor.Parser.SampleDSL.Tokens
{
    public class FunctionCall : IFunctionArgumentToken
    {
        public string FunctionName { get; set; }

        public IList<IFunctionArgumentToken> Arguments { get; set; }
    }
}
