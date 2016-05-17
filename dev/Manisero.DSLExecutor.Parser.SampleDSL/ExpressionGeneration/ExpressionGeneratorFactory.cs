using System;
using System.Collections.Generic;
using Manisero.DSLExecutor.Parser.SampleDSL.ExpressionGeneration.BatchExpressionGeneration;
using Manisero.DSLExecutor.Parser.SampleDSL.ExpressionGeneration.ConstantExpressionGeneration;
using Manisero.DSLExecutor.Parser.SampleDSL.ExpressionGeneration.FunctionExpressionGeneration;
using Manisero.DSLExecutor.Parser.SampleDSL.ExpressionGeneration.FunctionExpressionGeneration.ArgumentExpressionsGeneration;
using Manisero.DSLExecutor.Parser.SampleDSL.ExpressionGeneration.FunctionExpressionGeneration.MetadataResolution;
using Manisero.DSLExecutor.Parser.SampleDSL.ExpressionGeneration.FunctionExpressionGeneration.MetadataResolution.FunctionNameToMetadataMapFactories;
using Manisero.DSLExecutor.Utilities;

namespace Manisero.DSLExecutor.Parser.SampleDSL.ExpressionGeneration
{
    public interface IExpressionGeneratorFactory
    {
        IExpressionGenerator Create(IEnumerable<Type> functionTypeSamples);
    }

    public class ExpressionGeneratorFactory : IExpressionGeneratorFactory
    {
        public IExpressionGenerator Create(IEnumerable<Type> functionTypeSamples)
        {
            IExpressionGenerator expressionGenerator = null;

            expressionGenerator = new ExpressionGenerator(new ConstantExpressionGenerator(),
                                                          new FunctionExpressionGenerator(new FunctionMetadataResolver(new TypeSamplesAndSuffixConventionBasedFunctionNameToMetadataMapFactory(functionTypeSamples,
                                                                                                                                                                                               new FunctionContractProvider())),
                                                                                          new FunctionArgumentExpressionsGenerator(new FunctionArgumentExpressionGenerator(new Lazy<IExpressionGenerator>(() => expressionGenerator)))),
                                                          new BatchExpressionGenerator(new Lazy<IExpressionGenerator>(() => expressionGenerator)));

            return expressionGenerator;
        }
    }
}
