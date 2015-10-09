using System.Collections.Generic;
using System.Linq;

namespace Quokka
{
	internal class ArrayParameterValue : IArrayParameterValue
	{
		public IList<IParameterValue> Values { get; }

		public ArrayParameterValue(params IParameterValue[] values)
		{
			Values = values
				.ToList()
				.AsReadOnly();
		}
	}
}