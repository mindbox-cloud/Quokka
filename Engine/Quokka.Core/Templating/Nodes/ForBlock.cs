using System;
using System.Collections.Generic;
using System.Text;

namespace Quokka
{
	internal class ForBlock : TemplateNodeBase
	{
		private readonly ITemplateNode block;
		private readonly VariableOccurence collection;
		private readonly VariableDeclaration iterationVariable;

		public ForBlock(ITemplateNode block, VariableOccurence collection, VariableDeclaration iterationVariable)
		{
			this.block = block;
			this.collection = collection;
			this.iterationVariable = iterationVariable;
		}

		public override void CompileVariableDefinitions(SemanticAnalysisContext context)
		{
			var collectionVariableDefinition = 
				context.VariableScope.CreateOrUpdateVariableDefinition(collection);
			var innerScope = context.VariableScope.CreateChildScope();
			var iterationVariableDefinition = innerScope.CreateOrUpdateVariableDefinition(iterationVariable);
			collectionVariableDefinition.AddCollectionElementVariable(iterationVariableDefinition);
			block?.CompileVariableDefinitions(
				new SemanticAnalysisContext(innerScope, context.Functions, context.ErrorListener));
		}

		public override void Render(StringBuilder resultBuilder, RenderContext context)
		{
			if (block == null)
				return;

			var collectionValue = (IEnumerable<VariableValueStorage>)context.VariableScope.GetVariableValue(collection);
			foreach (var collectionElement in collectionValue)
			{
				var innerScope =
					context.VariableScope.CreateChildScope(
						VariableValueStorage.CreateCompositeStorage(iterationVariable.Name, collectionElement));

				block.Render(resultBuilder, new RenderContext(innerScope, context.Functions));
			}
		}
	}
}
