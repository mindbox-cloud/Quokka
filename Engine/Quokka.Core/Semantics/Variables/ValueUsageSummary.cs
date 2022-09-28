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
		private TypeDefinition compiledType;

		/// <summary>
		/// Usage summaries for all the values that were created by iterating over this value (if it's a collection).
		/// (it'll give us information about fields that we should expect every element of the collection to have).
		/// </summary>
		/// <remarks>Only relevant for collection variables.</remarks>
		private readonly IList<ValueUsageSummary> enumerationResultUsageSummaries;

		private readonly List<ValueUsageSummary> assignedVariables = new List<ValueUsageSummary>();

		public IReadOnlyList<ValueUsageSummary> EnumerationResultUsageSummaries => 
			enumerationResultUsageSummaries.ToList().AsReadOnly();

		public IReadOnlyList<ValueUsageSummary> AssignedVariables =>
			assignedVariables.ToList().AsReadOnly();

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

		public bool IsReadOnly
		{
			get
			{
				return usages.All(u => u.Intention == VariableUsageIntention.Read);
			}
		}

		public void Compile(ISemanticErrorListener errorListener)
		{
			if (usages.First().Intention == VariableUsageIntention.Read
					&& usages.Any(u => u.Intention == VariableUsageIntention.Write))
				errorListener.AddVariableUsageBeforeAssignmentError(this, usages.First().Location);

			Fields.Items.ToList().ForEach(f => f.Value.Compile(errorListener));

			Methods.Items.ToList().ForEach(f => f.Value.Compile(errorListener));

			EnumerationResultUsageSummaries?.ToList().ForEach(f => f.Compile(errorListener));

			compiledType = TypeDefinition.GetResultingTypeForMultipleOccurrences(
				usages.Concat(assignedVariables.SelectMany(v => v.GetAllUsagesExcept(this))).ToList(),
				occurence => occurence.RequiredType,
				(occurence, correctType) => errorListener.AddInconsistentVariableTypingError(
					this,
					occurence,
					correctType));
		}

		private ValueUsageSummary EnsureCompiled(ISemanticErrorListener errorListener)
		{
			if (compiledType == null)
				Compile(errorListener);

			return this;
		}

		private IEnumerable<ValueUsage> GetAllUsagesExcept(ValueUsageSummary variable)
		{
			return GetAllUsagesExcept(new HashSet<ValueUsageSummary> { variable });
		}

		private IEnumerable<ValueUsage> GetAllUsagesExcept(HashSet<ValueUsageSummary> variables)
		{
			if (variables.Contains(this))
				return Enumerable.Empty<ValueUsage>();

			variables.Add(this);

			return assignedVariables.SelectMany(v => v.GetAllUsagesExcept(variables)).Concat(usages);
		}

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

		internal void RegisterAssignmentToVariable(ValueUsageSummary destinationVariable)
		{
			assignedVariables.Add(destinationVariable);
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
			EnsureCompiled(errorListener);

			if (compiledType.IsAssignableTo(TypeDefinition.Composite))
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

				if (compiledType == TypeDefinition.Array)
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
				else if (compiledType == TypeDefinition.Composite)
				{
					return new CompositeModelDefinition(fields, methods);
				}
				else
				{
					throw new InvalidOperationException($"Unexpected type {compiledType}");
				}
			}
			else
			{
				return new PrimitiveModelDefinition(compiledType);
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

			var actualType = TypeDefinition.GetResultingTypeForMultipleOccurrences(
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
				if (expectedModelDefinition.Fields.TryGetValue(actualItem.Key, out var fieldExpectedDefinition))
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

			foreach (var actualItem in Methods.Items)
			{
				if (expectedModelDefinition.Methods.TryGetValue(actualItem.Key.ToMethodCallDefinition(), out var methodExpectedDefinition))
				{
					actualItem.Value.ValidateAgainstExpectedModelDefinition(methodExpectedDefinition, errorListener);
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
						.Where(s => s.Value.IsReadOnly)
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
