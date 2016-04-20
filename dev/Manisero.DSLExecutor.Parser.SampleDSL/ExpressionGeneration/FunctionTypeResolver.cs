using System;
using Manisero.DSLExecutor.Parser.SampleDSL.Parsing.Tokens;

namespace Manisero.DSLExecutor.Parser.SampleDSL.ExpressionGeneration
{
    public interface IFunctionTypeResolver
    {
        Type Resolve(FunctionCall functionCall);
    }

    public class FunctionTypeResolver : IFunctionTypeResolver
    {
        public Type Resolve(FunctionCall functionCall)
        {
            throw new NotImplementedException();
        }
    }
}
