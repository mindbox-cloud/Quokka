using System.Collections.Generic;

namespace Quokka
{
	internal class ArrayParameterValue : IArrayParameterValue
	{
		public IList<IParameterValue> Values { get; }

		public ArrayParameterValue(IList<IParameterValue> values)
		{
			Values = values;
		}
	}
}