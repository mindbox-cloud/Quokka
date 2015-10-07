using System.Collections.Generic;
using System.Linq;

namespace Quokka
{
	internal class OrExpression : BooleanExpressionBase
	{
		private readonly IReadOnlyCollection<IBooleanExpression> subExpressions;

		public OrExpression(IEnumerable<IBooleanExpression> subExpressions)
		{
			this.subExpressions = subExpressions.ToList().AsReadOnly();
		}

		public override bool Evaluate(RuntimeVariableScope variableScope)
		{
			return subExpressions.Any(subExpression => subExpression.Evaluate(variableScope));
		}

		public override void CompileVariableDefinitions(Scope scope, ISemanticErrorListener errorListener)
		{
			foreach (var subExpression in subExpressions)
				subExpression.CompileVariableDefinitions(scope, errorListener);
		}
	}
}
