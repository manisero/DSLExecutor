namespace Manisero.DSLExecutor.Tests.Extensions.TypeExtensionsTests
{
    // Definitions

    public interface IGenericInterfaceDefinition<T>
    {
    }

    public class GenericClassDefinition<T>
    {
    }

    // Definition implementations

    public interface Interface_Implementing_GenericInterfaceDefinition : IGenericInterfaceDefinition<int>
    {
    }

    public class Class_Implementing_GenericInterfaceDefinition : IGenericInterfaceDefinition<int>
    {
    }

    public class Class_DerivingFrom_GenericClassDefinition : GenericClassDefinition<int>
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

    public class ChildClass_Of_Class_DerivingFrom_GenericClassDefinition : Class_DerivingFrom_GenericClassDefinition
    {
    }
}
