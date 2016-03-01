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
                throw new InvalidOperationException("TODO: Handle and test this case");
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
                    throw new InvalidOperationException("TODO: Handle and test this case");
                }

                var argument = _expressionExecutorFactory.Value.Execute(argumentExpression);

                if (argument != null)
                {
                    if (!property.PropertyType.IsInstanceOfType(argument))
                    {
                        throw new InvalidOperationException("TODO: Handle and test this case");
                    }

                    property.SetValue(function, argument);
                }
            }
        }
    }
}
