using System.Collections.Generic;

namespace Quokka
{
	internal class ArrayParameterDefinition : ParameterDefinition, IArrayParameterDefinition
	{
		public IList<IParameterDefinition> ElementFields { get; }

		public ArrayParameterDefinition(
			string name,
			IList<IParameterDefinition> elementFields)
			: base(name, VariableType.Array)
		{
			ElementFields = elementFields;
		}
	}
}
