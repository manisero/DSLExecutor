﻿using System;
using Manisero.DSLExecutor.Domain.FunctionsDomain;

namespace Manisero.DSLExecutor.Runtime.FunctionExecution.FunctionHandlerResolution
{
    public class FunctionHandlerResolver
    {
        public IFunctionHandler<TFunction, TResult> Resolve<TFunction, TResult>()
            where TFunction : IFunction<TResult>
        {
            throw new NotImplementedException();
        }
    }
}