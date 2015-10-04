using System;

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
		public VariableType Type { get; }
		
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
		public VariableDefinition CollectionElementVariable { get; set; }

		public VariableDefinition(string name, string fullName, VariableType type)
		{
			Name = name;
			FullName = fullName;
			Type = type;
			Fields = new VariableCollection();
		}

		public IParameterDefinition ToParameterDefinition()
		{
			switch (Type)
			{
				case VariableType.Composite:

					return new CompositeParameterDefinition(
						Name,
						Type,
						Fields.GetParameterDefinitions());

				case VariableType.Array:
					if (CollectionElementVariable == null)
						throw new InvalidOperationException(
							"The variable is of an array type but the collection variable is not set");

					return new ArrayParameterDefinition(
						Name,
						Type,
						CollectionElementVariable.Fields.GetParameterDefinitions());

				default:
					return new ParameterDefinition(
						Name, 
						Type);
			}
		}

		
	}
}
