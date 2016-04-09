namespace Manisero.DSLExecutor.Parser.SampleDSL.Tokens
{
    public class FunctionCall : IFunctionArgumentToken
    {
        public string FunctionName { get; set; }

        public FunctionArguments FunctionArguments { get; set; }
    }
}
