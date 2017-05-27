using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Mindbox.Quokka
{
	/// <summary>
	/// Summary of all the usages of value and its members.
	/// </summary>
	/// <remarks>
	/// This is a highly mutable representation of all the things we know about the value.
	/// It gradually becomes more and more precise as we traverse the template tree. 
	/// This summary can then be used to determine the resulting type of this value.
	/// </remarks>
	internal class ValueUsageSummary
	{
		private readonly IList<ValueUsage> usages;

		/// <summary>
		/// Usage summaries for all the values that were created by iterating over this value (if it's a collection).
		/// (it'll give us information about fields that we should expect every element of the collection to have).
		/// </summary>
		/// <remarks>Only relevant for collection variables.</remarks>
		private readonly IList<ValueUsageSummary> enumerationResultUsageSummaries;
		
		public IReadOnlyList<ValueUsageSummary> EnumerationResultUsageSummaries => 
			enumerationResultUsageSummaries.ToList().AsReadOnly();

		public string FullName { get; }

		/// <summary>
		/// Own fields of the value.
		/// </summary>
		/// <remarks>Only relevant for composite variables.</remarks>
		public MemberCollection<string> Fields { get; }

		/// <summary>
		/// Own methods of the value.
		/// </summary>
		/// <remarks>Only relevant for composite variables.</remarks>
		public MemberCollection<MethodCall> Methods { get; }

		public ValueUsageSummary(string fullName)
			: this(
				  fullName,
				  new MemberCollection<string>(StringComparer.OrdinalIgnoreCase),
				  new MemberCollection<MethodCall>(null),
				  new List<ValueUsage>(),
				  new List<ValueUsageSummary>())
		{
		}

		private ValueUsageSummary(
			string fullName,
			MemberCollection<string> fields,
			MemberCollection<MethodCall> methods,
			IList<ValueUsage> usages,
			IList<ValueUsageSummary> enumerationResultUsageSummaries)
		{
			FullName = fullName;
			Fields = fields;
			Methods = methods;
			this.usages = usages;
			this.enumerationResultUsageSummaries = enumerationResultUsageSummaries;
		}

		public void AddUsage(ValueUsage occurence)
		{
			usages.Add(occurence);
		}

		public void AddEnumerationResultUsageSummary(ValueUsageSummary usageSummary)
		{
			enumerationResultUsageSummaries.Add(usageSummary);
		}

		public IModelDefinition ToModelDefinition(ModelDefinitionFactory modelDefinitionFactory, ISemanticErrorListener errorListener)
		{
			var type = TypeDefinition.GetResultingTypeForMultipleOccurences(
				usages,
				occurence => occurence.RequiredType,
				(occurence, correctType) => errorListener.AddInconsistentVariableTypingError(
					this,
					occurence,
					correctType));

			if (type == TypeDefinition.Composite)
			{
				return ConvertCollectionToModelDefinition(Fields, modelDefinitionFactory, errorListener);
			}
			else if (type == TypeDefinition.Array)
			{
				IModelDefinition collectionElementDefinition;
				if (enumerationResultUsageSummaries.Any())
				{
					collectionElementDefinition = Merge(
						$"{FullName}[]",
						enumerationResultUsageSummaries)
						.ToModelDefinition(modelDefinitionFactory, errorListener);
				}
				else
				{
					collectionElementDefinition = new PrimitiveModelDefinition(TypeDefinition.Unknown);
				}

				return modelDefinitionFactory.CreateArray(collectionElementDefinition);
			}
			else
				return modelDefinitionFactory.CreatePrimitive(type);
		}

		public void ValidateAgainstExpectedModelDefinition(IModelDefinition expectedModelDefinition, ISemanticErrorListener errorListener)
		{
			if (expectedModelDefinition == null)
				return;

			var expectedType = TypeDefinition.GetTypeDefinitionFromModelDefinition(expectedModelDefinition);
			if (expectedType == TypeDefinition.Unknown)
				return;

			var actualType = TypeDefinition.GetResultingTypeForMultipleOccurences(
				usages,
				occurence => occurence.RequiredType,
				(occurence, correctType) => { });
			if (actualType == TypeDefinition.Unknown)
				return;

			ValidateKnownTypeAgaintExpectedModelDefinition(expectedModelDefinition, expectedType, actualType, errorListener);
		}

		private void ValidateKnownTypeAgaintExpectedModelDefinition(
			IModelDefinition expectedModelDefinition,
			TypeDefinition expectedType,
			TypeDefinition actualType,
			ISemanticErrorListener errorListener)
		{
			if (expectedType.IsAssignableTo(actualType))
			{
				if (expectedType == TypeDefinition.Composite)
				{
					ValidateAgainstExpectedModelDefinition(
						(CompositeModelDefinition)expectedModelDefinition,
						errorListener);
				}
				else if (expectedType == TypeDefinition.Array)
				{
					if (enumerationResultUsageSummaries.Any())
					{
						var mergedCollectionElement = Merge(
							$"{FullName}[]",
							enumerationResultUsageSummaries);

						mergedCollectionElement.ValidateAgainstExpectedModelDefinition(
							((IArrayModelDefinition) expectedModelDefinition).ElementModelDefinition,
							errorListener);
					}
				}
			}
			else
			{
				errorListener.AddActualTypeNotMatchingDeclaredTypeError(
					this,
					actualType,
					expectedType,
					usages.First().Location);
			}
		}

		private void ValidateAgainstExpectedModelDefinition(
			ICompositeModelDefinition expectedModelDefinition,
			ISemanticErrorListener errorListener)
		{
			if (expectedModelDefinition == null)
				return;

			foreach (var actualItem in Fields.Items)
			{
				IModelDefinition fieldExpectedDefinition;
				if (expectedModelDefinition.Fields.TryGetValue(actualItem.Key, out fieldExpectedDefinition))
				{
					actualItem.Value.ValidateAgainstExpectedModelDefinition(fieldExpectedDefinition, errorListener);
				}
				else
				{
					errorListener.AddUnexpectedFieldOnCompositeDeclaredTypeError(
						actualItem.Value,
						actualItem.Value.GetFirstLocation());
				}
			}
		}

		public Location GetFirstLocation()
		{
			return usages.First().Location;
		}
		
		public static ICompositeModelDefinition ConvertCollectionToModelDefinition(
			MemberCollection<string> fields,
			ModelDefinitionFactory modelDefinitionFactory,
			ISemanticErrorListener errorListener)
		{
			return modelDefinitionFactory.CreateComposite(
				new ReadOnlyDictionary<string, IModelDefinition>(
					fields.Items
						.ToDictionary(
							kvp => kvp.Key,
							kvp => kvp.Value.ToModelDefinition(modelDefinitionFactory, errorListener),
							StringComparer.InvariantCultureIgnoreCase)));
		}

		public static ValueUsageSummary Merge(
			string resultFullName,
			IList<ValueUsageSummary> definitions)
		{
			var fields = MemberCollection<string>.Merge(
				resultFullName,
				definitions
					.Select(definition => definition.Fields)
					.ToList(),
				StringComparer.OrdinalIgnoreCase);

			var occurences = definitions.SelectMany(definition => definition.usages);
			var collectionElementVariables = definitions.SelectMany(definition => definition.enumerationResultUsageSummaries);

			return new ValueUsageSummary(
				resultFullName,
				fields,
				// should be merged also
				new MemberCollection<MethodCall>(null), 
				occurences.ToList(),
				collectionElementVariables.ToList());
		}
	}
}
