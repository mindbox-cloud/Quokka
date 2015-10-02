using System.Collections.Generic;
using System.Linq;

namespace Quokka
{
	internal class VariableOccurence
	{
		public string Name { get; }
		public VariableType RequiredType { get; }
		public IReadOnlyCollection<VariableOccurence> Members { get; }

		private VariableOccurence(string name, VariableType requiredType, IEnumerable<VariableOccurence> members)
		{
			Name = name;
			RequiredType = requiredType;

			if (members != null)
				Members = members.ToList().AsReadOnly();
		}

		public VariableOccurence(string name, VariableType requiredType, VariableOccurence member)
			: this(name, requiredType, member == null ? null : new[] { member })
		{
		}

	}
}
