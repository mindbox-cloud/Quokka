namespace Quokka
{
	internal interface IArithmeticExpression
	{
		double GetValue(VariableValueStorage valueStorage);

		void CompileVariableDefinitions(Scope scope, ISemanticErrorListener errorListener);
	}
}
