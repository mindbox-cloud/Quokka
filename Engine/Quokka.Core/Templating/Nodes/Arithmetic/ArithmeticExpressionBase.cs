namespace Mindbox.Quokka
{
	internal abstract class ArithmeticExpressionBase : IArithmeticExpression
	{
		public abstract TypeDefinition Type { get; }

		public abstract double GetValue(RenderContext renderContext);

		public abstract void CompileVariableDefinitions(SemanticAnalysisContext context);

		public virtual bool TryGetStaticValue(out object value)
		{
			value = null;
			return false;
		}
	}
}
