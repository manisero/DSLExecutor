using Manisero.DSLExecutor.Parser.SampleDSL.Parsing.Tokens;

namespace Manisero.DSLExecutor.Parser.SampleDSL.ExpressionGeneration.FunctionExpressionGeneration
{
    public interface IFunctionMetadataResolver
    {
        FunctionMetadata Resolve(FunctionCall functionCall);
    }
}
