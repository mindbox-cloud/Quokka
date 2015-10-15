using System;

namespace Quokka
{
	internal class TypeDefinition<TRuntimeType> : TypeDefinition
	{
		public TypeDefinition(string name, TypeDefinition baseType, int priority)
			: base(name, baseType, priority)
		{
			
		}
	}

	public class TypeDefinition
	{
		public string Name { get; }
		public TypeDefinition BaseType { get; }
		public int Priority { get; }

		internal TypeDefinition(string name, TypeDefinition baseType, int priority)
		{
			Name = name;
			BaseType = baseType;
			Priority = priority;
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
		public static TypeDefinition Decimal { get; } = new TypeDefinition<decimal>("Decimal", Primitive, 10);
		public static TypeDefinition Integer { get; } = new TypeDefinition<int>("Integer", Primitive, 10);
		public static TypeDefinition String { get; } = new TypeDefinition<string>("String", Primitive, 10);
		public static TypeDefinition DateTime { get; } = new TypeDefinition<DateTime>("DateTime", Primitive, 10);
		public static TypeDefinition TimeSpan { get; } = new TypeDefinition<TimeSpan>("TimeSpan", Primitive, 10);

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
			if (runtimeType == typeof(bool))
				return Boolean;
			if (runtimeType == typeof(DateTime))
				return DateTime;
			if (runtimeType == typeof(TimeSpan))
				return TimeSpan;

			throw new InvalidOperationException(
				$"Runtime type {runtimeType.Name} doesn't have a corresponding template variable type");
		}
    }
}