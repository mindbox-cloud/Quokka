using System.Collections.Generic;
using System.Linq;

namespace Quokka
{
	internal class AndExpression : IBooleanExpression
	{
		private readonly IReadOnlyCollection<IBooleanExpression> subExpressions;

		public AndExpression(IEnumerable<IBooleanExpression> subExpressions)
		{
			this.subExpressions = subExpressions.ToList().AsReadOnly();
		}

		public bool Evaluate()
		{
			return subExpressions.All(subExpression => subExpression.Evaluate());
		}
	}
}
