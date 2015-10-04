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

		public override void CompileVariableDefinitions(Scope scope, ISemanticErrorListener errorListener)
		{
			var collectionVariableDefinition = 
				scope.CreateOrUpdateVariableDefinition(collection, errorListener);
			var innerScope = scope.CreateChildScope();
			var iterationVariableDefinition = innerScope.CreateOrUpdateVariableDefinition(iterationVariable, errorListener);
			collectionVariableDefinition.CollectionElementVariable = iterationVariableDefinition;
            block.CompileVariableDefinitions(innerScope, errorListener);
		}
	}
}
