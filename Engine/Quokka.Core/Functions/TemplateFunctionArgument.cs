using System;

namespace Quokka
{
	public abstract class TemplateFunctionArgument<TType> : TemplateFunctionArgument
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

		internal virtual TType ConvertValue(VariableValueStorage value)
		{
			return (TType)value.GetPrimitiveValue();
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

	public sealed class DecimalFunctionArgument : TemplateFunctionArgument<decimal>
	{
		public DecimalFunctionArgument(string name, Func<decimal, ArgumentValueValidationResult> valueValidator = null)
			: base(name, valueValidator)
		{
		}

		internal override decimal ConvertValue(VariableValueStorage value)
		{
			if (value == null)
				throw new ArgumentNullException(nameof(value));

			return Convert.ToDecimal(value.GetPrimitiveValue());
		}
	}

	public sealed class StringFunctionArgument : TemplateFunctionArgument<string>
	{
		public StringFunctionArgument(string name, Func<string, ArgumentValueValidationResult> valueValidator = null)
			: base(name, valueValidator)
		{
		}
	}

	public sealed class DateTimeFunctionArgument : TemplateFunctionArgument<DateTime>
	{
		public DateTimeFunctionArgument(string name, Func<DateTime, ArgumentValueValidationResult> valueValidator = null)
			: base(name, valueValidator)
		{
		}
	}

	public sealed class BoolFunctionArgument : TemplateFunctionArgument<bool>
	{
		public BoolFunctionArgument(string name, Func<bool, ArgumentValueValidationResult> valueValidator = null)
			: base(name, valueValidator)
		{
		}
	}

	public sealed class TimeSpanFunctionArgument : TemplateFunctionArgument<TimeSpan>
	{
		public TimeSpanFunctionArgument(string name, Func<TimeSpan, ArgumentValueValidationResult> valueValidator = null)
			: base(name, valueValidator)
		{
		}
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
