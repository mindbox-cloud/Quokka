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

		public override TypeDefinition TryGetStaticType(SemanticAnalysisContext context)
		{
			// We never really know the variable type at compile-time. It's ok not to return anything here
			// because if parameter of the wrong type is used as a function parameter we catch it when we try to determine
			// parameter's type.
			return null;
		}

		public override VariableValueStorage GetValue(RenderContext renderContext)
		{
			return renderContext.VariableScope.GetValueStorageForVariable(VariableOccurence, false);
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
