using System;
using System.Collections.Generic;

namespace Manisero.DSLExecutor.Utilities
{
    public class FunctionMetadata
    {
        public IList<FunctionParameterMetadata> Parameters { get; set; }

        public Type ResultType { get; set; }
    }

    public class FunctionParameterMetadata
    {
        public string Name { get; set; }

        public Type Type { get; set; }
    }
}
