using System;
using Manisero.DSLExecutor.Parser.SampleDSL.ExpressionGeneration.FunctionExpressionGeneration;
using Manisero.DSLExecutor.Parser.SampleDSL.Parsing.Tokens;

namespace Manisero.DSLExecutor.Parser.SampleDSL.Tests.ExpressionGeneration.FunctionExpressionGeneration
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
