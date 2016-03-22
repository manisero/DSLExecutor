using System;
using System.Collections.Generic;
using System.Reflection;
using Manisero.DSLExecutor.Domain.ExpressionsDomain;
using Manisero.DSLExecutor.Domain.FunctionsDomain;
using Manisero.DSLExecutor.ExpressionExecution.SpecificExpressionExecution.FunctionExecution;
using Manisero.DSLExecutor.Extensions;

namespace Manisero.DSLExecutor.ExpressionExecution.SpecificExpressionExecution
{
    public interface IFunctionExpressionExecutor
    {
        object Execute(IFunctionExpression expression);
    }

    public class FunctionExpressionExecutor : IFunctionExpressionExecutor
    {
        private readonly IFunctionParametersFiller _functionParametersFiller;
        private readonly IFunctionHandlerResolver _functionHandlerResolver;

        private readonly Lazy<MethodInfo> _executeFunctionMethod;

        public FunctionExpressionExecutor(IFunctionParametersFiller functionParametersFiller,
                                          IFunctionHandlerResolver functionHandlerResolver)
        {
            _functionParametersFiller = functionParametersFiller;
            _functionHandlerResolver = functionHandlerResolver;

            _executeFunctionMethod = new Lazy<MethodInfo>(() => GetType().GetMethod(nameof(ExecuteFunction),
                                                                                    BindingFlags.Instance | BindingFlags.NonPublic));
        }

        public object Execute(IFunctionExpression expression)
        {
            var functionType = expression.FunctionType;
            var functionDefinitionImplementation = functionType.GetGenericInterfaceDefinitionImplementation(typeof(IFunction<>));
            var resultType = functionDefinitionImplementation.GetGenericArguments()[0];

            try
            {
                return _executeFunctionMethod.Value
                                             .MakeGenericMethod(functionType, resultType)
                                             .Invoke(this,
                                                     new object[] { expression.ArgumentExpressions });
            }
            catch (TargetInvocationException exception)
            {
                throw exception.InnerException;
            }
        }

        private TResult ExecuteFunction<TFunction, TResult>(IDictionary<string, IExpression> argumentExpressions)
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
