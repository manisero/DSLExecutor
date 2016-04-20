using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Manisero.DSLExecutor.Domain.ExpressionsDomain;
using Manisero.DSLExecutor.Parser.SampleDSL.Parsing.Tokens;

namespace Manisero.DSLExecutor.Parser.SampleDSL.ExpressionGeneration.BatchExpressionGeneration
{
    public interface IBatchExpressionGenerator
    {
        IBatchExpression Generate(TokenTree tokenTree);
    }

    public class BatchExpressionGenerator : IBatchExpressionGenerator
    {
        private readonly Lazy<IExpressionGenerator> _expressionGeneratorFactory;

        private readonly Lazy<MethodInfo> _createBatchExpressionMethod;

        public BatchExpressionGenerator(Lazy<IExpressionGenerator> expressionGeneratorFactory)
        {
            _expressionGeneratorFactory = expressionGeneratorFactory;

            _createBatchExpressionMethod = new Lazy<MethodInfo>(() => GetType().GetMethod(nameof(CreateBatchExpression),
                                                                                          BindingFlags.Instance | BindingFlags.NonPublic));
        }

        public IBatchExpression Generate(TokenTree tokenTree)
        {
            var functionExpressions = tokenTree.FunctionCalls
                                               .Select(x => _expressionGeneratorFactory.Value.Generate(x))
                                               .ToList();

            var sideExpressions = functionExpressions.Take(functionExpressions.Count - 1);
            var resultExpression = functionExpressions.Last();

            try
            {
                return (IBatchExpression)_createBatchExpressionMethod.Value
                                                                     .MakeGenericMethod(resultExpression.ResultType)
                                                                     .Invoke(this,
                                                                             new object[] { sideExpressions, resultExpression });
            }
            catch (TargetInvocationException exception)
            {
                throw exception.InnerException;
            }
        }

        private BatchExpression<TResult> CreateBatchExpression<TResult>(IEnumerable<IExpression> sideExpressions, Expression<TResult> resultExpression)
        {
            return new BatchExpression<TResult>
                {
                    SideExpressions = sideExpressions,
                    ResultExpression = resultExpression
                };
        }
    }
}
