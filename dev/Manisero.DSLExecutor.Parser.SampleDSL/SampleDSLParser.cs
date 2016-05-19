using System;
using System.Collections.Generic;
using Manisero.DSLExecutor.Domain.ExpressionsDomain;
using Manisero.DSLExecutor.Parser.SampleDSL.ExpressionGeneration;
using Manisero.DSLExecutor.Parser.SampleDSL.Parsing;
using Sprache;

namespace Manisero.DSLExecutor.Parser.SampleDSL
{
    public interface ISampleDSLParser
    {
        IExpression Parse(string input);
    }

    public class SampleDSLParser : ISampleDSLParser
    {
        private readonly Lazy<IExpressionGenerator> _expressionGenerator;

        public SampleDSLParser(IEnumerable<Type> functionTypeSamples) // TODO: Move functionTypeSamples to some configuration
        {
            _expressionGenerator = new Lazy<IExpressionGenerator>(() => InitializeExpressionGenerator(functionTypeSamples));
        }

        public IExpression Parse(string input)
        {
            var tokenTree = TokenTreeParsers.TokenTreeParser.Parse(input);

            return _expressionGenerator.Value.Generate(tokenTree);
        }

        private IExpressionGenerator InitializeExpressionGenerator(IEnumerable<Type> functionTypeSamples)
        {
            return new ExpressionGeneratorFactory().Create(functionTypeSamples);
        }
    }
}
