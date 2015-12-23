using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quokka
{
	internal class FunctionValueEnumerableElement : EnumerableElementBase
	{
		private readonly FunctionCall functionCall;

		public FunctionValueEnumerableElement(FunctionCall functionCall)
		{
			this.functionCall = functionCall;
		}

		public override void CompileVariableDefinitions(SemanticAnalysisContext context, VariableDefinition iterationVariable)
		{
			functionCall.CompileVariableDefinitions(context);
		}

		public override IEnumerable<VariableValueStorage> Enumerate(RenderContext context)
		{
			return functionCall.GetInvocationResult(context).GetElements();
		}
	}
}
