using System.Collections.Generic;

namespace Quokka
{
	public interface IArrayParameterDefinition : IParameterDefinition
	{
		VariableType ElementType { get; }
		IList<IParameterDefinition> ElementFields { get; } 
	}
}