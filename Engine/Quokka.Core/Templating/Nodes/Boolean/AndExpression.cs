using System.Collections.Generic;
using System.Linq;

namespace Quokka
{
	internal class AndExpression : BooleanExpressionBase
	{
		private readonly IReadOnlyCollection<IBooleanExpression> subExpressions;

		public AndExpression(IEnumerable<IBooleanExpression> subExpressions)
		{
			this.subExpressions = subExpressions.ToList().AsReadOnly();
		}

		public override bool Evaluate(RenderContext renderContext)
		{
			return subExpressions.All(subExpression => subExpression.Evaluate(renderContext));
		}

		public override void CompileVariableDefinitions(SemanticAnalysisContext context)
		{
			foreach (var subExpression in subExpressions)
				subExpression.CompileVariableDefinitions(context);
		}
	}
}
