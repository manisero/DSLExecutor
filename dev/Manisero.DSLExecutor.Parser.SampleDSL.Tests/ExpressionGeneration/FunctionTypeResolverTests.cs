using System;
using Manisero.DSLExecutor.Parser.SampleDSL.ExpressionGeneration;
using Manisero.DSLExecutor.Parser.SampleDSL.Parsing.Tokens;

namespace Manisero.DSLExecutor.Parser.SampleDSL.Tests.ExpressionGeneration
{
    public class FunctionTypeResolverTests
    {
        private Type Act(FunctionCall functionCall)
        {
            var functionTypeResolver = new FunctionTypeResolver();

            return functionTypeResolver.Resolve(functionCall);
        }
    }
}
