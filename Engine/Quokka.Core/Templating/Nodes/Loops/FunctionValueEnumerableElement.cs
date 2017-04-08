using System;
using System.Collections.Generic;

namespace Mindbox.Quokka
{
	internal class FunctionValueEnumerableElement : EnumerableElementBase
	{
		private readonly FunctionCall functionCall;

		public FunctionValueEnumerableElement(FunctionCall functionCall)
		{
			this.functionCall = functionCall;
		}
		
		public override void CompileVariableDefinitions(SemanticAnalysisContext context)
		{
			functionCall.CompileVariableDefinitions(context);

			var function = functionCall.TryGetFunctionForSemanticAnalysis(context);
			if (function == null)
				return;

			var returnType = TypeDefinition.GetTypeDefinitionFromModelDefinition(function.ReturnValueDefinition);
			if (!returnType.IsCompatibleWithRequired(TypeDefinition.Array))
				context.ErrorListener.AddInvalidFunctionResultTypeError(
					functionCall.FunctionName,
					TypeDefinition.Array,
					returnType,
					functionCall.Location);
		}

		public override void ProcessIterationVariableUsages(SemanticAnalysisContext context, VariableDefinition iterationVariable)
		{
			functionCall.MapArgumentVariableDefinitionsToResult(context, iterationVariable);
		}

		public override IModelDefinition GetEnumerationVariableDeclarationDefinition(SemanticAnalysisContext context)
		{
			var function = functionCall.TryGetFunctionForSemanticAnalysis(context);
			var arrayModelDefinition = function?.ReturnValueDefinition as IArrayModelDefinition;
			return arrayModelDefinition?.ElementModelDefinition;
		}

		public override IEnumerable<VariableValueStorage> Enumerate(RenderContext context)
		{
			return functionCall.GetInvocationResult(context).GetElements();
		}
	}
}
