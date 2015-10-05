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
		public string Name { get; }
		public string FullName { get; }
		public VariableType Type { get; set; }
		
		/// <summary>
		/// Own fields of the variable.
		/// </summary>
		/// <remarks>Only relevant for composite variables.</remarks>
		public VariableCollection Fields { get; }

		/// <summary>
		/// Variable that is used to iterate over this element if it's a collection
		/// (it'll give us information about fields that we should expect every element of the collection to have).
		/// </summary>
		/// <remarks>Only relevant for collection variables.</remarks>
		public List<VariableDefinition> CollectionElementVariables { get; } = new List<VariableDefinition>();

		public VariableDefinition(string name, string fullName, VariableType type)
			: this(name, fullName, type, new VariableCollection())
		{
		}

		private VariableDefinition(string name, string fullName, VariableType type, VariableCollection fields)
		{
			Name = name;
			FullName = fullName;
			Type = type;
			Fields = fields;
		}

		public IParameterDefinition ToParameterDefinition()
		{
			switch (Type)
			{
				case VariableType.Composite:

					return new CompositeParameterDefinition(
						Name,
						Fields.GetParameterDefinitions());

				case VariableType.Array:
					if (!CollectionElementVariables.Any())
						throw new InvalidOperationException(
							"The variable is of an array type but no collection variables found");

					var collectionElementDefinition = Merge(CollectionElementVariables.ToList());
					
					return new ArrayParameterDefinition(
						Name,
						collectionElementDefinition.Type,
						collectionElementDefinition.Fields.GetParameterDefinitions());

				default:
					return new ParameterDefinition(
						Name, 
						Type);
			}
		}

		public static VariableDefinition Merge(IList<VariableDefinition> definitions)
		{
			VariableType type = default(VariableType);
			string name = null;
			bool firstDefinitionProcessed = false;

			foreach (var definition in definitions)
			{
				if (!firstDefinitionProcessed)
				{
					type = definition.Type;
					name = definition.Name;
					firstDefinitionProcessed = true;
				}
				else
				{
					if (definition.Type != type)
						//TODO: change it to be an analysis error
						throw new InvalidOperationException("Inconsistent typing");
				}
			}

			var fields = VariableCollection.Merge(definitions.Select(definition => definition.Fields).ToList());

            return new VariableDefinition(name, name, type, fields);
		}
		
	}
}
