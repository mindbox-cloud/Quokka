namespace Quokka
{
	internal interface IBooleanExpression
	{
		bool Evaluate(RuntimeVariableScope variableScope);

		void CompileVariableDefinitions(CompilationVariableScope scope);
	}
}
