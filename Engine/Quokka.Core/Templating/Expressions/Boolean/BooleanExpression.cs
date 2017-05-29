using System;

namespace Mindbox.Quokka
{
	internal abstract class BooleanExpression : Expression
	{
		public abstract bool GetBooleanValue(RenderContext renderContext);

		public override TypeDefinition GetResultType(AnalysisContext context)
		{
			return TypeDefinition.Boolean;
		}

		public override VariableValueStorage Evaluate(RenderContext renderContext)
		{
			return new PrimitiveVariableValueStorage(GetBooleanValue(renderContext));
		}

		public sealed override void PerformSemanticAnalysis(AnalysisContext context, TypeDefinition expectedExpressionType)
		{
			if (!TypeDefinition.Boolean.IsAssignableTo(expectedExpressionType))
				throw new InvalidOperationException("Type is not compatible");

			PerformSemanticAnalysis(context);
		}

		public abstract void PerformSemanticAnalysis(AnalysisContext context);

		public override VariableValueStorage TryGetStaticEvaluationResult()
		{
			return null;
		}
	}
}
