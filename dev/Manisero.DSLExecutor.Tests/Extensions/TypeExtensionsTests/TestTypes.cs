namespace Manisero.DSLExecutor.Tests.Extensions.TypeExtensionsTests
{
    // Definition

    public interface IGenericInterfaceDefinition<T>
    {
    }

    // Definition implementations

    public interface Interface_Implementing_GenericInterfaceDefinition : IGenericInterfaceDefinition<int>
    {
    }

    public class Class_Implementing_GenericInterfaceDefinition : IGenericInterfaceDefinition<int>
    {
    }

    // Definition implementation children

    public interface ChildInterface_Of_Interface_Implementing_GenericInterfaceDefinition : Interface_Implementing_GenericInterfaceDefinition
    {
    }

    public class ChildClass_Of_Interface_Implementing_GenericInterfaceDefinition : Interface_Implementing_GenericInterfaceDefinition
    {
    }

    public class ChildClass_Of_Class_Implementing_GenericInterfaceDefinition : Class_Implementing_GenericInterfaceDefinition
    {
    }
}
