using System;
using System.Collections.Generic;
using System.Linq;
using Manisero.DSLExecutor.Parser.SampleDSL.Parsing.Tokens;
using Manisero.DSLExecutor.Utilities;

namespace Manisero.DSLExecutor.Parser.SampleDSL.ExpressionGeneration.FunctionExpressionGeneration.FunctionTypeResolvers
{
    public class TypeSamplesAndSuffixConventionBasedFunctionTypeResolver : IFunctionTypeResolver
    {
        private readonly IEnumerable<Type> _functionTypeSamples;
        private readonly IFunctionContractProvider _functionContractProvider;

        private readonly Lazy<IDictionary<string, Type>> _functionNameToTypeMap;

        public TypeSamplesAndSuffixConventionBasedFunctionTypeResolver(IEnumerable<Type> functionTypeSamples,
                                                                       IFunctionContractProvider functionContractProvider)
        {
            _functionTypeSamples = functionTypeSamples;
            _functionContractProvider = functionContractProvider;

            _functionNameToTypeMap = new Lazy<IDictionary<string, Type>>(InitializeFunctionNameToTypeMap);
        }

        public Type Resolve(FunctionCall functionCall)
        {
            Type result;

            return !_functionNameToTypeMap.Value.TryGetValue(functionCall.FunctionName, out result)
                       ? null
                       : result;
        }

        private IDictionary<string, Type> InitializeFunctionNameToTypeMap()
        {
            var result = new Dictionary<string, Type>();

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

                result.Add(functionName, type);
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
