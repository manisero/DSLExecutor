using System;
using System.Linq;
using Manisero.DSLExecutor.Domain.FunctionsDomain;
using Manisero.DSLExecutor.Extensions;

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
            var functionDefinitionImplementation = functionType.GetGenericInterfaceDefinitionImplementation(typeof(IFunction<>));

            if (functionDefinitionImplementation == null)
            {
                throw new ArgumentException($"{nameof(functionType)} must implement {typeof(IFunction<>)} interface.",
                                            nameof(functionType));
            }

            var parameters = functionType.GetProperties()
                                         .Select(x => new FunctionParameterMetadata
                                             {
                                                 Name = x.Name,
                                                 Type = x.PropertyType
                                             })
                                         .ToList();

            return new FunctionMetadata
                {
                    Parameters = parameters,
                    ResultType = functionDefinitionImplementation.GetGenericArguments()[0]
                };
        }
    }
}
