using System;
using System.Collections.Generic;
using System.Linq;

namespace Quokka
{
	internal class TableRowsTemplateFunction : TemplateFunction
	{
		private const int MinimumAllowedGroupSize = 1;

		private const string ValueFieldName = "Value";
		private const string CellsFieldName = "Cells";
		private const string ValueCountFieldName = "ValueCount";

		private static readonly IModelDefinition resultModelDefinition =
			new ArrayModelDefinition(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{
						CellsFieldName,
						new ArrayModelDefinition(
							new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
							{
								// Not sure if Primitive is actually correct here since the element can be of any type,
								// but it matches the current implementation of parameter discovery
								{ ValueFieldName, new PrimitiveModelDefinition(TypeDefinition.Unknown) }
							}))
					},
					{
						ValueCountFieldName,
						new PrimitiveModelDefinition(TypeDefinition.Integer)
					}
				}));

		public TableRowsTemplateFunction()
			: base(
				  "tableRows",
				  resultModelDefinition,
				  new TableValuesCollectionArgument("list"),
				  new IntegerFunctionArgument("rowSize", ValidateGroupSize))
		{
		}

		internal override VariableValueStorage Invoke(IList<VariableValueStorage> argumentsValues)
		{
			var collectionElements = argumentsValues[0].GetElements().ToList();
			int rowSize = Convert.ToInt32(argumentsValues[1].GetPrimitiveValue());

			// This must be validated at compile-time so it's ok to just die here.
			if (rowSize < 1)
				throw new InvalidOperationException("rowSize < 1");

			int collectionSize = collectionElements.Count;
			int cellCount = collectionSize % rowSize == 0
				? collectionSize
				: rowSize * ((collectionSize / rowSize) + 1);

			var groupingStructure =
				Enumerable.Range(0, cellCount)
					.Select(index => new
					{
						Element = index < collectionElements.Count ? collectionElements[index] : null,
						Index = index
					})
					.GroupBy(item => item.Index / rowSize)
					.Select(grouping => new CompositeModelValue(
						new ModelField(
							CellsFieldName,
							new ArrayModelValue(
								grouping
									.OrderBy(x => x.Index)
									.Select(x => new CompositeModelValue(
										new ModelField(ValueFieldName, x.Element?.ModelValue)))))))
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

		private class TableValuesCollectionArgument : CollectionArgument
		{
			public TableValuesCollectionArgument(string name)
				: base(name)
			{
			}

			internal override void MapArgumentValueToResult(
				SemanticAnalysisContext context,
				VariableDefinition resultDefinition,
				VariableDefinition argumentVariableDefinition)
			{
				var cellsField = resultDefinition
					.Fields
					.TryGetVariableDefinition("Cells");

				if (cellsField != null)
				{
					var a = cellsField.CollectionElementVariables
						.Select(iterator => iterator.Fields.TryGetVariableDefinition("Value"))
						.Where(value => value != null);

					foreach (var cellUsage in a)
						argumentVariableDefinition.AddCollectionElementVariable(cellUsage);
				}
			}
		}
	}
}
