using System.Collections.Generic;

namespace Quokka
{
	public interface IParameterDefinition
	{
		string Name { get; }
		VariableType Type { get; }
	}
}