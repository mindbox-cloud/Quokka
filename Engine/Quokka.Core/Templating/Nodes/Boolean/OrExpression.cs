using System.Collections.Generic;
using System.Linq;

namespace Quokka
{
	internal class OrExpression : IBooleanExpression
	{
		private readonly IReadOnlyCollection<IBooleanExpression> subExpressions;

		public OrExpression(IEnumerable<IBooleanExpression> subExpressions)
		{
			this.subExpressions = subExpressions.ToList().AsReadOnly();
		}

		public bool Evaluate()
		{
			return subExpressions.Any(subExpression => subExpression.Evaluate());
		}
	}
}
