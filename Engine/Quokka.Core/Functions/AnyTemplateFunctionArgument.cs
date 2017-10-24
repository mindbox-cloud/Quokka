namespace Mindbox.Quokka
{
	internal class AnyTemplateFunctionArgument : TemplateFunctionArgument
	{
		public AnyTemplateFunctionArgument(string name) : base(name, true)
		{
		}

		internal override TypeDefinition Type => TypeDefinition.Unknown;

		internal override ArgumentValueValidationResult ValidateConstantValue(VariableValueStorage value)
		{
			return ArgumentValueValidationResult.Valid;
		}
	}
}