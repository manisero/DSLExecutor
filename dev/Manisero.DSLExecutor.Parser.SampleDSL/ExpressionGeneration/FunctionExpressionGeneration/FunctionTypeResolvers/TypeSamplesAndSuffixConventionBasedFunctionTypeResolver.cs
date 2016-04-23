using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Manisero.DSLExecutor.Domain.FunctionsDomain;
using Manisero.DSLExecutor.Extensions;
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

            var assembliesToScan = _functionTypeSamples.Select(x => x.Assembly).Distinct();
            var typesToScan = assembliesToScan.SelectMany(x => x.GetTypes());

            foreach (var type in typesToScan)
            {
                var functionDefinitionImplementation = type.GetGenericInterfaceDefinitionImplementation(typeof(IFunction<>));

                if (functionDefinitionImplementation == null)
                {
                    continue;
                }

                // TODO: Fill result with type names without "Function" suffix
            }

            return result;
        }
    }
}
