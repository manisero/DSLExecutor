using System;
using System.Collections.Generic;
using Manisero.DSLExecutor.Domain.ExpressionsDomain;
using Manisero.DSLExecutor.ExpressionExecution;

namespace Manisero.DSLExecutor
{
    public interface IDSLExecutor
    {
        object ExecuteExpression(IExpression expression);
    }

    public class DSLExecutor : IDSLExecutor
    {
        private readonly Lazy<IExpressionExecutor> _expressionExecutor;

        public DSLExecutor(IDictionary<Type, Type> functionTypeToHandlerTypeMap) // TODO: Move functionTypeToHandlerTypeMap to some configuration
        {
            _expressionExecutor = new Lazy<IExpressionExecutor>(() => InitializeExpressionExecutor(functionTypeToHandlerTypeMap));
        }

        public object ExecuteExpression(IExpression expression)
        {
            return _expressionExecutor.Value.Execute(expression);
        }

        private IExpressionExecutor InitializeExpressionExecutor(IDictionary<Type, Type> functionTypeToHandlerTypeMap)
        {
            return new ExpressionExecutorFactory().Create(functionTypeToHandlerTypeMap);
        }
    }
}
