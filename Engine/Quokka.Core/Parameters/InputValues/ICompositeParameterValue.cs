using System.Collections.Generic;

namespace Quokka
{
	public interface ICompositeParameterValue : IParameterValue
	{
		IList<IParameterField> Fields { get; }
	}
}