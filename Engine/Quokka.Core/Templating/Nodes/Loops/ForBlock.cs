using System.Text;

namespace Quokka
{
	internal class ForBlock : TemplateNodeBase
	{
		private readonly ITemplateNode block;
		private readonly IEnumerableElement enumerableElement;
		private readonly VariableDeclaration iterationVariable;

		public ForBlock(ITemplateNode block, VariableDeclaration iterationVariable, IEnumerableElement enumerableElement)
		{
			this.block = block;
			this.iterationVariable = iterationVariable;
			this.enumerableElement = enumerableElement;
		}

		public override void CompileVariableDefinitions(SemanticAnalysisContext context)
		{
			var innerScope = context.VariableScope.CreateChildScope();
			
			var iterationVariableDefinition = innerScope.DeclareVariable(iterationVariable);

			enumerableElement.CompileVariableDefinitions(context);
			block?.CompileVariableDefinitions(
				new SemanticAnalysisContext(innerScope, context.Functions, context.ErrorListener));
			enumerableElement.ProcessIterationVariableUsages(context, iterationVariableDefinition);
			
			iterationVariableDefinition.ValidateAgainstExpectedModelDefinition(
				enumerableElement.GetEnumerationVariableDeclarationDefinition(context),
				context.ErrorListener);
		}

		public override void Render(StringBuilder resultBuilder, RenderContext context)
		{
			if (block == null)
				return;

			var collectionValue = enumerableElement.Enumerate(context);
			foreach (var collectionElement in collectionValue)
			{
				var innerScope =
					context.VariableScope.CreateChildScope(
						new CompositeVariableValueStorage(iterationVariable.Name, collectionElement));

				block.Render(resultBuilder, context.CreateInnerContext(innerScope));
			}
		}

		public override void CompileGrammarSpecificData(GrammarSpecificDataAnalysisContext context)
		{
			block?.CompileGrammarSpecificData(context);
		}
	}
}
