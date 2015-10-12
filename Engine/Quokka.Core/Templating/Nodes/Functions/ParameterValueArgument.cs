namespace Quokka
{
	internal class ParameterValueArgument : FunctionArgumentBase
	{
		private readonly VariableOccurence variableOccurence;

		public ParameterValueArgument(VariableOccurence variableOccurence, Location location)
			: base(location)
		{
			this.variableOccurence = variableOccurence;
		}

		public override void CompileVariableDefinitions(SemanticAnalysisContext context, VariableType requiredArgumentType)
		{
			context.VariableScope.CreateOrUpdateVariableDefinition(
				variableOccurence.CloneWithSpecificLeafType(requiredArgumentType));
		}

		public override object GetValue(RenderContext renderContext)
		{
			return renderContext.VariableScope.GetVariableValue(variableOccurence);
		}
	}
}
