using System.Collections.Generic;

namespace Quokka
{
	internal class ArrayParameterDefinition : ParameterDefinition, IArrayParameterDefinition
	{
		public VariableType ElementType { get; }
		public IList<IParameterDefinition> ElementFields { get; }

		public ArrayParameterDefinition(
			string name,
			VariableType elementType,
			IList<IParameterDefinition> elementFields)
			: base(name, VariableType.Array)
		{
			ElementType = elementType;
			ElementFields = elementFields;
		}
	}
}
