namespace Quokka
{
	internal interface IArithmeticExpression
	{
		double GetValue(RuntimeVariableScope variableScope);

		void CompileVariableDefinitions(CompilationVariableScope scope);
	}
}
