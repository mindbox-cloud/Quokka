namespace Quokka
{
	internal abstract class FunctionArgumentBase : IFunctionArgument
	{
		public virtual void CompileVariableDefinitions(SemanticAnalysisContext context, VariableType requiredArgumentType)
		{
		}

		public abstract object GetValue(RenderContext renderContext);
	}
}
