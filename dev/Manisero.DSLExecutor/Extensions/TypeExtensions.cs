using System;

namespace Manisero.DSLExecutor.Extensions
{
    public static class TypeExtensions
    {
        public static Type GetGenericTypeDefinitionImplementation(this Type type, Type definition)
        {
            if (definition.IsInterface)
            {
                return type.GetGenericInterfaceDefinitionImplementation(definition);
            }
            else
            {
                return type.GetGenericClassDefinitionImplementation(definition);
            }
        }

        private static Type GetGenericInterfaceDefinitionImplementation(this Type type, Type interfaceDefinition)
        {
            if (type.IsInterface && type.IsConstructedGenericType && type.GetGenericTypeDefinition() == interfaceDefinition)
            {
                return type;
            }

            var interfaces = type.GetInterfaces();

            foreach (var @interface in interfaces)
            {
                if (@interface.IsGenericType && @interface.GetGenericTypeDefinition() == interfaceDefinition)
                {
                    return @interface;
                }
            }

            return null;
        }

        private static Type GetGenericClassDefinitionImplementation(this Type type, Type classDefinition)
        {
            if (type == typeof(object))
            {
                return null;
            }

            if (type.IsConstructedGenericType && type.GetGenericTypeDefinition() == classDefinition)
            {
                return type;
            }

            return type.BaseType.GetGenericClassDefinitionImplementation(classDefinition);
        }
    }
}
