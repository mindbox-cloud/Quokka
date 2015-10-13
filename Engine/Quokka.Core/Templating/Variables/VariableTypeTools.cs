using System;

namespace Quokka
{
	//Todo: this will have to go somewhere in the proper Type object model
	internal static class VariableTypeTools
	{
		public static VariableType GetVariableTypeByRuntimeType(Type runtimeType)
		{
			if (runtimeType == typeof(string))
				return VariableType.String;
			if (runtimeType == typeof(int))
				return VariableType.Integer;
			if (runtimeType == typeof(decimal))
				return VariableType.Decimal;
			if (runtimeType == typeof(bool))
				return VariableType.Boolean;
			if (runtimeType == typeof(DateTime))
				return VariableType.DateTime;
			if (runtimeType == typeof(TimeSpan))
				return VariableType.TimeSpan;

			throw new InvalidOperationException(
				$"Runtime type {runtimeType.Name} doesn't have a corresponding template variable type");
		}

		// TODO: This needs to be rewritten to be an object hierarchy
		public static bool IsTypeCompatibleWithRequired(VariableType actualType, VariableType requiredType)
		{
			if (requiredType == VariableType.Unknown)
				return true;

			if (requiredType == VariableType.Primitive)
			{
				return actualType == VariableType.Boolean ||
						actualType == VariableType.DateTime ||
						actualType == VariableType.Decimal ||
						actualType == VariableType.Integer ||
						actualType == VariableType.String ||
						actualType == VariableType.TimeSpan;
			}

			return actualType == requiredType;
		}
	}
}
