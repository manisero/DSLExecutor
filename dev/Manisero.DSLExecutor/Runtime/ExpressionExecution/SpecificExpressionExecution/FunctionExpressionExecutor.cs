using System;
using System.Collections.Generic;
using System.Reflection;
using Manisero.DSLExecutor.Domain.ExpressionsDomain;
using Manisero.DSLExecutor.Domain.FunctionsDomain;
using Manisero.DSLExecutor.Extensions;
using Manisero.DSLExecutor.Runtime.ExpressionExecution.SpecificExpressionExecution.FunctionExpressionExecution;

namespace Manisero.DSLExecutor.Runtime.ExpressionExecution.SpecificExpressionExecution
{
    public interface IFunctionExpressionExecutor
    {
        object Execute(IFunctionExpression expression);
    }

    public class FunctionExpressionExecutor : IFunctionExpressionExecutor
    {
        private readonly IFunctionParametersFiller _functionParametersFiller;
        private readonly IFunctionHandlerResolver _functionHandlerResolver;

        private readonly Lazy<MethodInfo> _executeGenericMethod;

        public FunctionExpressionExecutor(IFunctionParametersFiller functionParametersFiller,
                                          IFunctionHandlerResolver functionHandlerResolver)
        {
            _functionParametersFiller = functionParametersFiller;
            _functionHandlerResolver = functionHandlerResolver;

            _executeGenericMethod = new Lazy<MethodInfo>(() => GetType().GetMethod(nameof(ExecuteGeneric),
                                                                                   BindingFlags.Instance | BindingFlags.NonPublic));
        }

        public object Execute(IFunctionExpression expression)
        {
            var functionType = expression.FunctionType;
            var functionDefinitionImplementation = functionType.GetGenericInterfaceDefinitionImplementation(typeof(IFunction<>));
            var resultType = functionDefinitionImplementation.GetGenericArguments()[0];

            try
            {
                return _executeGenericMethod.Value
                                            .MakeGenericMethod(functionType, resultType)
                                            .Invoke(this,
                                                    new object[] { expression.ArgumentExpressions });
            }
            catch (TargetInvocationException exception)
            {
                throw exception.InnerException;
            }
        }

        private TResult ExecuteGeneric<TFunction, TResult>(IDictionary<string, IExpression> argumentExpressions)
            where TFunction : IFunction<TResult>
        {
            var function = Activator.CreateInstance<TFunction>();
            _functionParametersFiller.Fill(function, argumentExpressions);

            var functionHandler = _functionHandlerResolver.Resolve<TFunction, TResult>();

            if (functionHandler == null)
            {
                throw new NotSupportedException($"Could not resolve handler for '{typeof(TFunction)}' function.");
            }

            return functionHandler.Handle(function);
        }
    }
}
