using System;
using System.Collections.Generic;
using Manisero.DSLExecutor.Library.Math;
using Manisero.DSLExecutor.Parser.SampleDSL;

namespace Manisero.DSLExecutor.WebApp.Application
{
    public class DSLProcessorInput
    {
        public string DSL { get; set; }
    }

    public class DSLProcessorOutput
    {
        public string Result { get; set; }
    }

    public class DSLProcessor
    {
        private readonly Lazy<ISampleDSLParser> _parser = new Lazy<ISampleDSLParser>(InitializeParser);
        private readonly Lazy<IDSLExecutor> _dslExecutor = new Lazy<IDSLExecutor>(InitializeDSLExecutor);

        public DSLProcessorOutput Process(DSLProcessorInput input)
        {
            string result;

            try
            {
                var expression = _parser.Value.Parse(input.DSL);
                var expressionResult = _dslExecutor.Value.ExecuteExpression(expression);

                result = expressionResult.ToString();
            }
            catch (Exception ex)
            {
                result = "ERROR: " + ex.Message;
            }

            return new DSLProcessorOutput
                {
                    Result = result
                };
        }

        private static ISampleDSLParser InitializeParser()
        {
            var functionTypeSamples = new[] { typeof(AddFunction) };

            return new SampleDSLParser(functionTypeSamples);
        }

        private static IDSLExecutor InitializeDSLExecutor()
        {
            var functionTypeToHandlerTypeMap = new Dictionary<Type, Type>
                {
                    [typeof(AddFunction)] = typeof(AddFunctionHandler)
                };

            return new DSLExecutor(functionTypeToHandlerTypeMap);
        }
    }
}
