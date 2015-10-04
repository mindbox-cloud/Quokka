namespace Quokka
{
	internal interface IBooleanExpression
	{
		bool Evaluate();

		void CompileVariableDefinitions(Scope scope, ISemanticErrorListener errorListener);
	}
}
