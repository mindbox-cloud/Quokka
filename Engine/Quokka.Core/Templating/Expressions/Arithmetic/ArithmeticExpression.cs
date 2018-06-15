using System;
using System.Globalization;

namespace Mindbox.Quokka
{
	internal abstract class ArithmeticExpression : Expression
	{
		public abstract double GetValue(RenderContext renderContext);

		public abstract void PerformSemanticAnalysis(AnalysisContext context);

		public sealed override void PerformSemanticAnalysis(
			AnalysisContext context,
			TypeDefinition expectedExpressionType)
		{
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
			try
			{
				var value = NormalizeValue(GetValue(renderContext));

				if (value is decimal decimalValue)
					return Math.Round(decimalValue, 2).ToString(CultureInfo.CurrentCulture);
				else if (value is int intValue)
					return intValue.ToString();
				else
					throw new InvalidOperationException($"The expression result is of unexpected type {value.GetType().Name}");
			}
			catch (OverflowException ex)
			{
				throw new UnrenderableTemplateModelException("Arithmetic operation result could not be evaluated", ex, null);
			}
		}

		public sealed override void RegisterAssignmentToVariable(
			AnalysisContext context,
			ValueUsageSummary destinationVariable)
		{
			// do nothing
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
