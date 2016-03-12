using System;
using System.Collections.Generic;
using Manisero.DSLExecutor.Runtime.ExpressionExecution.SpecificExpressionExecution;
using Manisero.DSLExecutor.Runtime.ExpressionExecution.SpecificExpressionExecution.FunctionExpressionExecution;
using Manisero.DSLExecutor.Runtime.ExpressionExecution.SpecificExpressionExecution.FunctionExpressionExecution.FunctionHandlerResolvers;

namespace Manisero.DSLExecutor.Runtime.ExpressionExecution
{
    public interface IExpressionExecutorFactory
    {
        IExpressionExecutor Create(IDictionary<Type, Type> functionTypeToHandlerTypeMap);
    }

    public class ExpressionExecutorFactory : IExpressionExecutorFactory
    {
        public IExpressionExecutor Create(IDictionary<Type, Type> functionTypeToHandlerTypeMap)
        {
            IExpressionExecutor expressionExecutor = null;

            expressionExecutor = new ExpressionExecutor(new ConstantExpressionExecutor(),
                                                        new FunctionExpressionExecutor(new FunctionParametersFiller(new Lazy<IExpressionExecutor>(() => expressionExecutor)),
                                                                                       new FunctionHandlerResolver()),
                                                        new BatchExpressionExecutor(new Lazy<IExpressionExecutor>(() => expressionExecutor)));

            return expressionExecutor;
        }
    }
}
