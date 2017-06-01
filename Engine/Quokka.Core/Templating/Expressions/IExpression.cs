using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mindbox.Quokka
{
	internal interface IExpression
	{
		VariableValueStorage TryGetStaticEvaluationResult();

		VariableValueStorage Evaluate(RenderContext renderContext);

		TypeDefinition GetResultType(AnalysisContext context);

		void PerformSemanticAnalysis(AnalysisContext context, TypeDefinition expectedExpressionType);

		string GetOutputValue(RenderContext context);
	}
}
