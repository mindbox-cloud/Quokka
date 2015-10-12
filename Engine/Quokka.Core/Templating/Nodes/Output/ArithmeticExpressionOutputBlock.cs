using System;
using System.Globalization;
using System.Text;

namespace Quokka
{
	internal class ArithmeticExpressionOutputBlock : TemplateNodeBase, IOutputBlock
	{
		private const double Epsilon = 1e-8;
		private readonly IArithmeticExpression expression;

		public ArithmeticExpressionOutputBlock(IArithmeticExpression expression)
		{
			this.expression = expression;
		}

		public override void CompileVariableDefinitions(SemanticAnalysisContext context)
		{
			expression.CompileVariableDefinitions(context);
		}

		public override void Render(StringBuilder resultBuilder, RenderContext context)
		{
			double value = expression.GetValue(context);

			if (Math.Abs(value - (int)value) < Epsilon)
				resultBuilder.Append((int)value);
			else
				resultBuilder.Append(value.ToString("0.00", CultureInfo.CurrentCulture));
		}
	}
}
