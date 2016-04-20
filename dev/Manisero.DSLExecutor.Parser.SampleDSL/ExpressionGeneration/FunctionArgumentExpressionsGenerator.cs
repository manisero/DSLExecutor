using System;
using System.Collections.Generic;
using Manisero.DSLExecutor.Domain.ExpressionsDomain;
using Manisero.DSLExecutor.Parser.SampleDSL.Parsing.Tokens;

namespace Manisero.DSLExecutor.Parser.SampleDSL.ExpressionGeneration
{
    public interface IFunctionArgumentExpressionsGenerator
    {
        IEnumerable<IExpression> Generate(IEnumerable<IFunctionArgumentToken> functionArgumentTokens, FunctionMetadata functionMetadata);
    }

    public class FunctionArgumentExpressionsGenerator : IFunctionArgumentExpressionsGenerator
    {
        public IEnumerable<IExpression> Generate(IEnumerable<IFunctionArgumentToken> functionArgumentTokens, FunctionMetadata functionMetadata)
        {
            throw new NotImplementedException();
        }
    }
}
