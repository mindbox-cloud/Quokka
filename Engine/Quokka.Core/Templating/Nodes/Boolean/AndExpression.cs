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

		public override bool Evaluate(RuntimeVariableScope variableScope)
		{
			return subExpressions.All(subExpression => subExpression.Evaluate(variableScope));
		}

		public override void CompileVariableDefinitions(CompilationVariableScope scope)
		{
			foreach (var subExpression in subExpressions)
				subExpression.CompileVariableDefinitions(scope);
		}
	}
}
