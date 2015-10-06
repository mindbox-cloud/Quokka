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

		public override bool Evaluate(VariableValueStorage valueStorage)
		{
			return subExpressions.All(subExpression => subExpression.Evaluate(valueStorage));
		}

		public override void CompileVariableDefinitions(Scope scope, ISemanticErrorListener errorListener)
		{
			foreach (var subExpression in subExpressions)
				subExpression.CompileVariableDefinitions(scope, errorListener);
		}
	}
}
