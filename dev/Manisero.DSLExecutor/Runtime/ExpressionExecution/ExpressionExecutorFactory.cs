using System;
using System.Collections.Generic;
using Manisero.DSLExecutor.Runtime.ExpressionExecution.SpecificExpressionExecutors;
using Manisero.DSLExecutor.Runtime.FunctionExecution;
using Manisero.DSLExecutor.Runtime.FunctionExecution.FunctionHandlerResolution.FunctionHandlerResolvers;
using Manisero.DSLExecutor.Runtime.FunctionExecution.FunctionHandlerResolution.FunctionHandlerTypeResolvers;

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
                                                        new FunctionExpressionExecutor(new Lazy<IExpressionExecutor>(() => expressionExecutor),
                                                                                       new FunctionExecutor(new DictionarySourcedFunctionHandlerTypeResolver(functionTypeToHandlerTypeMap),
                                                                                                            new ActivatorUsingFunctionHandlerResolver())),
                                                        new BatchExpressionExecutor(new Lazy<IExpressionExecutor>(() => expressionExecutor)));

            return expressionExecutor;
        }
    }
}
