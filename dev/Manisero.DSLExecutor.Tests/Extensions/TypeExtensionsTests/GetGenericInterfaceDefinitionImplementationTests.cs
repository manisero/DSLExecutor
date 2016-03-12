using FluentAssertions;
using Manisero.DSLExecutor.Extensions;
using System;
using Xunit;

namespace Manisero.DSLExecutor.Tests.Extensions.TypeExtensionsTests
{
    public class GetGenericInterfaceDefinitionImplementationTests
    {
        private Type Act(Type type, Type interfaceDefinition)
        {
            return type.GetGenericInterfaceDefinitionImplementation(interfaceDefinition);
        }

        [Theory]
        [InlineData(typeof(IGenericInterfaceDefinition<int>), typeof(IGenericInterfaceDefinition<>))]
        [InlineData(typeof(Interface_Implementing_GenericInterfaceDefinition), typeof(IGenericInterfaceDefinition<>))]
        [InlineData(typeof(Class_Implementing_GenericInterfaceDefinition), typeof(IGenericInterfaceDefinition<>))]
        [InlineData(typeof(ChildInterface_Of_Interface_Implementing_GenericInterfaceDefinition), typeof(IGenericInterfaceDefinition<>))]
        [InlineData(typeof(ChildClass_Of_Interface_Implementing_GenericInterfaceDefinition), typeof(IGenericInterfaceDefinition<>))]
        [InlineData(typeof(ChildClass_Of_Class_Implementing_GenericInterfaceDefinition), typeof(IGenericInterfaceDefinition<>))]
        public void type_implementing_definition___definition_implementation(Type type, Type definition)
        {
            var result = Act(type, definition);

            result.ShouldBeEquivalentTo(definition.MakeGenericType(typeof(int)), $"{nameof(type)}: '{type}', {nameof(definition)}: '{definition}'.");
        }

        [Theory]
        [InlineData(typeof(object), typeof(IGenericInterfaceDefinition<>))]
        [InlineData(typeof(IGenericInterfaceDefinition<>), typeof(IGenericInterfaceDefinition<>))]
        public void type_not_implementing_definition___null(Type type, Type definition)
        {
            var result = Act(type, definition);

            result.Should().BeNull($"{nameof(type)}: '{type}', {nameof(definition)}: '{definition}'.");
        }
    }
}
