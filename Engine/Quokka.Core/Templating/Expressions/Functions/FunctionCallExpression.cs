using System;
using System.Collections.Generic;
using System.Linq;

namespace Mindbox.Quokka
{
	internal class FunctionCallExpression : VariantValueExpression
    {
		public string FunctionName { get; }

	    public Location Location { get; }

	    private readonly IReadOnlyList<ArgumentValue> argumentValues;

		public FunctionCallExpression(string functionName, IEnumerable<ArgumentValue> argumentValues, Location location)
	    {
			FunctionName = functionName;
		    this.argumentValues = argumentValues.ToList().AsReadOnly();
			Location = location;
		}

	    public override void PerformSemanticAnalysis(AnalysisContext context, TypeDefinition expectedExpressionType)
	    {
		    var resultType = GetResultType(context);
		    if (!resultType.IsAssignableTo(expectedExpressionType))
			    context.ErrorListener.AddInvalidFunctionResultTypeError(
				    FunctionName,
				    expectedExpressionType,
				    resultType,
				    Location);

			var function = TryGetFunctionForSemanticAnalysis(context);
		    function?.Arguments.PerformSemanticAnalysis(context, argumentValues, Location);
		}
		
	    public override VariableValueStorage Evaluate(RenderContext renderContext)
	    {
			var function = renderContext.Functions.TryGetFunction(FunctionName, argumentValues);
		    if (function == null)
			    throw new InvalidOperationException($"Function {FunctionName} not found");

		    try
		    {
			    return function.Invoke(
					argumentValues
						.Select((argumentValue, argumentNumber) => 
							argumentValue.GetValue(renderContext, function.Arguments.GetArgument(argumentNumber)))
						.ToList());
		    }
		    catch (Exception ex)
		    {
			    throw new UnrenderableTemplateModelException(
					$"Function {FunctionName} invocation resulted in error",
					ex,
					Location);
		    }
	    }

	    public override void RegisterIterationOverExpressionResult(AnalysisContext context, ValueUsageSummary iterationVariable)
	    {
		    var function = TryGetFunctionForSemanticAnalysis(context);

		    // We only do this if the template is semantically valid. If not, semantic errors are already handled
		    // earlier in the workflow.
		    function?.Arguments.AnalyzeArgumentValuesBasedOnFunctionResultUsages(context, argumentValues, iterationVariable);
		}

	    public override IModelDefinition GetExpressionResultModelDefinition(AnalysisContext context)
	    {
			var function = TryGetFunctionForSemanticAnalysis(context);
		    if (function == null)
			    return new PrimitiveModelDefinition(TypeDefinition.Unknown);

		    return function.ReturnValueDefinition;
	    }

	    public override bool CheckIfExpressionIsNull(RenderContext renderContext)
	    {
		    var evaluationResult = Evaluate(renderContext);
		    return evaluationResult.CheckIfValueIsNull();
	    }
		
	    private TemplateFunction TryGetFunctionForSemanticAnalysis(AnalysisContext context)
	    {
		    var function = context.Functions.TryGetFunction(FunctionName, argumentValues);
		    if (function == null)
			    context.ErrorListener.AddUndefinedFunctionError(FunctionName, Location);

		    return function;
	    }
	}
}
