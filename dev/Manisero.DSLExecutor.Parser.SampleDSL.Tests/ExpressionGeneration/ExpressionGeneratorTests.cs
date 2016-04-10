using System;
using Manisero.DSLExecutor.Domain.ExpressionsDomain;
using Manisero.DSLExecutor.Parser.SampleDSL.ExpressionGeneration;
using Manisero.DSLExecutor.Parser.SampleDSL.Parsing.Tokens;
using Xunit;

namespace Manisero.DSLExecutor.Parser.SampleDSL.Tests.ExpressionGeneration
{
    public class ExpressionGeneratorTests
    {
        private IExpression Act(TokenTree tokenTree)
        {
            var generator = new ExpressionGenerator();

            return generator.Generate(tokenTree);
        }

        [Fact]
        public void test()
        {
            var result = Act(null);

            throw new NotImplementedException();
        }
    }
}
