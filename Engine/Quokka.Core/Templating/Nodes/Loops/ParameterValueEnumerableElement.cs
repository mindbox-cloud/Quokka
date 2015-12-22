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

		public override void CompileVariableDefinitions(SemanticAnalysisContext context, VariableDefinition iterationVariable)
		{
			var collectionVariableDefinition = context.VariableScope.CreateOrUpdateVariableDefinition(collectionVariable);
			collectionVariableDefinition.AddCollectionElementVariable(iterationVariable);
		}

		public override IEnumerable<VariableValueStorage> Enumerate(RenderContext context)
		{
			return context.VariableScope.GetVariableValueCollection(collectionVariable);
		}
	}
}
