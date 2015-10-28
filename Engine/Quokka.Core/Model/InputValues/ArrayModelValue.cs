using System.Collections.Generic;
using System.Linq;

namespace Quokka
{
	public class ArrayModelValue : IArrayModelValue
	{
		public IList<IModelValue> Values { get; }

		public ArrayModelValue(params IModelValue[] values)
		{
			Values = values
				.ToList()
				.AsReadOnly();
		}

		public ArrayModelValue(IEnumerable<IModelValue> values)
		{
			Values = values
				.ToList()
				.AsReadOnly();
		}
	}
}