using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mindbox.Quokka
{
	internal class StringConcatenationExpression : StringExpression
	{
		private readonly IExpression firstOperand;
		private readonly IExpression secondOperand;

		public StringConcatenationExpression(
			IExpression firstOperand, 
			IExpression secondOperand)
		{
			if (firstOperand == null)
				throw new ArgumentNullException(nameof(firstOperand));
			if (secondOperand == null)
				throw new ArgumentNullException(nameof(secondOperand));

			this.firstOperand = firstOperand;
			this.secondOperand = secondOperand;
		}

		public override bool CheckIfExpressionIsNull(RenderContext renderContext)
		{
			return false;
		}

		public override VariableValueStorage Evaluate(RenderContext renderContext)
		{
			return new PrimitiveVariableValueStorage(
				firstOperand.GetOutputValue(renderContext) +
				secondOperand.GetOutputValue(renderContext));
		}

		public override void PerformSemanticAnalysis(AnalysisContext context, TypeDefinition expectedExpressionType)
		{
			firstOperand.PerformSemanticAnalysis(context, TypeDefinition.Primitive);
			secondOperand.PerformSemanticAnalysis(context, TypeDefinition.Primitive);
		}

		public override VariableValueStorage TryGetStaticEvaluationResult()
		{
			return null;
		}
	}
}
