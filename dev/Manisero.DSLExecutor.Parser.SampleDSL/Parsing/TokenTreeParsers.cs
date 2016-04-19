using System.Linq;
using Manisero.DSLExecutor.Parser.SampleDSL.Parsing.Tokens;
using Sprache;

namespace Manisero.DSLExecutor.Parser.SampleDSL.Parsing
{
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
