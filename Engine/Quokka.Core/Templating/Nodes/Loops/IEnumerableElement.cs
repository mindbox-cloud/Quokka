using System.Collections.Generic;

namespace Quokka
{
	internal interface IEnumerableElement
	{
		void CompileVariableDefinitions(SemanticAnalysisContext context);

		void ProcessIterationVariableUsages(SemanticAnalysisContext context, VariableDefinition iterationVariable);

		IModelDefinition GetEnumerationVariableDeclarationDefinition(SemanticAnalysisContext context);

		IEnumerable<VariableValueStorage> Enumerate(RenderContext context);
	}
}
