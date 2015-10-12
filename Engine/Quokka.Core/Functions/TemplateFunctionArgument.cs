using System;

namespace Quokka
{
	public sealed class TemplateFunctionArgument<TType> : TemplateFunctionArgument
	{
		internal override Type RuntimeType => typeof(TType);
		private readonly Func<TType, ArgumentValueValidationResult> valueValidator;

		public TemplateFunctionArgument(string name, Func<TType, ArgumentValueValidationResult> valueValidator = null) 
			:base(name)
		{
			this.valueValidator = valueValidator;
		}

		internal override ArgumentValueValidationResult ValidateValue(object value)
		{
			if (valueValidator != null)
				return valueValidator((TType)value);

			return new ArgumentValueValidationResult(true, null);
		}
	}

	public abstract class TemplateFunctionArgument
	{
		public string Name { get; }
		internal abstract Type RuntimeType { get; }

		protected TemplateFunctionArgument(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
				throw new ArgumentException("Argument name should not be null or blank", nameof(name));

			Name = name;
		}

		internal abstract ArgumentValueValidationResult ValidateValue(object value);
	}

	public sealed class ArgumentValueValidationResult
	{
		public bool IsValid { get; }
		public string ErrorMessage { get; }

		public ArgumentValueValidationResult(bool isValid, string errorMessage = null)
		{
			if (isValid != (errorMessage == null))
				throw new ArgumentException("Error message must be specified when the value is not valid", nameof(errorMessage));

			IsValid = isValid;
			ErrorMessage = errorMessage;
		}
	}
}
