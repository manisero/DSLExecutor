using System;
using System.Linq;
using Manisero.DSLExecutor.Domain.FunctionsDomain;
using Manisero.DSLExecutor.Extensions;

namespace Manisero.DSLExecutor.Utilities
{
    public interface IFunctionContractProvider
    {
        FunctionContract Provide(Type functionType);
    }

    public class FunctionContractProvider : IFunctionContractProvider
    {
        public FunctionContract Provide(Type functionType)
        {
            var functionDefinitionImplementation = functionType.GetGenericInterfaceDefinitionImplementation(typeof(IFunction<>));

            if (functionDefinitionImplementation == null)
            {
                return null;
            }

            var parameters = functionType.GetProperties()
                                         .Select(x => new FunctionParameterMetadata
                                             {
                                                 Name = x.Name,
                                                 Type = x.PropertyType
                                             })
                                         .ToList();

            return new FunctionContract
                {
                    Parameters = parameters,
                    ResultType = functionDefinitionImplementation.GetGenericArguments()[0]
                };
        }
    }
}
