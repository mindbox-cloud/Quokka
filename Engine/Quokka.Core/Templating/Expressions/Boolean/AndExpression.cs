using System.Collections.Generic;
using System.Linq;

namespace Mindbox.Quokka
{
	internal class AndExpression : BooleanExpression
	{
		private readonly IReadOnlyCollection<BooleanExpression> subExpressions;

		public AndExpression(IEnumerable<BooleanExpression> subExpressions)
		{
			this.subExpressions = subExpressions.ToList().AsReadOnly();
		}

		public override bool GetBooleanValue(RenderContext renderContext)
		{
			return subExpressions.All(subExpression => subExpression.GetBooleanValue(renderContext));
		}

		public override void PerformSemanticAnalysis(AnalysisContext context)
		{
			foreach (var subExpression in subExpressions)
				subExpression.PerformSemanticAnalysis(context);
		}
	}
}
