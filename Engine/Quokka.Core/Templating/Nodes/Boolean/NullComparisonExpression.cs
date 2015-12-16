using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quokka
{
	internal class NullComparisonExpression : BooleanExpressionBase
	{
		private readonly VariableOccurence variableOccurence;
		private readonly ComparisonOperation comparisonOperation;

		public NullComparisonExpression(VariableOccurence variableOccurence, ComparisonOperation comparisonOperation)
		{
			if (comparisonOperation != ComparisonOperation.Equals && comparisonOperation != ComparisonOperation.NotEquals)
				throw new ArgumentOutOfRangeException(nameof(comparisonOperation));

			this.variableOccurence = variableOccurence;
			this.comparisonOperation = comparisonOperation;
		}

		public override bool Evaluate(RenderContext renderContext)
		{
			bool isValueNull = renderContext.VariableScope.CheckIfVariableIsNull(variableOccurence);

			switch (comparisonOperation)
			{
				case ComparisonOperation.Equals:
					return isValueNull;
				case ComparisonOperation.NotEquals:
					return !isValueNull;
				default:
					throw new InvalidOperationException("Unsupported comparison operation");
			}
		}

		public override void CompileVariableDefinitions(SemanticAnalysisContext context)
		{
			context.VariableScope.CreateOrUpdateVariableDefinition(variableOccurence);
		}
	}
}
