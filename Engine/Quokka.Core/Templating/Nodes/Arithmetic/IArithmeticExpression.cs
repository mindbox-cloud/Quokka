namespace Quokka
{
	internal interface IArithmeticExpression
	{
		double GetValue();

		void CompileVariableDefinitions(Scope scope, ISemanticErrorListener errorListener);
	}
}
