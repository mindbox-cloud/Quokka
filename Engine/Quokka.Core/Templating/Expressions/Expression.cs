using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mindbox.Quokka
{
    internal abstract class Expression : IExpression
	{
		public abstract VariableValueStorage TryGetStaticEvaluationResult();

		public abstract VariableValueStorage Evaluate(RenderContext renderContext);

		public abstract TypeDefinition GetResultType(AnalysisContext context);

		public abstract void PerformSemanticAnalysis(AnalysisContext context, TypeDefinition expectedExpressionType);

		public virtual string GetOutputValue(RenderContext renderContext)
		{
			return Evaluate(renderContext).GetPrimitiveValue().ToString();
		}
	}
}
