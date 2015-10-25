using System.Collections.Generic;
using System.Linq;

namespace Quokka
{
	internal class CompositeModelValue : ICompositeModelValue
	{
		public IReadOnlyList<IModelField> Fields { get; }

		public CompositeModelValue(params IModelField[] fields)
		{
			Fields = fields
				.ToList()
				.AsReadOnly();
		}
	}
}