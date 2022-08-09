using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Mindbox.Quokka
{
	public class TypeDefinition
	{
		private static readonly IReadOnlyDictionary<Type, TypeDefinition> primitiveTypeMap;

		public string Name { get; }
		internal TypeDefinition BaseType { get; }
		internal int Priority { get; }

		public static TypeDefinition Unknown { get; }
		public static TypeDefinition Primitive { get; }

		public static TypeDefinition Boolean { get; }
		public static TypeDefinition Decimal { get; }
		public static TypeDefinition Integer { get; }
		public static TypeDefinition String { get; }
		public static TypeDefinition DateTime { get; }
		public static TypeDefinition TimeSpan { get; }

		public static TypeDefinition Composite { get; }
		public static TypeDefinition Array { get; }

		static TypeDefinition()
		{
			Unknown = new TypeDefinition("Unknown", null, 0);
			Primitive = new TypeDefinition("Primitive", Unknown, 5);

			Composite = new TypeDefinition("Composite", Unknown, 20);
			Array = new TypeDefinition("Array", Composite, 50);

			Boolean = new PrimitiveTypeDefinition<bool>("Boolean", Primitive, 10);
			Decimal = new PrimitiveTypeDefinition<decimal>("Decimal", Primitive, 15);
			Integer = new PrimitiveTypeDefinition<int>("Integer", Decimal, 20);
			String = new PrimitiveTypeDefinition<string>("String", Primitive, 10);
			DateTime = new PrimitiveTypeDefinition<DateTime>("DateTime", Primitive, 15);
			TimeSpan = new PrimitiveTypeDefinition<TimeSpan>("TimeSpan", Primitive, 15);

			primitiveTypeMap = new ReadOnlyDictionary<Type, TypeDefinition>(
				new []
				{
					Boolean,
					Decimal,
					Integer,
					String,
					DateTime,
					TimeSpan
				}
				.Cast<IPrimitiveTypeDefinition>()
				.ToDictionary(type => type.RuntimeType, type => (TypeDefinition)type));

		}

		internal TypeDefinition(string name, TypeDefinition baseType, int priority)
		{
			Name = name;
			BaseType = baseType;
			Priority = priority;
		}

		/// <summary>
		/// Checks if the type can be assigned to the required type and is therefore compatible to it.
		/// </summary>
		internal bool IsAssignableTo(TypeDefinition requiredType)
		{
			if (this == requiredType)
				return true;
			
			if (BaseType != null && BaseType.IsAssignableTo(requiredType))
				return true;
			
			return false;
		}
		
		public override string ToString()
		{
			return Name;
		}
		
		internal static TypeDefinition GetTypeDefinitionByRuntimeType(Type runtimeType)
		{
			if (!primitiveTypeMap.TryGetValue(runtimeType, out var result))
				throw new InvalidOperationException(
					$"Runtime type {runtimeType.Name} doesn't have a corresponding template variable type");

			return result;
		}

		internal static TypeDefinition GetTypeDefinitionFromModelDefinition(IModelDefinition modelDefinition)
		{
			if (modelDefinition == null)
				throw new ArgumentNullException(nameof(modelDefinition));

			if (modelDefinition is IArrayModelDefinition)
				return Array;
			else if (modelDefinition is ICompositeModelDefinition)
				return Composite;
			else if (modelDefinition is IPrimitiveModelDefinition primitiveDefinition)
				return primitiveDefinition.Type;
			else
				throw new InvalidOperationException("Unsupported model definition");
		}

		internal static TypeDefinition GetResultingTypeForMultipleOccurrences<TTypedObject>(
			IList<TTypedObject> occurrences,
			Func<TTypedObject, TypeDefinition> typeSelector,
			Action<TTypedObject, TypeDefinition> inconsistentTypeErrorHandler = null)
		{
			if (!occurrences.Any())
				throw new InvalidOperationException("No occurrences");

			if (occurrences.Count == 1)
				return typeSelector(occurrences.Single());

			var occurrencesByTypePriority = occurrences
				.OrderByDescending(oc => typeSelector(oc).Priority);

			TypeDefinition resultingType = null;

			foreach (var occurence in occurrencesByTypePriority)
			{
				var occurenceType = typeSelector(occurence);

				if (resultingType == null)
				{
					resultingType = occurenceType;
				}
				else
				{
					if (inconsistentTypeErrorHandler != null && !resultingType.IsAssignableTo(occurenceType))
						inconsistentTypeErrorHandler(occurence, resultingType);
				}
			}

			return resultingType;
		}
	}
}