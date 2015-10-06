using System.Collections.Generic;

namespace Quokka
{
	internal class CompositeParameterDefinition : ParameterDefinition, ICompositeParameterDefinition
	{
		public IList<IParameterDefinition> Fields { get; }

		public CompositeParameterDefinition(string name, IList<IParameterDefinition> fields)
			: base(name, VariableType.Composite)
		{
			Fields = fields;
		}
	}
}
