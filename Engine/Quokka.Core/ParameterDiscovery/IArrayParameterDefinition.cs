using System.Collections.Generic;

namespace Quokka
{
	public interface IArrayParameterDefinition : IParameterDefinition
	{
		IList<IParameterDefinition> ElementFields { get; } 
	}
}