using System;
using System.Collections.Generic;

namespace Manisero.DSLExecutor.Parser.SampleDSL.ExpressionGeneration.FunctionExpressionGeneration
{
    public class FunctionMetadata
    {
        public string Name { get; set; }

        public IDictionary<string, Type> Parameters { get; set; }

        public Type ResultType { get; set; }
    }
}
