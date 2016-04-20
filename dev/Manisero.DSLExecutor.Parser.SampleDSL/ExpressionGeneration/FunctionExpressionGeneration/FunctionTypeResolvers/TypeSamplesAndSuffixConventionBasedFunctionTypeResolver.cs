using System;
using System.Collections.Generic;
using Manisero.DSLExecutor.Parser.SampleDSL.Parsing.Tokens;

namespace Manisero.DSLExecutor.Parser.SampleDSL.ExpressionGeneration.FunctionExpressionGeneration.FunctionTypeResolvers
{
    public class TypeSamplesAndSuffixConventionBasedFunctionTypeResolver : IFunctionTypeResolver
    {
        private readonly IEnumerable<Type> _functionTypeSamples;

        private readonly Lazy<IDictionary<string, Type>> _functionNameToTypeMap;

        public TypeSamplesAndSuffixConventionBasedFunctionTypeResolver(IEnumerable<Type> functionTypeSamples)
        {
            _functionTypeSamples = functionTypeSamples;

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

            foreach (var functionTypeSample in _functionTypeSamples)
            {
                // TODO: Scan sample's assembly for function types
                // TODO: Fill result with type names without "Function" suffix
            }

            return result;
        }
    }
}
