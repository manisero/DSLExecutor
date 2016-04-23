using System.Collections.Generic;

namespace Manisero.DSLExecutor.Parser.SampleDSL.ExpressionGeneration.FunctionExpressionGeneration.MetadataResolution
{
    public interface IFunctionNameToMetadataMapFactory
    {
        IDictionary<string, FunctionMetadata> Create();
    }
}
