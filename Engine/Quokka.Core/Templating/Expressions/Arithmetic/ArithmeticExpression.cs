using System;
using System.Globalization;

namespace Mindbox.Quokka
{
	internal abstract class ArithmeticExpression : Expression
	{
		public abstract TypeDefinition Type { get; }

		public abstract double GetValue(RenderContext renderContext);

		public override TypeDefinition GetResultType(AnalysisContext context)
		{
			return Type;
		}

		public abstract void PerformSemanticAnalysis(AnalysisContext context);

		public sealed override void PerformSemanticAnalysis(
			AnalysisContext context,
			TypeDefinition expectedExpressionType)
		{
			/*
			if (!Type.IsCompatibleWithRequired(expectedExpressionType))
				throw new InvalidOperationException("Type is not compatible");
			*/
			PerformSemanticAnalysis(context);
		}

		protected virtual bool TryGetStaticValue(out double value)
		{
			value = Double.NaN;
			return false;
		}

		public override VariableValueStorage Evaluate(RenderContext renderContext)
		{
			return new PrimitiveVariableValueStorage(NormalizeValue(GetValue(renderContext)));
		}

		public override VariableValueStorage TryGetStaticEvaluationResult()
		{
			return TryGetStaticValue(out double numberValue) 
				? new PrimitiveVariableValueStorage(NormalizeValue(numberValue))
				: null;
		}

		public override string GetOutputValue(RenderContext renderContext)
		{
			var value = NormalizeValue(GetValue(renderContext));

			if (value is decimal decimalValue)
				return Math.Round(decimalValue, 2).ToString();
			else if (value is int intValue)
				return intValue.ToString();
			else
				throw new InvalidOperationException($"The expression result is of unexpected type {value.GetType().Name}");
		}

		private static object NormalizeValue(double value)
		{
			const double Epsilon = 1e-8;

			int intValue = (int)value;

			if (Math.Abs(value - (int)value) < Epsilon)
				return intValue;
			else
				return (decimal)value;
		}
	}
}
