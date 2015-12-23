using System;
using System.Collections.Generic;
using System.Linq;

namespace Quokka
{
	internal class GroupListTemplateFunction : TemplateFunction
	{
		private const int MinimumAllowedGroupSize = 1;

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
			: base(
				  "groupList",
				  resultModelDefinition,
				  new CollectionArgument("list"),
				  new IntegerFunctionArgument("groupSize", ValidateGroupSize))
		{
		}

		internal override VariableValueStorage Invoke(IList<VariableValueStorage> argumentsValues)
		{
			var collectionElements = argumentsValues[0].GetElements();
			int groupSize = Convert.ToInt32(argumentsValues[1].GetPrimitiveValue());

			// This must be validated at compile-time so it's ok to just die here.
			if (groupSize < 1)
				throw new InvalidOperationException("groupSize < 1");

			var groupingStructure =
				collectionElements
					.Select((element, index) => new
					{
						Element = element,
						Index = index
					})
					.GroupBy(item => item.Index / groupSize)
					.Select(grouping => new CompositeModelValue(
						new ModelField(
							"Elements",
							new ArrayModelValue(
								grouping
									.OrderBy(x => x.Index)
									.Select(x => x.Element.ModelValue)))))
					.ToList();

			return new ArrayVariableValueStorage(new ArrayModelValue(groupingStructure));
		}

		private static ArgumentValueValidationResult ValidateGroupSize(int groupSize)
		{
			if (groupSize >= MinimumAllowedGroupSize)
				return ArgumentValueValidationResult.Valid;

			return new ArgumentValueValidationResult(
				false,
				$"Количество элементов в группе должно быть не меньше {MinimumAllowedGroupSize}");
		}
	}
}
