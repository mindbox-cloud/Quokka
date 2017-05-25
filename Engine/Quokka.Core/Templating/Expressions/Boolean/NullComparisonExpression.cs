using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mindbox.Quokka
{
	internal class NullComparisonExpression : BooleanExpressionBase
	{
		private readonly VariantValueExpression variantValueExpression;
		private readonly ComparisonOperation comparisonOperation;

		public NullComparisonExpression(VariantValueExpression variantValueExpression, ComparisonOperation comparisonOperation)
		{
			if (comparisonOperation != ComparisonOperation.Equals && comparisonOperation != ComparisonOperation.NotEquals)
				throw new ArgumentOutOfRangeException(nameof(comparisonOperation));
			
			this.variantValueExpression = variantValueExpression;
			this.comparisonOperation = comparisonOperation;
		}

		public override bool GetBooleanValue(RenderContext renderContext)
		{
			bool isValueNull = variantValueExpression.CheckIfExpressionIsNull(renderContext);

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
			variantValueExpression.CompileVariableDefinitions(context, TypeDefinition.Unknown);
		}
	}
}
