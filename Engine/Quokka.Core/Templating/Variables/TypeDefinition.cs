using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Mindbox.Quokka
{
	interface IPrimitiveTypeDefinition
	{
		 Type RuntimeType { get; }
	}

	internal class PrimitiveTypeDefinition<TRuntimeType> : TypeDefinition, IPrimitiveTypeDefinition
	{
		public Type RuntimeType => typeof(TRuntimeType);

		public PrimitiveTypeDefinition(string name, TypeDefinition baseType, int priority)
			: base(name, baseType, priority)
		{
			
		}
	}

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
			Array = new TypeDefinition("Array", Unknown, 50);

			Boolean = new PrimitiveTypeDefinition<bool>("Boolean", Primitive, 10);
			Decimal = new PrimitiveTypeDefinition<decimal>("Decimal", Primitive, 15);
			Integer = new PrimitiveTypeDefinition<int>("Integer", Decimal, 20);
			String = new PrimitiveTypeDefinition<string>("String", Primitive, 10);
			DateTime = new PrimitiveTypeDefinition<DateTime>("DateTime", Primitive, 15);
			TimeSpan = new PrimitiveTypeDefinition<TimeSpan>("TimeSpan", Primitive, 15);

			primitiveTypeMap = new ReadOnlyDictionary<Type, TypeDefinition>(
				new[]
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
		/// Checks if the less concrete type compatible with the more concrete type for model validation purposes
		/// </summary>
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
		
		internal static TypeDefinition GetTypeDefinitionByRuntimeType(Type runtimeType)
		{
			TypeDefinition result;

			if (!primitiveTypeMap.TryGetValue(runtimeType, out result))
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
			else if (modelDefinition is IPrimitiveModelDefinition)
				return ((IPrimitiveModelDefinition)modelDefinition).Type;
			else
				throw new InvalidOperationException("Unsupported model definition");
		}

		internal static TypeDefinition GetResultingTypeForMultipleOccurences<TTypedObject>(
			IList<TTypedObject> occurences,
			Func<TTypedObject, TypeDefinition> typeSelector,
			Action<TTypedObject, TypeDefinition> inconsistentTypeErrorHandler)
		{
			if (!occurences.Any())
				throw new InvalidOperationException("No occurences");

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