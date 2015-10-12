namespace Quokka
{
	internal interface IFunctionArgument
	{
		void CompileVariableDefinitions(SemanticAnalysisContext context, VariableType requiredArgumentType);

		object GetValue(RenderContext renderContext);
	}
}