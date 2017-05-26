﻿using System.Collections.Generic;
using System.Linq;

namespace Mindbox.Quokka
{
	internal class AndExpression : BooleanExpressionBase
	{
		private readonly IReadOnlyCollection<IBooleanExpression> subExpressions;

		public AndExpression(IEnumerable<IBooleanExpression> subExpressions)
		{
			this.subExpressions = subExpressions.ToList().AsReadOnly();
		}

		public override bool GetBooleanValue(RenderContext renderContext)
		{
			return subExpressions.All(subExpression => subExpression.GetBooleanValue(renderContext));
		}

		public override void CompileVariableDefinitions(SemanticAnalysisContext context)
		{
			foreach (var subExpression in subExpressions)
				subExpression.CompileVariableDefinitions(context);
		}
	}
}