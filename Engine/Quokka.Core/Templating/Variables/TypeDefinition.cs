using System;
using System.Collections.Generic;
using System.Linq;

namespace Quokka
{
	internal class TypeDefinition<TRuntimeType> : TypeDefinition
	{
		public TypeDefinition(string name, TypeDefinition baseType, int priority, bool allowsNull = false)
			: base(name, baseType, priority, allowsNull)
		{
			
		}
	}

	public class TypeDefinition
	{
		public string Name { get; }
		internal TypeDefinition BaseType { get; }
		internal int Priority { get; }
		internal bool AllowsNull { get; }

		internal TypeDefinition(string name, TypeDefinition baseType, int priority, bool allowsNull = false)
		{
			Name = name;
			BaseType = baseType;
			Priority = priority;
			AllowsNull = allowsNull;
		}

		internal bool IsCompatibleWithRequired(TypeDefinition requiredType)
		{
			if (this == requiredType)
				return true;

			if (BaseType != null && BaseType.IsCompatibleWithRequired(requiredType))
				return true;

			return false;
		}

		public override string ToString()
		{
			return Name;
		}

		public static TypeDefinition Unknown { get; } = new TypeDefinition("Unknown", null, 0);
		public static TypeDefinition Primitive { get; } = new TypeDefinition("Primitive", Unknown, 5);

		public static TypeDefinition Boolean { get; } = new TypeDefinition<bool>("Boolean", Primitive, 10);
		public static TypeDefinition NullableDecimal { get; } = new TypeDefinition<decimal?>("NullableDecimal", Primitive, 10, true);
		public static TypeDefinition Decimal { get; } = new TypeDefinition<decimal>("Decimal", NullableDecimal, 10);
		public static TypeDefinition Integer { get; } = new TypeDefinition<int>("Integer", Primitive, 10);
		public static TypeDefinition String { get; } = new TypeDefinition<string>("String", Primitive, 10, true);
		public static TypeDefinition NullableDateTime { get; } = new TypeDefinition<DateTime?>("NullableDateTime", Primitive, 10, true);
		public static TypeDefinition DateTime { get; } = new TypeDefinition<DateTime>("DateTime", NullableDateTime, 10);
		public static TypeDefinition NullableTimeSpan { get; } = new TypeDefinition<TimeSpan?>("NullableTimeSpan", Primitive, 10, true);
		public static TypeDefinition TimeSpan { get; } = new TypeDefinition<TimeSpan>("TimeSpan", NullableTimeSpan, 10);

		public static TypeDefinition Composite { get; } = new TypeDefinition("Composite", Unknown, 20);
		public static TypeDefinition Array { get; } = new TypeDefinition("Array", Unknown, 50);

		public static TypeDefinition GetTypeDefinitionByRuntimeType(Type runtimeType)
		{
			if (runtimeType == typeof(string))
				return String;
			if (runtimeType == typeof(int))
				return Integer;
			if (runtimeType == typeof(decimal))
				return Decimal;
			if (runtimeType == typeof(decimal?))
				return NullableDecimal;
			if (runtimeType == typeof(bool))
				return Boolean;
			if (runtimeType == typeof(DateTime))
				return DateTime;
			if (runtimeType == typeof(TimeSpan))
				return TimeSpan;
			if (runtimeType == typeof(DateTime?))
				return NullableDateTime;
			if (runtimeType == typeof(TimeSpan?))
				return NullableTimeSpan;

			throw new InvalidOperationException(
				$"Runtime type {runtimeType.Name} doesn't have a corresponding template variable type");
		}

		internal static TypeDefinition GetResultingTypeForMultupleOccurences<TTypedObject>(
			IList<TTypedObject> occurences,
			Func<TTypedObject, TypeDefinition> typeSelector,
			Action<TTypedObject, TypeDefinition> inconsistentTypeErrorHandler)
		{
			if (!occurences.Any())
				throw new InvalidOperationException("Variable has no occurences");

			if (occurences.Count == 1)
				return typeSelector(occurences.Single());

			var occurencesByTypePriority = occurences
				.OrderByDescending(oc => typeSelector(oc).Priority);

			TypeDefinition resultingType = null;

			foreach (var occurence in occurencesByTypePriority)
			{
				var occurenceType = typeSelector(occurence);

				if (resultingType == null)
				{
					resultingType = occurenceType;
				}
				else
				{
					if (occurenceType != resultingType && !resultingType.IsCompatibleWithRequired(occurenceType))
						inconsistentTypeErrorHandler(occurence, resultingType);
				}
			}

			return resultingType;
		}
	}
}