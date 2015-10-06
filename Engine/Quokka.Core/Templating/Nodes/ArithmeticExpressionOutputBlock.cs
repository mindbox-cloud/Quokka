using System;
using System.Text;

namespace Quokka
{
	internal class ArithmeticExpressionOutputBlock : TemplateNodeBase
	{
		private const double epsilon = 1e-8;
		private readonly IArithmeticExpression expression;

		public ArithmeticExpressionOutputBlock(IArithmeticExpression expression)
		{
			this.expression = expression;
		}

		public override void CompileVariableDefinitions(Scope scope, ISemanticErrorListener errorListener)
		{
			expression.CompileVariableDefinitions(scope, errorListener);
		}

		public override void Render(StringBuilder resultBuilder, VariableValueStorage valueStorage)
		{
			double value = expression.GetValue(valueStorage);

			if (Math.Abs(value - (int)value) < epsilon)
				resultBuilder.Append((int)value);
			else
				resultBuilder.AppendFormat("{0:0.00}", value);
		}
	}
}
