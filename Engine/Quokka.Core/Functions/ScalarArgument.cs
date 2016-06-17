using System;

namespace Quokka
{
	public abstract class ScalarArgument<TType> : TemplateFunctionArgument
	{
		internal override TypeDefinition Type => TypeDefinition.GetTypeDefinitionByRuntimeType(typeof(TType));

		private readonly Func<TType, ArgumentValueValidationResult> valueValidator;

		protected ScalarArgument(string name, Func<TType, ArgumentValueValidationResult> valueValidator = null) 
			:base(name)
		{
			this.valueValidator = valueValidator;
		}

		internal override ArgumentValueValidationResult ValidateValue(VariableValueStorage value)
		{
			return valueValidator != null 
				? valueValidator(ConvertValue(value)) 
				: ArgumentValueValidationResult.Valid;
		}

		internal virtual TType ConvertValue(VariableValueStorage value)
		{
			return (TType)value.GetPrimitiveValue();
		}
	}

	public sealed class DecimalFunctionArgument : ScalarArgument<decimal>
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

	public sealed class StringFunctionArgument : ScalarArgument<string>
	{
		public StringFunctionArgument(string name, Func<string, ArgumentValueValidationResult> valueValidator = null)
			: base(name, valueValidator)
		{
		}
	}

	public sealed class DateTimeFunctionArgument : ScalarArgument<DateTime>
	{
		public DateTimeFunctionArgument(string name, Func<DateTime, ArgumentValueValidationResult> valueValidator = null)
			: base(name, valueValidator)
		{
		}
	}

	public sealed class BoolFunctionArgument : ScalarArgument<bool>
	{
		public BoolFunctionArgument(string name, Func<bool, ArgumentValueValidationResult> valueValidator = null)
			: base(name, valueValidator)
		{
		}
	}

	public sealed class IntegerFunctionArgument : ScalarArgument<int>
	{
		public IntegerFunctionArgument(string name, Func<int, ArgumentValueValidationResult> valueValidator = null)
			: base(name, valueValidator)
		{
		}

		internal override int ConvertValue(VariableValueStorage value)
		{
			if (value == null)
				throw new ArgumentNullException(nameof(value));

			return Convert.ToInt32(value.GetPrimitiveValue());
		}
	}

	public sealed class TimeSpanFunctionArgument : ScalarArgument<TimeSpan>
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

		public static ArgumentValueValidationResult Valid { get; } = new ArgumentValueValidationResult(true);
	}
}
