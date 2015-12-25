namespace Quokka
{
	internal class ParameterValueArgument : FunctionArgumentBase
	{
		public VariableOccurence VariableOccurence { get; }

		public ParameterValueArgument(VariableOccurence variableOccurence, Location location)
			: base(location)
		{
			VariableOccurence = variableOccurence;
		}

		public override void CompileVariableDefinitions(SemanticAnalysisContext context, TypeDefinition requiredArgumentType)
		{
			context.VariableScope.CreateOrUpdateVariableDefinition(
				VariableOccurence.CloneWithSpecificLeafType(requiredArgumentType));
		}

		public override VariableValueStorage GetValue(RenderContext renderContext)
		{
			return renderContext.VariableScope.GetValueStorageForVariable(VariableOccurence);
		}

		public override void MapArgumentVariableDefinitionsToResult(
			SemanticAnalysisContext context,
			VariableDefinition resultDefinition,
			TemplateFunctionArgument functionArgument)
		{
			functionArgument.MapArgumentValueToResult(
				context,
				resultDefinition,
				context.VariableScope.TryGetVariableDefinition(VariableOccurence));
		}
	}
}
