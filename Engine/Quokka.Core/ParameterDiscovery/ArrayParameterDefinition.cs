using System.Collections.Generic;

namespace Quokka
{
	internal class ArrayParameterDefinition : ParameterDefinition, IArrayParameterDefinition
	{
		public IList<IParameterDefinition> ElementFields { get; }

		public ArrayParameterDefinition(
			string name,
			VariableType type,
			IList<IParameterDefinition> elementFields)
			: base(name, type)
		{
			ElementFields = elementFields;
		}
	}
}
