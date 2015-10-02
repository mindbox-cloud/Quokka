namespace Quokka
{
	internal class ForBlock : TemplateNodeBase
	{
		private readonly ITemplateNode block;
		private readonly VariableOccurence collection;
		private readonly IterationVariableDeclaration iterationVariable;

		public ForBlock(ITemplateNode block, VariableOccurence collection, IterationVariableDeclaration iterationVariable)
		{
			this.block = block;
			this.collection = collection;
			this.iterationVariable = iterationVariable;
		}
	}
}
