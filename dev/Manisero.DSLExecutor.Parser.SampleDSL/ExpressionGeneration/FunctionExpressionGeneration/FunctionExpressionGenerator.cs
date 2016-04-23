using System;
using System.Collections.Generic;
using System.Reflection;
using Manisero.DSLExecutor.Domain.ExpressionsDomain;
using Manisero.DSLExecutor.Domain.FunctionsDomain;
using Manisero.DSLExecutor.Parser.SampleDSL.Parsing.Tokens;

namespace Manisero.DSLExecutor.Parser.SampleDSL.ExpressionGeneration.FunctionExpressionGeneration
{
    public interface IFunctionExpressionGenerator
    {
        IFunctionExpression Generate(FunctionCall functionCall);
    }

    public class FunctionExpressionGenerator : IFunctionExpressionGenerator
    {
        private readonly IFunctionMetadataResolver _functionMetadataResolver;
        private readonly IFunctionArgumentExpressionsGenerator _functionArgumentExpressionsGenerator;

        private readonly Lazy<MethodInfo> _createFunctionExpressionMethod;

        public FunctionExpressionGenerator(IFunctionMetadataResolver functionMetadataResolver,
                                           IFunctionArgumentExpressionsGenerator functionArgumentExpressionsGenerator)
        {
            _functionMetadataResolver = functionMetadataResolver;
            _functionArgumentExpressionsGenerator = functionArgumentExpressionsGenerator;

            _createFunctionExpressionMethod = new Lazy<MethodInfo>(() => GetType().GetMethod(nameof(CreateFunctionExpression),
                                                                                             BindingFlags.Instance | BindingFlags.NonPublic));
        }

        public IFunctionExpression Generate(FunctionCall functionCall)
        {
            var functionMetadata = _functionMetadataResolver.Resolve(functionCall);

            if (functionMetadata == null)
            {
                throw new InvalidOperationException($"Could not find function of name '{functionCall.FunctionName}'.");
            }
            
            var argumentExpressions = _functionArgumentExpressionsGenerator.Generate(functionCall.Arguments, functionMetadata.FunctionContract);

            try
            {
                return (IFunctionExpression)_createFunctionExpressionMethod.Value
                                                                           .MakeGenericMethod(functionMetadata.FunctionType,
                                                                                              functionMetadata.FunctionContract.ResultType)
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
