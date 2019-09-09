using System;

namespace Mindbox.Quokka
{
	public abstract class ScalarArgument<TType> : TemplateFunctionArgument
	{
		internal override TypeDefinition Type => TypeDefinition.GetTypeDefinitionByRuntimeType(typeof(TType));

		private readonly Func<TType, ArgumentValueValidationResult>? valueValidator;

		protected ScalarArgument(string name, bool allowsNull = false, Func<TType, ArgumentValueValidationResult>? valueValidator = null) 
			:base(name, allowsNull)
		{
			this.valueValidator = valueValidator;
		}

		internal override ArgumentValueValidationResult ValidateConstantValue(VariableValueStorage value)
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
		public DecimalFunctionArgument(string name, bool allowsNull = false, Func<decimal, ArgumentValueValidationResult>? valueValidator = null)
			: base(name, allowsNull, valueValidator)
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
		public StringFunctionArgument(string name, bool allowsNull = false, Func<string, ArgumentValueValidationResult>? valueValidator = null)
			: base(name, allowsNull, valueValidator)
		{
		}
	}

	public sealed class DateTimeFunctionArgument : ScalarArgument<DateTime>
	{
		public DateTimeFunctionArgument(string name, bool allowsNull = false, Func<DateTime, ArgumentValueValidationResult>? valueValidator = null)
			: base(name, allowsNull, valueValidator)
		{
		}
	}

	public sealed class BoolFunctionArgument : ScalarArgument<bool>
	{
		public BoolFunctionArgument(string name, bool allowsNull = false, Func<bool, ArgumentValueValidationResult>? valueValidator = null)
			: base(name, allowsNull, valueValidator)
		{
		}
	}

	public sealed class IntegerFunctionArgument : ScalarArgument<int>
	{
		public IntegerFunctionArgument(string name, bool allowsNull = false, Func<int, ArgumentValueValidationResult>? valueValidator = null)
			: base(name, allowsNull, valueValidator)
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
		public TimeSpanFunctionArgument(string name, bool allowsNull = false, Func<TimeSpan, ArgumentValueValidationResult>? valueValidator = null)
			: base(name, allowsNull, valueValidator)
		{
		}
	}

	public sealed class ArgumentValueValidationResult
	{
		public bool IsValid { get; }
		public string? ErrorMessage { get; }

		public ArgumentValueValidationResult(bool isValid, string? errorMessage = null)
		{
			if (isValid != (errorMessage == null))
				throw new ArgumentException("Error message must be specified when the value is not valid", nameof(errorMessage));

			IsValid = isValid;
			ErrorMessage = errorMessage;
		}

		public static ArgumentValueValidationResult Valid { get; } = new ArgumentValueValidationResult(true);
	}
}
