using System;
using Manisero.DSLExecutor.Domain.ExpressionsDomain;
using Manisero.DSLExecutor.Parser.SampleDSL.ExpressionGeneration.BatchExpressionGeneration;
using Manisero.DSLExecutor.Parser.SampleDSL.ExpressionGeneration.ConstantExpressionGeneration;
using Manisero.DSLExecutor.Parser.SampleDSL.ExpressionGeneration.FunctionExpressionGeneration;
using Manisero.DSLExecutor.Parser.SampleDSL.Parsing.Tokens;

namespace Manisero.DSLExecutor.Parser.SampleDSL.ExpressionGeneration
{
    public interface IExpressionGenerator
    {
        IExpression Generate(IToken token);
    }

    public class ExpressionGenerator : IExpressionGenerator
    {
        private readonly IConstantExpressionGenerator _constantExpressionGenerator;
        private readonly IFunctionExpressionGenerator _functionExpressionGenerator;
        private readonly IBatchExpressionGenerator _batchExpressionGenerator;

        public ExpressionGenerator(IConstantExpressionGenerator constantExpressionGenerator,
                                   IFunctionExpressionGenerator functionExpressionGenerator,
                                   IBatchExpressionGenerator batchExpressionGenerator)
        {
            _constantExpressionGenerator = constantExpressionGenerator;
            _functionExpressionGenerator = functionExpressionGenerator;
            _batchExpressionGenerator = batchExpressionGenerator;
        }

        public IExpression Generate(IToken token)
        {
            var literal = token as Literal;

            if (literal != null)
            {
                return _constantExpressionGenerator.Generate(literal);
            }

            var functionCall = token as FunctionCall;

            if (functionCall != null)
            {
                return _functionExpressionGenerator.Generate(functionCall);
            }

            var tokenTree = token as TokenTree;

            if (tokenTree != null)
            {
                return _batchExpressionGenerator.Generate(tokenTree);
            }

            throw new NotSupportedException($"Not supported token type: {token.GetType().FullName}.");
        }
    }
}
