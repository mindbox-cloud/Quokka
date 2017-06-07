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
		/// <remarks>Only relevant for composite values.</remarks>
		public MemberCollection<string> Fields { get; }

		/// <summary>
		/// Own methods of the value.
		/// </summary>
		/// <remarks>Only relevant for composite values.</remarks>
		public MemberCollection<MethodCall> Methods { get; }

		public ValueUsageSummary(string fullName)
			: this(
				  fullName,
				  new MemberCollection<string>(),
				  new MemberCollection<MethodCall>(),
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

		private IModelDefinition ToModelDefinition(ISemanticErrorListener errorListener)
		{
			var type = TypeDefinition.GetResultingTypeForMultipleOccurences(
				usages,
				occurence => occurence.RequiredType,
				(occurence, correctType) => errorListener.AddInconsistentVariableTypingError(
					this,
					occurence,
					correctType));

			if (type.IsAssignableTo(TypeDefinition.Composite))
			{
				var fields = new ReadOnlyDictionary<string, IModelDefinition>(
					Fields.Items
						.ToDictionary(
							kvp => kvp.Key,
							kvp => kvp.Value.ToModelDefinition(errorListener),
							StringComparer.InvariantCultureIgnoreCase));

				var methods = new ReadOnlyDictionary<IMethodCallDefinition, IModelDefinition>(
					Methods.Items
						.ToDictionary(
							kvp => kvp.Key.ToMethodCallDefinition(),
							kvp => kvp.Value.ToModelDefinition(errorListener)));

				CheckForFieldsAndMethodsNameConflicts(errorListener);

				if (type == TypeDefinition.Array)
				{
					IModelDefinition collectionElementDefinition;
					if (enumerationResultUsageSummaries.Any())
					{
						collectionElementDefinition = Merge(
								$"{FullName}[]",
								enumerationResultUsageSummaries)
							.ToModelDefinition(errorListener);
					}
					else
					{
						collectionElementDefinition = new PrimitiveModelDefinition(TypeDefinition.Unknown);
					}

					return new ArrayModelDefinition(collectionElementDefinition, fields, methods);
				}
				else if (type == TypeDefinition.Composite)
				{
					return new CompositeModelDefinition(fields, methods);
				}
				else
				{
					throw new InvalidOperationException($"Unexpected type {type}");
				}
			}
			else
			{
				return new PrimitiveModelDefinition(type);
			}
		}

		private void CheckForFieldsAndMethodsNameConflicts(ISemanticErrorListener errorListener)
		{
			var methodNames = new HashSet<string>(
				Methods.Items.Select(item => item.Key.Name),
				StringComparer.OrdinalIgnoreCase);

			var conflictingFields = Fields.Items
				.Where(field => methodNames.Contains(field.Key))
				.Select(kvp => kvp.Value);

			foreach (var field in conflictingFields)
				errorListener.AddFieldAndMethodNameConflictError(field, field.GetFirstLocation());
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
				occurence => occurence.RequiredType);

			if (actualType == TypeDefinition.Unknown)
				return;

			ValidateKnownTypeAgainstExpectedModelDefinition(expectedModelDefinition, expectedType, actualType, errorListener);
		}

		private void ValidateKnownTypeAgainstExpectedModelDefinition(
			IModelDefinition expectedModelDefinition,
			TypeDefinition expectedType,
			TypeDefinition actualType,
			ISemanticErrorListener errorListener)
		{
			if (expectedType.IsAssignableTo(actualType))
			{
				if (expectedType.IsAssignableTo(TypeDefinition.Composite))
				{
					ValidateAgainstExpectedModelDefinition(
						(CompositeModelDefinition)expectedModelDefinition,
						errorListener);

					if (expectedType == TypeDefinition.Array)
					{
						if (enumerationResultUsageSummaries.Any())
						{
							var mergedCollectionElement = Merge(
								$"{FullName}[]",
								enumerationResultUsageSummaries);

							mergedCollectionElement.ValidateAgainstExpectedModelDefinition(
								((IArrayModelDefinition)expectedModelDefinition).ElementModelDefinition,
								errorListener);
						}
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
			ISemanticErrorListener errorListener)
		{
			return new CompositeModelDefinition(
				new ReadOnlyDictionary<string, IModelDefinition>(
					fields.Items
						.ToDictionary(
							kvp => kvp.Key,
							kvp => kvp.Value.ToModelDefinition(errorListener),
							StringComparer.InvariantCultureIgnoreCase)),
				null);
		}

		public static ValueUsageSummary Merge(
			string resultFullName,
			IList<ValueUsageSummary> definitions)
		{
			var fields = MemberCollection<string>.Merge(
				resultFullName,
				definitions
					.Select(definition => definition.Fields)
					.ToList());

			var methods = MemberCollection<MethodCall>.Merge(
				resultFullName,
				definitions
					.Select(definition => definition.Methods)
					.ToList());

			var mergesUsages = definitions
				.SelectMany(definition => definition.usages);
			var mergedEnumerationResultUsageSummaries = definitions
				.SelectMany(definition => definition.enumerationResultUsageSummaries);

			return new ValueUsageSummary(
				resultFullName,
				fields,
				methods, 
				mergesUsages.ToList(),
				mergedEnumerationResultUsageSummaries.ToList());
		}
	}
}
