using System;

namespace Mindbox.Quokka
{
	internal class NumberExpression : ArithmeticExpression
	{
		private readonly double number;

		public NumberExpression(double number)
		{
			this.number = number;
		}
		
		public override TypeDefinition GetResultType(AnalysisContext context)
		{
			return Math.Abs(number % 1) < Double.Epsilon
						? TypeDefinition.Integer
						: TypeDefinition.Decimal;
		}

		public override bool CheckIfExpressionIsNull(RenderContext renderContext)
		{
			return false;
		}

		public override double GetValue(RenderContext renderContext)
		{
			return number;
		}

		public override void PerformSemanticAnalysis(AnalysisContext context)
		{
			// This node is constant and therefore can't affect semantic analysis context.
		}

		protected override bool TryGetStaticValue(out double value)
		{
			value = number;
			return true;
		}
	}
}