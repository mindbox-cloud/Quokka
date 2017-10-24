using System.Collections.Generic;
using System.Linq;

namespace Mindbox.Quokka
{
	internal class OrExpression : BooleanExpression
	{
		private readonly IReadOnlyCollection<BooleanExpression> subExpressions;

		public OrExpression(IEnumerable<BooleanExpression> subExpressions)
		{
			this.subExpressions = subExpressions.ToList().AsReadOnly();
		}

		public override bool GetBooleanValue(RenderContext renderContext)
		{
			// "Any" is guaranteed to short-circuit: 
			// "The enumeration of source is stopped as soon as the result can be determined."
			// https://msdn.microsoft.com/ru-ru/library/bb337697(v=vs.110).aspx

			return subExpressions.Any(subExpression => subExpression.GetBooleanValue(renderContext));
		}

		public override void PerformSemanticAnalysis(AnalysisContext context)
		{
			foreach (var subExpression in subExpressions)
				subExpression.PerformSemanticAnalysis(context);
		}

		public override bool CheckIfExpressionIsNull(RenderContext renderContext)
		{
			return subExpressions.Any(expression => expression.CheckIfExpressionIsNull(renderContext));
		}
	}
}
