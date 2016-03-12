using FluentAssertions;
using Manisero.DSLExecutor.Extensions;
using System;
using Xunit;

namespace Manisero.DSLExecutor.Tests.Extensions.TypeExtensionsTests
{
    public class GetGenericTypeDefinitionImplementationTests
    {
        private Type Act(Type type, Type definition)
        {
            return type.GetGenericTypeDefinitionImplementation(definition);
        }

        [Theory]
        [InlineData(typeof(IGenericInterfaceDefinition<int>), typeof(IGenericInterfaceDefinition<>))]
        [InlineData(typeof(GenericClassDefinition<int>), typeof(GenericClassDefinition<>))]
        [InlineData(typeof(Interface_Implementing_GenericInterfaceDefinition), typeof(IGenericInterfaceDefinition<>))]
        [InlineData(typeof(Class_Implementing_GenericInterfaceDefinition), typeof(IGenericInterfaceDefinition<>))]
        [InlineData(typeof(Class_DerivingFrom_GenericClassDefinition), typeof(GenericClassDefinition<>))]
        [InlineData(typeof(ChildInterface_Of_Interface_Implementing_GenericInterfaceDefinition), typeof(IGenericInterfaceDefinition<>))]
        [InlineData(typeof(ChildClass_Of_Interface_Implementing_GenericInterfaceDefinition), typeof(IGenericInterfaceDefinition<>))]
        [InlineData(typeof(ChildClass_Of_Class_Implementing_GenericInterfaceDefinition), typeof(IGenericInterfaceDefinition<>))]
        [InlineData(typeof(ChildClass_Of_Class_DerivingFrom_GenericClassDefinition), typeof(GenericClassDefinition<>))]
        public void type_implementing_definition___definition_implementation(Type type, Type definition)
        {
            var result = Act(type, definition);

            result.ShouldBeEquivalentTo(definition.MakeGenericType(typeof(int)), $"{nameof(type)}: '{type}', {nameof(definition)}: '{definition}'.");
        }

        [Theory]
        [InlineData(typeof(object), typeof(IGenericInterfaceDefinition<>))]
        [InlineData(typeof(IGenericInterfaceDefinition<>), typeof(IGenericInterfaceDefinition<>))]
        [InlineData(typeof(object), typeof(GenericClassDefinition<>))]
        [InlineData(typeof(GenericClassDefinition<>), typeof(GenericClassDefinition<>))]
        public void type_not_implementing_definition___null(Type type, Type definition)
        {
            var result = Act(type, definition);

            result.Should().BeNull($"{nameof(type)}: '{type}', {nameof(definition)}: '{definition}'.");
        }
    }
}
