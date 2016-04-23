using System;
using System.Collections.Generic;
using System.Reflection;
using Manisero.DSLExecutor.Domain.ExpressionsDomain;
using Manisero.DSLExecutor.Domain.FunctionsDomain;
using Manisero.DSLExecutor.Parser.SampleDSL.Parsing.Tokens;
using Manisero.DSLExecutor.Utilities;

namespace Manisero.DSLExecutor.Parser.SampleDSL.ExpressionGeneration.FunctionExpressionGeneration
{
    public interface IFunctionExpressionGenerator
    {
        IFunctionExpression Generate(FunctionCall functionCall);
    }

    public class FunctionExpressionGenerator : IFunctionExpressionGenerator
    {
        private readonly IFunctionTypeResolver _functionTypeResolver;
        private readonly IFunctionContractProvider _functionContractProvider;
        private readonly IFunctionArgumentExpressionsGenerator _functionArgumentExpressionsGenerator;

        private readonly Lazy<MethodInfo> _createFunctionExpressionMethod;

        public FunctionExpressionGenerator(IFunctionTypeResolver functionTypeResolver,
                                           IFunctionContractProvider functionContractProvider,
                                           IFunctionArgumentExpressionsGenerator functionArgumentExpressionsGenerator)
        {
            _functionTypeResolver = functionTypeResolver;
            _functionContractProvider = functionContractProvider;
            _functionArgumentExpressionsGenerator = functionArgumentExpressionsGenerator;

            _createFunctionExpressionMethod = new Lazy<MethodInfo>(() => GetType().GetMethod(nameof(CreateFunctionExpression),
                                                                                             BindingFlags.Instance | BindingFlags.NonPublic));
        }

        public IFunctionExpression Generate(FunctionCall functionCall)
        {
            var functionType = _functionTypeResolver.Resolve(functionCall);

            if (functionType == null)
            {
                throw new InvalidOperationException($"Could not find function of name '{functionCall.FunctionName}'.");
            }

            var functionContract = _functionContractProvider.Provide(functionType);
            var argumentExpressions = _functionArgumentExpressionsGenerator.Generate(functionCall.Arguments, functionContract);

            try
            {
                return (IFunctionExpression)_createFunctionExpressionMethod.Value
                                                                           .MakeGenericMethod(functionType, functionContract.ResultType)
                                                                           .Invoke(this,
                                                                                   new object[] { argumentExpressions });
            }
            catch (TargetInvocationException exception)
            {
                throw exception.InnerException;
            }
        }

        private FunctionExpression<TFunction, TResult> CreateFunctionExpression<TFunction, TResult>(IDictionary<string, IExpression> argumentExpressions)
            where TFunction : IFunction<TResult>
        {
            return new FunctionExpression<TFunction, TResult>
                {
                    ArgumentExpressions = argumentExpressions
                };
        }
    }
}
