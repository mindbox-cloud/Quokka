namespace Mindbox.Quokka
{
	internal interface IArithmeticExpression : IExpression
	{
		TypeDefinition Type { get; }

		double GetValue(RenderContext renderContext);

		void CompileVariableDefinitions(SemanticAnalysisContext context);

		bool TryGetStaticValue(out object value);
	}
}
