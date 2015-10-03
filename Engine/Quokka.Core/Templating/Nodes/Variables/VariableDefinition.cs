using System.Collections.Generic;

namespace Quokka
{
	internal class VariableDefinition
	{
		public string Name { get; }
		public string FullName { get; }
		public VariableType Type { get; }

		// I would like to make this immutabe as well but for now I'm going for an easier mutable implementation.
		public VariableCollection Fields { get; }

		public VariableDefinition(string name, string fullName, VariableType type)
		{
			Name = name;
			FullName = fullName;
			Type = type;
			Fields = new VariableCollection();
		}
	}
}
