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
        private readonly Lazy<IExpressionExecutor> _expressionExecutorFactory;
        private readonly IFunctionExecutor _functionExecutor;

        private readonly Lazy<MethodInfo> _executeGenericMethod;

        public FunctionExpressionExecutor(Lazy<IExpressionExecutor> expressionExecutorFactory,
                                          IFunctionExecutor functionExecutor)
        {
            _expressionExecutorFactory = expressionExecutorFactory;
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
            FillFunctionParameters(function, argumentExpressions);

            return _functionExecutor.Execute<TFunction, TResult>(function);
        }

        private void FillFunctionParameters<TFunction>(TFunction function, IDictionary<string, IExpression> argumentExpressions)
        {
            var functionProperties = typeof(TFunction).GetProperties();

            if ((argumentExpressions?.Count ?? 0) != functionProperties.Length)
            {
                // TODO: Unit-test this case
                throw new InvalidOperationException("Arguments number does not match the function's parameters number.");
            }

            if (functionProperties.Length == 0)
            {
                return;
            }

            foreach (var property in functionProperties)
            {
                IExpression argumentExpression;

                if (!argumentExpressions.TryGetValue(property.Name, out argumentExpression))
                {
                    // TODO: Unit-test this case
                    throw new InvalidOperationException($"Argument Expression for '{property.Name}' parameter not found.");
                }

                if (!property.PropertyType.IsAssignableFrom(argumentExpression.ResultType))
                {
                    // TODO: Unit-test this case
                    throw new InvalidOperationException($"Result Type of Argument Expression for '{property.Name}' parameter is invalid. Expected: '{property.PropertyType}' or its child. Actual: '{argumentExpression.ResultType}'.");
                }

                var argument = _expressionExecutorFactory.Value.Execute(argumentExpression);

                if (argument != null)
                {
                    property.SetValue(function, argument);
                }
            }
        }
    }
}
