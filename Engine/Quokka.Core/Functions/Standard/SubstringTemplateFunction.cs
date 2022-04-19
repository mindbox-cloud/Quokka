namespace Mindbox.Quokka
{
	public class SubstringTemplateFunction : ScalarTemplateFunction<string, int, string>
	{
		public SubstringTemplateFunction() 
			: base("substring", 
				  new StringFunctionArgument("value"), 
				  new IntegerFunctionArgument("startIndex", 
					  valueValidator: value => value >= 1 
						? ArgumentValueValidationResult.Valid
						: new ArgumentValueValidationResult(false, "Substring start index can't be less than 1")))
		{
		}

		public override string Invoke(string value, int startIndex)
		{
			// in templates indexing from 1 is assumed
			return value.Substring(startIndex - 1);
		}
	}
}