using System;
using System.Collections.Generic;
using System.Linq;
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
        public ICollection<string> Result { get; set; }
    }

    public class DSLProcessor
    {
        private readonly Lazy<ISampleDSLParser> _parser = new Lazy<ISampleDSLParser>(InitializeParser);
        private readonly Lazy<IDSLExecutor> _dslExecutor = new Lazy<IDSLExecutor>(InitializeDSLExecutor);

        public DSLProcessorOutput Process(DSLProcessorInput input)
        {
            ICollection<string> result;

            try
            {
                var expression = _parser.Value.Parse(input.DSL);
                var expressionResult = _dslExecutor.Value.ExecuteExpression(expression);

                result = RequestLog.Get()
                                   .Concat(new[] { expressionResult.ToString() })
                                   .ToList();
            }
            catch (Exception ex)
            {
                result = new[] { "ERROR: " + ex.Message };
            }

            return new DSLProcessorOutput
                {
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
