﻿namespace Quokka
{
	internal abstract class BooleanExpressionBase : IBooleanExpression
	{
		public abstract bool Evaluate(RuntimeVariableScope variableScope);

		public virtual void CompileVariableDefinitions(CompilationVariableScope scope)
		{
		}
	}
}
