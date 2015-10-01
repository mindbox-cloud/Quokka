using System.Collections.Generic;
using System.Linq;

namespace Quokka
{
	internal class ParameterMetadata
	{
		public string Name { get; }
		public ParameterType Type { get; }
		public IReadOnlyCollection<ParameterMetadata> Members { get; }

		private ParameterMetadata(string name, ParameterType type, IEnumerable<ParameterMetadata> members)
		{
			Name = name;
			Type = type;

			if (members != null)
				Members = members.ToList().AsReadOnly();
		}

		public ParameterMetadata(string name, ParameterType type, ParameterMetadata member)
			: this(name, type, member == null ? null : new[] { member })
		{
		}

	}
}
