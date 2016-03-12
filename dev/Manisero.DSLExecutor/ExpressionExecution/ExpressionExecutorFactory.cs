using System;
using System.Collections.Generic;
using Manisero.DSLExecutor.ExpressionExecution.SpecificExpressionExecution;
using Manisero.DSLExecutor.ExpressionExecution.SpecificExpressionExecution.FunctionExecution;
using Manisero.DSLExecutor.ExpressionExecution.SpecificExpressionExecution.FunctionExecution.FunctionHandlerResolvers;

namespace Manisero.DSLExecutor.ExpressionExecution
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
                                                                                       new DictionarySourcedActivatorUsingFunctionHandlerResolver(functionTypeToHandlerTypeMap)),
                                                        new BatchExpressionExecutor(new Lazy<IExpressionExecutor>(() => expressionExecutor)));

            return expressionExecutor;
        }
    }
}
