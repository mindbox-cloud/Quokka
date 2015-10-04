namespace Quokka
{
	internal class VariableOutputBlock : TemplateNodeBase
	{
		private readonly VariableOccurence variableOccurence;

		public VariableOutputBlock(VariableOccurence variableOccurence)
		{
			this.variableOccurence = variableOccurence;
		}

		public override void CompileVariableDefinitions(Scope scope, ISemanticErrorListener errorListener)
		{
			scope.CreateOrUpdateVariableDefinition(variableOccurence, errorListener);
		}
	}
}
