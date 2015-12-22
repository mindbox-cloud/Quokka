using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quokka
{
	internal interface IEnumerableElement
	{
		void CompileVariableDefinitions(SemanticAnalysisContext context, VariableDefinition iterationVariable);

		IEnumerable<VariableValueStorage> Enumerate(RenderContext context);
	}
}
