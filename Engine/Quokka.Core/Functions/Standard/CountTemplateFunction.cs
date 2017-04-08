using System.Collections.Generic;
using System.Linq;

namespace Mindbox.Quokka
{
	internal class CountTemplateFunction : TemplateFunction
	{
		public CountTemplateFunction()
			:base(
				 "count",
				 new PrimitiveModelDefinition(TypeDefinition.Integer),
				 new CollectionArgument("list"))
		{
		}

		internal override VariableValueStorage Invoke(IList<VariableValueStorage> argumentsValues)
		{
			var collection = argumentsValues[0].GetElements();
			return new PrimitiveVariableValueStorage(collection.Count());
		}
	}
}
