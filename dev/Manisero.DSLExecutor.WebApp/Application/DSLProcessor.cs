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
        public DSLProcessorOutput Process(DSLProcessorInput input)
        {
            return new DSLProcessorOutput
                {
                    Result = input.DSL
                };
        }
    }
}
