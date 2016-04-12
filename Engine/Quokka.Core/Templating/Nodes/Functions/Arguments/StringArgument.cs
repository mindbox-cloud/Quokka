namespace Quokka
{
	internal class StringArgument : FunctionArgumentBase
	{
		private readonly string value;
		
		public StringArgument(string value, Location location)
			: base(location)
		{
			this.value = value;
		}

		public override TypeDefinition TryGetStaticType(SemanticAnalysisContext context)
		{
			return TypeDefinition.String;
		}

		public override VariableValueStorage GetValue(RenderContext renderContext)
		{
			return new PrimitiveVariableValueStorage(value);
		}

		public override void CompileVariableDefinitions(SemanticAnalysisContext context, TypeDefinition requiredArgumentType)
		{
			// This node is constant and therefore can't affect semantic analysis context.
		}

		public override bool TryGetStaticValue(out object staticValue)
		{
			staticValue = value;
			return true;
		}
	}
}
