using System;
using System.Collections.Generic;
using System.Reflection;
using Manisero.DSLExecutor.Domain.ExpressionsDomain;

namespace Manisero.DSLExecutor.ExpressionExecution.SpecificExpressionExecution.FunctionExecution
{
    public interface IFunctionParametersFiller
    {
        void Fill<TFunction>(TFunction function, IDictionary<string, IExpression> argumentExpressions);
    }

    public class FunctionParametersFiller : IFunctionParametersFiller
    {
        private readonly Lazy<IExpressionExecutor> _expressionExecutorFactory;

        public FunctionParametersFiller(Lazy<IExpressionExecutor> expressionExecutorFactory)
        {
            _expressionExecutorFactory = expressionExecutorFactory;
        }

        public void Fill<TFunction>(TFunction function, IDictionary<string, IExpression> argumentExpressions)
        {
            var functionProperties = typeof(TFunction).GetProperties();

            if ((argumentExpressions?.Count ?? 0) != functionProperties.Length)
            {
                // TODO: Unit-test this case
                throw new InvalidOperationException("Arguments number does not match the function's parameters number.");
            }

            foreach (var property in functionProperties)
            {
                FillParameter(function, property, argumentExpressions);
            }
        }

        private void FillParameter(object function, PropertyInfo property, IDictionary<string, IExpression> argumentExpressions)
        {
            IExpression argumentExpression;

            if (!argumentExpressions.TryGetValue(property.Name, out argumentExpression))
            {
                // TODO: Unit-test this case
                throw new InvalidOperationException($"Argument expression for '{property.Name}' parameter not found.");
            }

            if (!property.PropertyType.IsAssignableFrom(argumentExpression.ResultType))
            {
                // TODO: Unit-test this case
                throw new InvalidOperationException($"Result type of argument expression for '{property.Name}' parameter is invalid. Expected: '{property.PropertyType}' or its child. Actual: '{argumentExpression.ResultType}'.");
            }

            var argument = _expressionExecutorFactory.Value.Execute(argumentExpression);

            if (argument != null)
            {
                property.SetValue(function, argument);
            }
        }
    }
}
