using System;
using System.Collections.Generic;
using FluentAssertions;
using Manisero.DSLExecutor.Runtime.FunctionExecution.FunctionHandlerResolution.FunctionHandlerTypeResolvers;
using Manisero.DSLExecutor.Tests.TestsDomain;
using Xunit;

namespace Manisero.DSLExecutor.Tests.Runtime.FunctionExecution.FunctionHandlerResolution.FunctionHandlerTypeResolvers
{
    public class DictionarySourcedFunctionHandlerTypeResolverTests
    {
        private Type Act(Type functionType, IDictionary<Type, Type> functionTypeToHandlerTypeMap)
        {
            var resolver = new DictionarySourcedFunctionHandlerTypeResolver(functionTypeToHandlerTypeMap);

            return resolver.Resolve(functionType);
        }

        [Fact]
        public void known_function_type___resolver_type()
        {
            var map = new Dictionary<Type, Type>
                {
                    [typeof(FunctionWithoutParameters)] = typeof(object),
                    [typeof(FunctionWithParameters)] = typeof(int)
                };

            var result = Act(typeof(FunctionWithParameters), map);

            result.Should().Be(typeof(int));
        }

        [Fact]
        public void unknown_function_type___null()
        {
            var map = new Dictionary<Type, Type>
                {
                    [typeof(FunctionWithoutParameters)] = typeof(object)
                };

            var result = Act(typeof(FunctionWithParameters), map);

            result.Should().BeNull();
        }
    }
}
