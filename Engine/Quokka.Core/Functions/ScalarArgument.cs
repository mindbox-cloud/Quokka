// // Copyright 2022 Mindbox Ltd
// //
// // Licensed under the Apache License, Version 2.0 (the "License");
// // you may not use this file except in compliance with the License.
// // You may obtain a copy of the License at
// //
// //     http://www.apache.org/licenses/LICENSE-2.0
// //
// // Unless required by applicable law or agreed to in writing, software
// // distributed under the License is distributed on an "AS IS" BASIS,
// // WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// // See the License for the specific language governing permissions and
// // limitations under the License.

using System;

namespace Mindbox.Quokka
{
	public abstract class ScalarArgument<TType> : TemplateFunctionArgument
	{
		internal override TypeDefinition Type => TypeDefinition.GetTypeDefinitionByRuntimeType(typeof(TType));

		private readonly Func<TType, ArgumentValueValidationResult> valueValidator;

		protected ScalarArgument(string name, bool allowsNull = false, Func<TType, ArgumentValueValidationResult> valueValidator = null) 
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
		public DecimalFunctionArgument(string name, bool allowsNull = false, Func<decimal, ArgumentValueValidationResult> valueValidator = null)
			: base(name, allowsNull, valueValidator)
		{
		}

		internal override decimal ConvertValue(VariableValueStorage value)
		{
			ArgumentNullException.ThrowIfNull(value);
			return Convert.ToDecimal(value.GetPrimitiveValue());
		}
	}

	public sealed class StringFunctionArgument : ScalarArgument<string>
	{
		public StringFunctionArgument(string name, bool allowsNull = false, Func<string, ArgumentValueValidationResult> valueValidator = null)
			: base(name, allowsNull, valueValidator)
		{
		}
	}

	public sealed class DateTimeFunctionArgument : ScalarArgument<DateTime>
	{
		public DateTimeFunctionArgument(string name, bool allowsNull = false, Func<DateTime, ArgumentValueValidationResult> valueValidator = null)
			: base(name, allowsNull, valueValidator)
		{
		}
	}

	public sealed class BoolFunctionArgument : ScalarArgument<bool>
	{
		public BoolFunctionArgument(string name, bool allowsNull = false, Func<bool, ArgumentValueValidationResult> valueValidator = null)
			: base(name, allowsNull, valueValidator)
		{
		}
	}

	public sealed class IntegerFunctionArgument : ScalarArgument<int>
	{
		public IntegerFunctionArgument(string name, bool allowsNull = false, Func<int, ArgumentValueValidationResult> valueValidator = null)
			: base(name, allowsNull, valueValidator)
		{
		}

		internal override int ConvertValue(VariableValueStorage value)
		{
			ArgumentNullException.ThrowIfNull(value);
			
			return Convert.ToInt32(value.GetPrimitiveValue());
		}
	}

	public sealed class TimeSpanFunctionArgument : ScalarArgument<TimeSpan>
	{
		public TimeSpanFunctionArgument(string name, bool allowsNull = false, Func<TimeSpan, ArgumentValueValidationResult> valueValidator = null)
			: base(name, allowsNull, valueValidator)
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

		public static ArgumentValueValidationResult Valid { get; } = new(true);
	}
}
