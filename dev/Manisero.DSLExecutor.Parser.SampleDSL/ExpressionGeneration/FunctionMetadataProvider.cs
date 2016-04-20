using System;

namespace Manisero.DSLExecutor.Parser.SampleDSL.ExpressionGeneration
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
