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
			// "Any" is guaranteed to short-circuit: 
			// "The enumeration of source is stopped as soon as the result can be determined."
			// https://msdn.microsoft.com/ru-ru/library/bb337697(v=vs.110).aspx

			return subExpressions.Any(subExpression => subExpression.Evaluate(variableScope));
		}

		public override void CompileVariableDefinitions(Scope scope, ISemanticErrorListener errorListener)
		{
			foreach (var subExpression in subExpressions)
				subExpression.CompileVariableDefinitions(scope, errorListener);
		}
	}
}
