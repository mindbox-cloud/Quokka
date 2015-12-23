using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quokka
{
	internal class GroupListTemplateFunction : TemplateFunction
	{
		private static readonly IModelDefinition resultModelDefinition = new ArrayModelDefinition(
			new CompositeModelDefinition(
				new Dictionary<string, IModelDefinition>
				{
					{
						"Elements",
						new ArrayModelDefinition(
							new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
							{
								// Not sure if Primitive is actually correct here since the element can be of any type,
								// but it matches the current implementation of parameter discovery
								{ "Object", new PrimitiveModelDefinition(TypeDefinition.Unknown) }
							}))
					}
				}));

		public GroupListTemplateFunction()
			: base("groupList", resultModelDefinition)
		{
		}

		internal override VariableValueStorage Invoke(IList<VariableValueStorage> argumentsValues)
		{
			throw new NotImplementedException();
		}

		
	}
}
