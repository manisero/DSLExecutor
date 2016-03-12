using System;
using System.Collections.Generic;
using System.Reflection;
using Manisero.DSLExecutor.Domain.ExpressionsDomain;
using Manisero.DSLExecutor.Domain.FunctionsDomain;
using Manisero.DSLExecutor.Extensions;
using Manisero.DSLExecutor.Runtime.FunctionExecution;

namespace Manisero.DSLExecutor.Runtime.ExpressionExecution.SpecificExpressionExecutors
{
    public interface IFunctionExpressionExecutor
    {
        object Execute(IFunctionExpression expression);
    }

    public class FunctionExpressionExecutor : IFunctionExpressionExecutor
    {
        private readonly IFunctionParametersFiller _functionParametersFiller;
        private readonly IFunctionExecutor _functionExecutor;

        private readonly Lazy<MethodInfo> _executeGenericMethod;

        public FunctionExpressionExecutor(IFunctionParametersFiller functionParametersFiller,
                                          IFunctionExecutor functionExecutor)
        {
            _functionParametersFiller = functionParametersFiller;
            _functionExecutor = functionExecutor;

            _executeGenericMethod = new Lazy<MethodInfo>(() => GetType().GetMethod(nameof(ExecuteGeneric),
                                                                                   BindingFlags.Instance | BindingFlags.NonPublic));
        }

        public object Execute(IFunctionExpression expression)
        {
            var functionType = expression.FunctionType;
            var functionDefinitionImplementation = functionType.GetGenericInterfaceDefinitionImplementation(typeof(IFunction<>));
            var resultType = functionDefinitionImplementation.GetGenericArguments()[0];

            return _executeGenericMethod.Value
                                        .MakeGenericMethod(functionType, resultType)
                                        .Invoke(this,
                                                new object[] { expression.ArgumentExpressions });
        }

        private TResult ExecuteGeneric<TFunction, TResult>(IDictionary<string, IExpression> argumentExpressions)
            where TFunction : IFunction<TResult>
        {
            var function = Activator.CreateInstance<TFunction>();
            _functionParametersFiller.Fill(function, argumentExpressions);

            return _functionExecutor.Execute<TFunction, TResult>(function);
        }
    }
}
