namespace Quokka
{
	internal class VariableOutputBlock : TemplateNodeBase
	{
		private readonly VariableOccurence variableOccurence;

		public VariableOutputBlock(VariableOccurence variableOccurence)
		{
			this.variableOccurence = variableOccurence;
		}

		public override void CompileVariableDefinitions(VariableCollection variableCollection, ISemanticErrorListener errorListener)
		{
			variableCollection.ProcessVariableOccurence(variableOccurence, errorListener);
		}
	}
}
