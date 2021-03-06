﻿using System;
using Manisero.DSLExecutor.Domain.ExpressionsDomain;

namespace Manisero.DSLExecutor.Tests.TestsDomain
{
    public class EmptyExpression : IExpression
    {
        public Type ResultType => typeof(object);
    }

    public class EmptyExpression<TResult> : Expression<TResult>
    {
    }
}
