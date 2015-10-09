using System.Collections.Generic;

namespace Quokka
{
	public interface IArrayParameterValue : IParameterValue
	{
		IList<IParameterValue> Values { get; }
	}
}