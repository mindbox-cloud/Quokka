using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mindbox.Quokka
{
    internal class FunctionCallExpression : VariantValueExpression
    {
		public string FunctionName { get; }

	    public Location Location { get; }

	    private readonly IReadOnlyList<Argument> arguments;

		public FunctionCallExpression(string functionName, Location location, IEnumerable<Argument> arguments)
	    {
			FunctionName = functionName;
		    Location = location;
		    this.arguments = arguments.ToList().AsReadOnly();
		}

	    public override TypeDefinition GetResultType(SemanticAnalysisContext context)
	    {
		    var function = TryGetFunctionForSemanticAnalysis(context);
		    return function != null
						? TypeDefinition.GetTypeDefinitionFromModelDefinition(function.ReturnValueDefinition)
						: TypeDefinition.Unknown;
	    }

	    public override void CompileVariableDefinitions(SemanticAnalysisContext context, TypeDefinition expectedExpressionType)
	    {
		    var resultType = GetResultType(context);
		    if (!resultType.IsAssignableTo(expectedExpressionType))
			    context.ErrorListener.AddInvalidFunctionResultTypeError(
				    FunctionName,
				    expectedExpressionType,
				    resultType,
				    Location);

			var function = TryGetFunctionForSemanticAnalysis(context);
		    function?.Arguments.CompileVariableDefinitions(context, arguments, Location);
		}
		
	    public override VariableValueStorage Evaluate(RenderContext renderContext)
	    {
			var function = renderContext.Functions.TryGetFunction(FunctionName);
		    if (function == null)
			    throw new InvalidOperationException($"Function {FunctionName} not found");

		    return function.Invoke(arguments.Select(arg => arg.GetValue(renderContext)).ToList());
		}

	    public override void RegisterIterationOverExpressionResult(SemanticAnalysisContext context, VariableDefinition iterationVariable)
	    {
		    var function = TryGetFunctionForSemanticAnalysis(context);

		    // We only do this if the template is semantically valid. If not, semantic errors are already handled
		    // earlier in the workflow.
		    function?.Arguments.MapArgumentVariableDefinitionsToResult(context, arguments, iterationVariable);
		}

	    public override IModelDefinition GetExpressionResultModelDefinition(SemanticAnalysisContext context)
	    {
			var function = TryGetFunctionForSemanticAnalysis(context);
		    var arrayModelDefinition = function?.ReturnValueDefinition as IArrayModelDefinition;
		    return arrayModelDefinition?.ElementModelDefinition;
		}

	    public override bool CheckIfExpressionIsNull(RenderContext renderContext)
	    {
		    throw new NotImplementedException();
	    }
		
	    private TemplateFunction TryGetFunctionForSemanticAnalysis(SemanticAnalysisContext context)
	    {
		    var function = context.Functions.TryGetFunction(FunctionName);
		    if (function == null)
			    context.ErrorListener.AddUndefinedFunctionError(FunctionName, Location);

		    return function;
	    }
	}
}
