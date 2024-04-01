// // Copyright 2022 Mindbox Ltd
// //
// // Licensed under the Apache License, Version 2.0 (the "License");
// // you may not use this file except in compliance with the License.
// // You may obtain a copy of the License at
// //
// //     http://www.apache.org/licenses/LICENSE-2.0
// //
// // Unless required by applicable law or agreed to in writing, software
// // distributed under the License is distributed on an "AS IS" BASIS,
// // WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// // See the License for the specific language governing permissions and
// // limitations under the License.

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
			var value = NormalizeValue(GetValue(renderContext));

			if (value is decimal decimalValue)
				return Math.Round(decimalValue, 2).ToString(renderContext.Settings.CultureInfo);
			else if (value is int intValue)
				return intValue.ToString(renderContext.Settings.CultureInfo);
			else
				throw new InvalidOperationException($"The expression result is of unexpected type {value.GetType().Name}");
		}

		public sealed override void RegisterAssignmentToVariable(
			AnalysisContext context,
			ValueUsageSummary destinationVariable)
		{
			// do nothing
		}

		private static object NormalizeValue(double value)
		{
			try
			{
				const double Epsilon = 1e-8;

				int intValue = (int)value;

				if (Math.Abs(value - (int)value) < Epsilon)
					return intValue;
				else
					return (decimal)value;
			}
			catch (OverflowException ex)
			{
				throw new UnrenderableTemplateModelException("Arithmetic operation result could not be evaluated", ex, null);
			}
		}
	}
}
