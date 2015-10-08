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

		public override void CompileVariableDefinitions(Scope scope, ISemanticErrorListener errorListener)
		{
			var collectionVariableDefinition = 
				scope.CreateOrUpdateVariableDefinition(collection, errorListener);
			var innerScope = scope.CreateChildScope();
			var iterationVariableDefinition = innerScope.CreateOrUpdateVariableDefinition(iterationVariable, errorListener);
			collectionVariableDefinition.CollectionElementVariables.Add(iterationVariableDefinition);
			block?.CompileVariableDefinitions(innerScope, errorListener);
		}

		public override void Render(StringBuilder resultBuilder, RuntimeVariableScope variableScope)
		{
			if (block == null)
				return;

			var collectionValue = (IEnumerable<VariableValueStorage>)variableScope.GetVariableValue(collection);
			foreach (var collectionElement in collectionValue)
			{
				var innerScope = new RuntimeVariableScope(
					VariableValueStorage.CreateCompositeStorage(iterationVariable.Name, collectionElement),
					variableScope);

				block.Render(resultBuilder, innerScope);
			}
		}
	}
}
