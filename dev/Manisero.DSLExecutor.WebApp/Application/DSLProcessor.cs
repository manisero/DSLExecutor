using System;
using System.Collections.Generic;
using Manisero.DSLExecutor.Library.Math;
using Manisero.DSLExecutor.Parser.SampleDSL;
using Manisero.DSLExecutor.WebApp.Application.Functions;

namespace Manisero.DSLExecutor.WebApp.Application
{
    public class DSLProcessorInput
    {
        public string DSL { get; set; }
    }

    public class DSLProcessorOutput
    {
        public ICollection<string> Log { get; set; }

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

                result = expressionResult?.ToString();
            }
            catch (Exception ex)
            {
                result = "ERROR: " + ex.Message;
            }

            return new DSLProcessorOutput
                {
                    Log = RequestLog.Get(),
                    Result = result
                };
        }

        private static ISampleDSLParser InitializeParser()
        {
            var functionTypeSamples = new[]
                {
                    typeof(AddFunction),
                    typeof(LogFunction)
                };

            return new SampleDSLParser(functionTypeSamples);
        }

        private static IDSLExecutor InitializeDSLExecutor()
        {
            var functionTypeToHandlerTypeMap = new Dictionary<Type, Type>
                {
                    [typeof(AddFunction)] = typeof(AddFunctionHandler),
                    [typeof(SubFunction)] = typeof(SubFunctionHandler),
                    [typeof(LogFunction)] = typeof(LogFunctionHandler),
                    [typeof(StringFunction)] = typeof(StringFunctionHandler)
            };

            return new DSLExecutor(functionTypeToHandlerTypeMap);
        }
    }
}
