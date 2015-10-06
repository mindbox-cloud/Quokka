using System.Collections.Generic;

namespace Quokka
{
	public interface ICompositeParameterDefinition : IParameterDefinition
	{
		IList<IParameterDefinition> Fields { get; }
	}
}
