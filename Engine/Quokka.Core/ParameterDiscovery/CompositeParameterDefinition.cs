using System.Collections.Generic;

namespace Quokka
{
	internal class CompositeParameterDefinition : ParameterDefinition, ICompositeParameterDefinition
	{
		public IList<IParameterDefinition> Fields { get; }

		public CompositeParameterDefinition(string name, VariableType type, IList<IParameterDefinition> fields)
			: base(name, type)
		{
			Fields = fields;
		}
	}
}
