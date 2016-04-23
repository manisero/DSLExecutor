using System;
using System.Collections.Generic;
using System.Linq;
using Manisero.DSLExecutor.Utilities;

namespace Manisero.DSLExecutor.Parser.SampleDSL.ExpressionGeneration.FunctionExpressionGeneration.MetadataResolution.FunctionNameToMetadataMapFactories
{
    public class TypeSamplesAndSuffixConventionBasedFunctionNameToMetadataMapFactory : IFunctionNameToMetadataMapFactory
    {
        private const string FunctionTypeNameSuffix = "Function";

        private readonly IEnumerable<Type> _functionTypeSamples;
        private readonly IFunctionContractProvider _functionContractProvider;

        public TypeSamplesAndSuffixConventionBasedFunctionNameToMetadataMapFactory(IEnumerable<Type> functionTypeSamples,
                                                                                   IFunctionContractProvider functionContractProvider)
        {
            _functionTypeSamples = functionTypeSamples;
            _functionContractProvider = functionContractProvider;
        }

        public IDictionary<string, FunctionMetadata> Create()
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
            return functionType.Name.EndsWith(FunctionTypeNameSuffix)
                       ? functionType.Name.Substring(0, functionType.Name.Length - FunctionTypeNameSuffix.Length)
                       : functionType.Name;
        }
    }
}
