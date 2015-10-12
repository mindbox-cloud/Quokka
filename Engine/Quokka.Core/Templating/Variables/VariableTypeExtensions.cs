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

			throw new InvalidOperationException(
				$"Runtime type {runtimeType.Name} doesn't have a corresponding template variable type");
		}
	}
}
