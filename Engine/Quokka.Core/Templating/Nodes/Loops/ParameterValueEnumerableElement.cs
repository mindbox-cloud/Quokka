using System;
using System.Collections.Generic;

namespace Quokka
{
	internal class ParameterValueEnumerableElement : EnumerableElementBase
	{
		private readonly VariableOccurence collectionVariable;

		public ParameterValueEnumerableElement(VariableOccurence collectionVariable)
		{
			this.collectionVariable = collectionVariable;
		}

		public override void CompileVariableDefinitions(SemanticAnalysisContext context)
		{
			context.VariableScope.CreateOrUpdateVariableDefinition(collectionVariable);
		}

		public override void ProcessIterationVariableUsages(SemanticAnalysisContext context, VariableDefinition iterationVariable)
		{
			var collectionVariableDefinition = context.VariableScope.TryGetVariableDefinition(collectionVariable);
			if (collectionVariableDefinition == null)
				throw new NotImplementedException("Variable definition for collection variable not found");

			collectionVariableDefinition.AddCollectionElementVariable(iterationVariable);
		}

		public override IModelDefinition GetEnumerationVariableDeclarationDefinition(SemanticAnalysisContext context)
		{
			return new PrimitiveModelDefinition(TypeDefinition.Unknown);
		}

		public override IEnumerable<VariableValueStorage> Enumerate(RenderContext context)
		{
			return context.VariableScope.GetVariableValueCollection(collectionVariable);
		}
	}
}
