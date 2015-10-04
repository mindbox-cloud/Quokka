namespace Quokka
{
	internal interface ITemplateNode
	{
		void CompileVariableDefinitions(Scope scope, ISemanticErrorListener errorListener);
	}
}
