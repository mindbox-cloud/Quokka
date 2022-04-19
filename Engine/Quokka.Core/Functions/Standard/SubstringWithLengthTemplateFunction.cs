namespace Mindbox.Quokka
{
	public class SubstringWithLengthTemplateFunction : ScalarTemplateFunction<string, int, int, string>
	{
		public SubstringWithLengthTemplateFunction()
			: base("substring", 
				new StringFunctionArgument("value"), 
				new IntegerFunctionArgument("startIndex",
					valueValidator: value => value >= 1
						? ArgumentValueValidationResult.Valid
						: new ArgumentValueValidationResult(false, "Substring start index can't be less than 1")),
				new IntegerFunctionArgument("length",
					valueValidator: value => value >= 1
						? ArgumentValueValidationResult.Valid
						: new ArgumentValueValidationResult(false, "Substring length can't be less than 1")))
		{
		}

		public override string Invoke(string value, int startIndex, int length)
		{
			// in templates indexing from 1 is assumed
			return value.Substring(startIndex - 1, length);
		}
	}
}