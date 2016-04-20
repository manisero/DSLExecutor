using System;
using System.Collections.Generic;

namespace Manisero.DSLExecutor.Parser.SampleDSL.ExpressionGeneration.FunctionExpressionGeneration
{
    public class FunctionMetadata
    {
        public string Name { get; set; }

        public IList<FunctionParameterMetadata> Parameters { get; set; }

        public Type ResultType { get; set; }
    }

    public class FunctionParameterMetadata
    {
        public string Name { get; set; }

        public Type Type { get; set; }
    }
}
