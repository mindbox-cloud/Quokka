namespace Quokka
{
	internal class VariableOutputBlock : TemplateNodeBase
	{
		public VariableOccurence VariableOccurence { get; }

		public VariableOutputBlock(VariableOccurence variableOccurence)
		{
			VariableOccurence = variableOccurence;
		}
	}
}
