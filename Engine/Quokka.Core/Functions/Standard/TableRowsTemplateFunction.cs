﻿// // Copyright 2022 Mindbox Ltd
// //
// // Licensed under the Apache License, Version 2.0 (the "License");
// // you may not use this file except in compliance with the License.
// // You may obtain a copy of the License at
// //
// //     http://www.apache.org/licenses/LICENSE-2.0
// //
// // Unless required by applicable law or agreed to in writing, software
// // distributed under the License is distributed on an "AS IS" BASIS,
// // WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// // See the License for the specific language governing permissions and
// // limitations under the License.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Mindbox.Quokka
{
	internal class TableRowsTemplateFunction : TemplateFunction
	{
		private const int MinimumAllowedGroupSize = 1;

		private const string ValueFieldName = "Value";
		private const string CellsFieldName = "Cells";
		private const string ValueCountFieldName = "ValueCount";
		private const string IndexFieldName = "Index";
		private const string IsFirstFieldName = "IsFirst";
		private const string IsLastFieldName = "IsLast";

		private static readonly IModelDefinition resultModelDefinition =
			new ArrayModelDefinition(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>(StringComparer.InvariantCultureIgnoreCase)
				{
					{
						CellsFieldName,
						new ArrayModelDefinition(
							new CompositeModelDefinition(new Dictionary<string, IModelDefinition>(StringComparer.InvariantCultureIgnoreCase)
							{
								// Not sure if Primitive is actually correct here since the element can be of any type,
								// but it matches the current implementation of parameter discovery
								{
									ValueFieldName,
									new PrimitiveModelDefinition(TypeDefinition.Unknown)
								},
								{
									IndexFieldName,
									new PrimitiveModelDefinition(TypeDefinition.Integer)
								},
								{
									IsFirstFieldName,
									new PrimitiveModelDefinition(TypeDefinition.Boolean)
								},
								{
									IsLastFieldName,
									new PrimitiveModelDefinition(TypeDefinition.Boolean)
								}
							}))
					},
					{
						ValueCountFieldName,
						new PrimitiveModelDefinition(TypeDefinition.Integer)
					},
					{
						IndexFieldName,
						new PrimitiveModelDefinition(TypeDefinition.Integer)
					},
					{
						IsFirstFieldName,
						new PrimitiveModelDefinition(TypeDefinition.Boolean)
					},
					{
						IsLastFieldName,
						new PrimitiveModelDefinition(TypeDefinition.Boolean)
					}
				}));

		public TableRowsTemplateFunction()
			: base(
				  "tableRows",
				  resultModelDefinition,
				  new TableValuesCollectionArgument("list"),
				  new IntegerFunctionArgument("rowSize", valueValidator: ValidateGroupSize))
		{
		}

		internal override VariableValueStorage Invoke(RenderContext renderContext, IList<VariableValueStorage> argumentsValues)
		{
			var collectionElements = argumentsValues[0].GetElements().ToList();
			int rowSize = Convert.ToInt32(argumentsValues[1].GetPrimitiveValue());

			// This must be validated at compile-time so it's ok to just die here.
			if (rowSize < 1)
				throw new InvalidOperationException("rowSize < 1");

			int collectionSize = collectionElements.Count;
			int rowCount = collectionSize / rowSize + 
				(collectionSize % rowSize == 0 ? 0 : 1);

			int firstRowIndex = 0;
			int lastRowIndex = rowCount - 1;
			int firstCellIndex = 0;
			int lastCellIndex = rowSize - 1;

			int cellCount = rowCount * rowSize;

			var groupingStructure =
				Enumerable.Range(0, cellCount)
					.Select(index => new
					{
						Value = index < collectionElements.Count ? collectionElements[index] : null,
						Index = index
					})
					.GroupBy(item => item.Index / rowSize)
					.Select(grouping => new
					{
						Index = grouping.Key,
						Cells = grouping
					})
					.OrderBy(row => row.Index)
					.Select(row => new CompositeModelValue(
						new ModelField(
							IndexFieldName,
							new PrimitiveModelValue(row.Index + 1)),
						new ModelField(
							ValueCountFieldName,
							new PrimitiveModelValue(row.Cells.Count(c => c.Value != null))),
						new ModelField(
							IsFirstFieldName,
							new PrimitiveModelValue(row.Index == firstRowIndex)),
						new ModelField(
							IsLastFieldName,
							new PrimitiveModelValue(row.Index == lastRowIndex)),
						new ModelField(
							CellsFieldName,
							new ArrayModelValue(
								row.Cells
									.OrderBy(x => x.Index)
									.Select(x => new
									{
										IndexInsideRow = x.Index % rowSize,
										x.Value
									})
									.Select(cell => new CompositeModelValue(
										new ModelField(
											ValueFieldName,
											cell.Value?.ModelValue),
										new ModelField(
											IndexFieldName,
											new PrimitiveModelValue(cell.IndexInsideRow + 1)),
										new ModelField(
											IsFirstFieldName,
											new PrimitiveModelValue(cell.IndexInsideRow == firstCellIndex)),
										new ModelField(
											IsLastFieldName,
											new PrimitiveModelValue(cell.IndexInsideRow == lastCellIndex))))))))
					.ToList();

			return new ArrayVariableValueStorage(new ArrayModelValue(groupingStructure));
		}

		private static ArgumentValueValidationResult ValidateGroupSize(int groupSize)
		{
			if (groupSize >= MinimumAllowedGroupSize)
				return ArgumentValueValidationResult.Valid;

			return new ArgumentValueValidationResult(
				false,
				$"Group size can't be less than {MinimumAllowedGroupSize}");
		}

		private class TableValuesCollectionArgument : CollectionArgument
		{
			public TableValuesCollectionArgument(string name)
				: base(name)
			{
			}
			

			internal override void AnalyzeArgumentValueBasedOnFunctionResultUsages(
				AnalysisContext context,
				ValueUsageSummary resultValueUsageSummary,
				IExpression argumentValueExpression)
			{
				ValueUsageSummary argumentValueUsageSummary;

				switch (argumentValueExpression)
				{
					case VariableValueExpression variableValueExpression:
						argumentValueUsageSummary = variableValueExpression.GetVariableDefinition(context);
						break;
					case MemberValueExpression memberValueExpression:
						argumentValueUsageSummary = memberValueExpression.GetLeafMemberVariableDefinition(context);
						break;
					default:
						// No other known expressions that have an Array result type should exist at the moment.
						return;
				}

				var cellsField = resultValueUsageSummary
					.Fields
					.TryGetMemberUsageSummary("Cells");

				if (cellsField != null)
				{
					var cellValueUsages = cellsField.EnumerationResultUsageSummaries
						.Select(iterator => iterator.Fields.TryGetMemberUsageSummary("Value"))
						.Where(value => value != null);

					foreach (var cellValueUsage in cellValueUsages)
						argumentValueUsageSummary.AddEnumerationResultUsageSummary(cellValueUsage);
				}
			}
		}
	}
}
