namespace Quokka
{
	public class VariadicArgument<TType> : TemplateFunctionArgument
	{
		private readonly ScalarArgument<TType> argument;

		public VariadicArgument(ScalarArgument<TType> argument) : base(argument.Name)
		{
			this.argument = argument;
		}

		internal override TypeDefinition Type => argument.Type;

		internal override ArgumentValueValidationResult ValidateValue(VariableValueStorage value)
		{
			return argument.ValidateValue(value);
		}

		internal TType ConvertValue(VariableValueStorage value)
		{
			return argument.ConvertValue(value);
		}
	}
}