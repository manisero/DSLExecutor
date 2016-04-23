using System;
using System.Collections.Generic;
using System.Linq;
using Manisero.DSLExecutor.Parser.SampleDSL.Parsing.Tokens;
using Manisero.DSLExecutor.Utilities;

namespace Manisero.DSLExecutor.Parser.SampleDSL.ExpressionGeneration.FunctionExpressionGeneration.FunctionTypeResolvers
{
    public class TypeSamplesAndSuffixConventionBasedFunctionMetadataResolver : IFunctionMetadataResolver
    {
        private readonly IEnumerable<Type> _functionTypeSamples;
        private readonly IFunctionContractProvider _functionContractProvider;

        private readonly Lazy<IDictionary<string, FunctionMetadata>> _functionNameToMetadataMap;

        public TypeSamplesAndSuffixConventionBasedFunctionMetadataResolver(IEnumerable<Type> functionTypeSamples,
                                                                           IFunctionContractProvider functionContractProvider)
        {
            _functionTypeSamples = functionTypeSamples;
            _functionContractProvider = functionContractProvider;

            _functionNameToMetadataMap = new Lazy<IDictionary<string, FunctionMetadata>>(InitializeFunctionNameToMetadataMap);
        }

        public FunctionMetadata Resolve(FunctionCall functionCall)
        {
            FunctionMetadata result;

            return !_functionNameToMetadataMap.Value.TryGetValue(functionCall.FunctionName, out result)
                       ? null
                       : result;
        }

        private IDictionary<string, FunctionMetadata> InitializeFunctionNameToMetadataMap()
        {
            var result = new Dictionary<string, FunctionMetadata>();

            var assembliesToScan = _functionTypeSamples.Select(x => x.Assembly).Distinct();
            var typesToScan = assembliesToScan.SelectMany(x => x.GetTypes());

            foreach (var type in typesToScan)
            {
                var functionContract = _functionContractProvider.Provide(type);

                if (functionContract == null)
                {
                    continue;
                }

                var functionName = GetFunctionName(type);
                var functionMetadata = new FunctionMetadata
                    {
                        FunctionType = type,
                        FunctionContract = functionContract
                    };

                result.Add(functionName, functionMetadata);
            }

            return result;
        }

        private string GetFunctionName(Type functionType)
        {
            const string functionTypeNameSuffix = "Function";

            return functionType.Name.EndsWith(functionTypeNameSuffix)
                       ? functionType.Name.Substring(0, functionType.Name.Length - functionTypeNameSuffix.Length)
                       : functionType.Name;
        }
    }
}
