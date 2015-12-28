using System;
using System.Collections.Generic;
using System.Linq;

namespace Quokka
{
	/// <summary>
	/// The definition of a variable that we discover from the template - its name, required type, properties etc.
	/// </summary>
	/// <remarks>
	/// This is a highly mutable representation of all the things we know about the variable.
	/// It gradually becomes more and more precise as we go through the tree. Basically, only name 
	/// is guaranteed to remain unchanged. Everything else, including type, is a subject to change
	/// as we get to know more things about variable usages.
	/// </remarks>
	internal class VariableDefinition
	{
		private readonly IList<VariableOccurence> occurences;

		/// <summary>
		/// Variables that are used to iterate over this element if it's a collection
		/// (it'll give us information about fields that we should expect every element of the collection to have).
		/// </summary>
		/// <remarks>Only relevant for collection variables.</remarks>
		private readonly IList<VariableDefinition> collectionElementVariables;


		public IList<VariableDefinition> CollectionElementVariables => collectionElementVariables.ToList().AsReadOnly();

		public string Name { get; }
		public string FullName { get; }

		/// <summary>
		/// Own fields of the variable.
		/// </summary>
		/// <remarks>Only relevant for composite variables.</remarks>
		public VariableCollection Fields { get; }
		
		public VariableDefinition(string name, string fullName)
			: this(
				  name,
				  fullName,
				  new VariableCollection(),
				  new List<VariableOccurence>(),
				  new List<VariableDefinition>())
		{
		}

		private VariableDefinition(
			string name,
			string fullName,
			VariableCollection fields,
			IList<VariableOccurence> occurences,
			IList<VariableDefinition> collectionElementVariables)
		{
			Name = name;
			FullName = fullName;
			Fields = fields;
			this.occurences = occurences;
			this.collectionElementVariables = collectionElementVariables;
		}

		public void AddOccurence(VariableOccurence occurence)
		{
			if (!string.Equals(occurence.Name, Name, StringComparison.InvariantCultureIgnoreCase))
				throw new InvalidOperationException("Variable occurence name doesn't match the definition");

			occurences.Add(occurence);
		}

		public void AddCollectionElementVariable(VariableDefinition collectionElementVariable)
		{
			collectionElementVariables.Add(collectionElementVariable);
		}

		public IModelDefinition ToModelDefinition(ModelDefinitionFactory modelDefinitionFactory, ISemanticErrorListener errorListener)
		{
			var type = TypeDefinition.GetResultingTypeForMultipleOccurences(
				occurences,
				occurence => occurence.RequiredType,
				(occurence, correctType) => errorListener.AddInconsistentVariableTypingError(
					this,
					occurence,
					correctType));

			if (type == TypeDefinition.Composite)
			{
				return Fields.ToModelDefinition(modelDefinitionFactory, errorListener);
			}
			else if (type == TypeDefinition.Array)
			{
				IModelDefinition collectionElementDefinition;
				if (collectionElementVariables.Any())
				{
					collectionElementDefinition = Merge(
						"Element",
						$"{FullName}[]",
						collectionElementVariables)
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
				occurences,
				occurence => occurence.RequiredType,
				(occurence, correctType) => { });
			if (actualType == TypeDefinition.Unknown)
				return;

			if (expectedType.IsCompatibleWithRequired(actualType))
			{
				if (expectedType == TypeDefinition.Composite)
				{
					Fields.ValidateAgainstExpectedModelDefinition(
						(CompositeModelDefinition)expectedModelDefinition,
						errorListener);
				}
				else if (expectedType == TypeDefinition.Array)
				{
					if (collectionElementVariables.Any())
					{
						var mergedCollectionElement = Merge(
							"Element",
							$"{FullName}[]",
							collectionElementVariables);

						mergedCollectionElement.ValidateAgainstExpectedModelDefinition(
							((IArrayModelDefinition)expectedModelDefinition).ElementModelDefinition,
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
					occurences.First().Location);
			}
		}

		public Location GetFirstLocation()
		{
			return occurences.First().Location;
		}

		public static VariableDefinition Merge(
			string resultName,
			string resultFullName,
			IList<VariableDefinition> definitions)
		{
			var fields = VariableCollection.Merge(resultFullName, definitions.Select(definition => definition.Fields).ToList());
			var occurences = definitions.SelectMany(definition => definition.occurences);
			var collectionElementVariables = definitions.SelectMany(definition => definition.collectionElementVariables);

			return new VariableDefinition(
				resultName,
				resultFullName,
				fields,
				occurences.ToList(),
				collectionElementVariables.ToList());
		}
	}
}
