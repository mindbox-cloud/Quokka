using System.Collections.Generic;
using System.Linq;

namespace Quokka
{
	internal class ArrayModelValue : IArrayModelValue
	{
		public IList<IModelValue> Values { get; }

		public ArrayModelValue(params IModelValue[] values)
		{
			Values = values
				.ToList()
				.AsReadOnly();
		}
	}
}