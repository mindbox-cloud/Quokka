namespace Quokka
{
	internal interface IBooleanExpression
	{
		bool Evaluate(VariableValueStorage valueStorage);

		void CompileVariableDefinitions(Scope scope, ISemanticErrorListener errorListener);
	}
}
