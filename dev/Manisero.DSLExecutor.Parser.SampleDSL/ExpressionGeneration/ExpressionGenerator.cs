using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Manisero.DSLExecutor.Domain.ExpressionsDomain;
using Manisero.DSLExecutor.Parser.SampleDSL.ExpressionGeneration.FunctionExpressionGeneration;
using Manisero.DSLExecutor.Parser.SampleDSL.Parsing.Tokens;

namespace Manisero.DSLExecutor.Parser.SampleDSL.ExpressionGeneration
{
    public interface IExpressionGenerator
    {
        IBatchExpression Generate(TokenTree tokenTree);
    }

    public class ExpressionGenerator : IExpressionGenerator
    {
        private readonly IFunctionExpressionGenerator _functionExpressionGenerator;

        private readonly Lazy<MethodInfo> _createBatchExpressionMethod;

        public ExpressionGenerator(IFunctionExpressionGenerator functionExpressionGenerator)
        {
            _functionExpressionGenerator = functionExpressionGenerator;

            _createBatchExpressionMethod = new Lazy<MethodInfo>(() => GetType().GetMethod(nameof(CreateBatchExpression),
                                                                                          BindingFlags.Instance | BindingFlags.NonPublic));
        }

        public IBatchExpression Generate(TokenTree tokenTree)
        {
            var functionExpressions = tokenTree.FunctionCalls
                                               .Select(x => _functionExpressionGenerator.Generate(x))
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
