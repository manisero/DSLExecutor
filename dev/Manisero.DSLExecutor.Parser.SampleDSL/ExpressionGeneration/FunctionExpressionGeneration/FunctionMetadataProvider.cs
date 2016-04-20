using System;

namespace Manisero.DSLExecutor.Parser.SampleDSL.ExpressionGeneration.FunctionExpressionGeneration
{
    public interface IFunctionMetadataProvider
    {
        FunctionMetadata Provide(Type functionType);
    }

    public class FunctionMetadataProvider : IFunctionMetadataProvider
    {
        public FunctionMetadata Provide(Type functionType)
        {
            throw new NotImplementedException();
        }
    }
}
