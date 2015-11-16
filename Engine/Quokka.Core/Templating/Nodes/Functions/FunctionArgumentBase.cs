namespace Quokka
{
	internal abstract class FunctionArgumentBase : IFunctionArgument
	{
		public Location Location { get; }

		protected FunctionArgumentBase(Location location)
		{
			Location = location;
		}

		public abstract void CompileVariableDefinitions(SemanticAnalysisContext context, TypeDefinition requiredArgumentType);

		public virtual bool TryGetStaticValue(out object staticValue)
		{
			staticValue = null;
			return false;
		}

		public abstract object GetValue(RenderContext renderContext);
	}
}
