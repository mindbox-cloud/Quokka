using System.Collections.Generic;

namespace Quokka
{
	internal abstract class EnumerableElementBase : IEnumerableElement
	{
		public abstract void CompileVariableDefinitions(SemanticAnalysisContext context, VariableDefinition iterationVariable);

		public abstract IEnumerable<VariableValueStorage> Enumerate(RenderContext context);
	}
}
