using System;

namespace Manisero.DSLExecutor.Utilities
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
