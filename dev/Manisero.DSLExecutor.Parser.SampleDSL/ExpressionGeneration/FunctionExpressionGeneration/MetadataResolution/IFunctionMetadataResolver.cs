using System;
using System.Collections.Generic;
using Manisero.DSLExecutor.Parser.SampleDSL.Parsing.Tokens;

namespace Manisero.DSLExecutor.Parser.SampleDSL.ExpressionGeneration.FunctionExpressionGeneration.MetadataResolution
{
    public interface IFunctionMetadataResolver
    {
        FunctionMetadata Resolve(FunctionCall functionCall);
    }

    public class FunctionMetadataResolver : IFunctionMetadataResolver
    {
        private readonly Lazy<IDictionary<string, FunctionMetadata>> _functionNameToMetadataMap;

        public FunctionMetadataResolver(IFunctionNameToMetadataMapFactory functionNameToMetadataMapFactory)
        {
            _functionNameToMetadataMap = new Lazy<IDictionary<string, FunctionMetadata>>(functionNameToMetadataMapFactory.Create);
        }

        public FunctionMetadata Resolve(FunctionCall functionCall)
        {
            FunctionMetadata result;

            return !_functionNameToMetadataMap.Value.TryGetValue(functionCall.FunctionName, out result)
                       ? null
                       : result;
        }
    }
}
