namespace Quokka
{
	internal abstract class TemplateNodeBase : ITemplateNode
	{
		public virtual void CompileVariableDefinitions(Scope scope, ISemanticErrorListener errorListener)
		{
		}
	}
}
