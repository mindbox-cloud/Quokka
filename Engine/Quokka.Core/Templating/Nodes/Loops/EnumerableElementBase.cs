using System.Collections.Generic;

namespace Quokka
{
	internal abstract class EnumerableElementBase : IEnumerableElement
	{
		public abstract void CompileVariableDefinitions(SemanticAnalysisContext context);

		public abstract void ProcessIterationVariableUsages(SemanticAnalysisContext context, VariableDefinition iterationVariable);

		public abstract IModelDefinition GetEnumerationVariableDeclarationDefinition(SemanticAnalysisContext context);

		public abstract IEnumerable<VariableValueStorage> Enumerate(RenderContext context);
	}
}
