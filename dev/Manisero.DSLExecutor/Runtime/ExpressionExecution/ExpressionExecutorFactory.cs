using System;
using Manisero.DSLExecutor.Runtime.ExpressionExecution.SpecificExpressionExecutors;

namespace Manisero.DSLExecutor.Runtime.ExpressionExecution
{
    public interface IExpressionExecutorFactory
    {
        IExpressionExecutor Create();
    }

    public class ExpressionExecutorFactory : IExpressionExecutorFactory
    {
        public IExpressionExecutor Create()
        {
            IExpressionExecutor expressionExecutor = null;

            expressionExecutor = new ExpressionExecutor(new ConstantExpressionExecutor(),
                                                        new FunctionExpressionExecutor(),
                                                        new BatchExpressionExecutor(new Lazy<IExpressionExecutor>(() => expressionExecutor)));

            return expressionExecutor;
        }
    }
}
