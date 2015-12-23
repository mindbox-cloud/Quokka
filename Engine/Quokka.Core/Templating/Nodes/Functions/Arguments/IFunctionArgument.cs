namespace Quokka
{
	internal interface IFunctionArgument
	{
		void CompileVariableDefinitions(SemanticAnalysisContext context, TypeDefinition requiredArgumentType);

		/// <summary>
		/// Try to get argument value if it's static (independent of parameter values and other factors).
		/// </summary>
		/// <param name="staticValue"></param>
		/// <returns><c>True</c>, if the value is known at compile-time, otherwise <c>False</c>.</returns>
		/// <remarks>
		/// We can use this value to perform some validation at compile-time rather than at runtime and give semantic errors
		/// after compilation.
		/// </remarks>
		bool TryGetStaticValue(out object staticValue);

		VariableValueStorage GetValue(RenderContext renderContext);
	}
}