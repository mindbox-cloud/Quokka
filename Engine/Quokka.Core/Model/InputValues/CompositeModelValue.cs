using System.Collections.Generic;
using System.Linq;

namespace Quokka
{
	public class CompositeModelValue : ICompositeModelValue
	{
		public IReadOnlyList<IModelField> Fields { get; }

		public CompositeModelValue(params IModelField[] fields)
		{
			Fields = fields
				.ToList()
				.AsReadOnly();
		}

		public CompositeModelValue(IEnumerable<ModelField> fields)
		{
			Fields = fields
				.ToList()
				.AsReadOnly();
		}
	}
}