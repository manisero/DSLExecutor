using System;
using System.Reflection;
using Manisero.DSLExecutor.Domain.ExpressionsDomain;
using Manisero.DSLExecutor.Domain.FunctionsDomain;
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

        public FunctionExpressionExecutor(Lazy<IExpressionExecutor> expressionExecutorFactory,
                                          IFunctionExecutor functionExecutor)
        {
            _expressionExecutorFactory = expressionExecutorFactory;
            _functionExecutor = functionExecutor;
        }

        public object Execute(IFunctionExpression expression)
        {
            var functionType = expression.FunctionType;
            var functionProperties = functionType.GetProperties();

            if ((expression.ArgumentExpressions?.Count ?? 0) != functionProperties.Length)
            {
                // TODO: Unit-test this case
                throw new InvalidOperationException("Arguments number does not match the function's parameters number.");
            }

            var function = (IFunction)Activator.CreateInstance(functionType);

            if (functionProperties.Length > 0)
            {
                FillFunctionParameters(expression, function, functionProperties);
            }

            return _functionExecutor.Execute(function);
        }

        private void FillFunctionParameters(IFunctionExpression expression, IFunction function, PropertyInfo[] functionProperties)
        {
            foreach (var property in functionProperties)
            {
                IExpression argumentExpression;

                if (!expression.ArgumentExpressions.TryGetValue(property.Name, out argumentExpression))
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
