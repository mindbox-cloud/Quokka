using System;

namespace Mindbox.Quokka
{
	internal abstract class BooleanExpressionBase : ExpressionBase, IBooleanExpression
	{
		public abstract bool GetBooleanValue(RenderContext renderContext);

		public override TypeDefinition GetResultType(SemanticAnalysisContext context)
		{
			return TypeDefinition.Boolean;
		}

		public override VariableValueStorage Evaluate(RenderContext renderContext)
		{
			return new PrimitiveVariableValueStorage(GetBooleanValue(renderContext));
		}

		public sealed override void CompileVariableDefinitions(SemanticAnalysisContext context, TypeDefinition expectedExpressionType)
		{
			if (!TypeDefinition.Boolean.IsAssignableTo(expectedExpressionType))
				throw new InvalidOperationException("Type is not compatible");

			CompileVariableDefinitions(context);
		}

		public abstract void CompileVariableDefinitions(SemanticAnalysisContext context);

		public override VariableValueStorage TryGetStaticEvaluationResult()
		{
			return null;
		}
	}
}
