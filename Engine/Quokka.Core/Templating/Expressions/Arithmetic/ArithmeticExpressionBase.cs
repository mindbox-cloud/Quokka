using System;
using System.Globalization;

namespace Mindbox.Quokka
{
	internal abstract class ArithmeticExpressionBase : ExpressionBase, IArithmeticExpression
	{
		public abstract TypeDefinition Type { get; }

		public abstract double GetValue(RenderContext renderContext);

		public override TypeDefinition GetResultType(SemanticAnalysisContext context)
		{
			return Type;
		}

		public abstract void CompileVariableDefinitions(SemanticAnalysisContext context);

		public sealed override void CompileVariableDefinitions(
			SemanticAnalysisContext context,
			TypeDefinition expectedExpressionType)
		{
			/*
			if (!Type.IsCompatibleWithRequired(expectedExpressionType))
				throw new InvalidOperationException("Type is not compatible");
			*/
			CompileVariableDefinitions(context);
		}

		public virtual bool TryGetStaticValue(out object value)
		{
			value = null;
			return false;
		}

		public override VariableValueStorage Evaluate(RenderContext renderContext)
		{
			const double Epsilon = 1e-8;

			double value = GetValue(renderContext);
			int intValue = (int)value;

			if (Math.Abs(value - (int)value) < Epsilon)
				return new PrimitiveVariableValueStorage(intValue);
			else
				return new PrimitiveVariableValueStorage(value);
		}

		public override VariableValueStorage TryGetStaticEvaluationResult()
		{
			return TryGetStaticValue(out object numberValue) 
				? new PrimitiveVariableValueStorage(numberValue) 
				: null;
		}
	}
}
