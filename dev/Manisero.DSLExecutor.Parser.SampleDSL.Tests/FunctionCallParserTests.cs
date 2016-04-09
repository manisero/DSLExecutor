using System.Collections.Generic;
using Sprache;
using Xunit;

namespace Manisero.DSLExecutor.Parser.SampleDSL.Tests
{
    public interface IFunctionArgumentToken
    {
    }

    public class FunctionArguments
    {
        public IEnumerable<IFunctionArgumentToken> Arguments { get; set; }
    }

    public class FunctionCall : IFunctionArgumentToken
    {
        public string FunctionName { get; set; }

        public FunctionArguments FunctionArguments { get; set; }
    }

    public class FunctionParsersContainer
    {
        public static readonly Parser<FunctionArguments> FunctionArgumentsParser = null;

        public static readonly Parser<FunctionCall> FunctionCallParser = null;
    }

    public class FunctionCallParserTests
    {
        [Theory]
        public void test()
        {
        }
    }
}
