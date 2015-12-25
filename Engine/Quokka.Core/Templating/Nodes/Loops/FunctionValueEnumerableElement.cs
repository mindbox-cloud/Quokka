using System;
using System.Collections.Generic;

namespace Quokka
{
	internal class FunctionValueEnumerableElement : EnumerableElementBase
	{
		private readonly FunctionCall functionCall;

		public FunctionValueEnumerableElement(FunctionCall functionCall)
		{
			this.functionCall = functionCall;
		}
		
		public override void CompileVariableDefinitions(SemanticAnalysisContext context)
		{
			functionCall.CompileVariableDefinitions(context);
		}

		public override void ProcessIterationVariableUsages(SemanticAnalysisContext context, VariableDefinition iterationVariable)
		{
			functionCall.MapArgumentVariableDefinitionsToResult(context, iterationVariable);
		}

		public override IEnumerable<VariableValueStorage> Enumerate(RenderContext context)
		{
			return functionCall.GetInvocationResult(context).GetElements();
		}
	}
}
