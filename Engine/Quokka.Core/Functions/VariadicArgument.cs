namespace Quokka
{
	public class VariadicArgument<TType> : TemplateFunctionArgument
	{
		public int MinimumOccurrences { get; }

		private readonly ScalarArgument<TType> argument;

		public VariadicArgument(ScalarArgument<TType> argument, int minimumOccurrences = 0) : base(argument.Name)
		{
			MinimumOccurrences = minimumOccurrences;
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