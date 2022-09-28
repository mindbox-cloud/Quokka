// // Copyright 2022 Mindbox Ltd
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

using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mindbox.Quokka.Tests
{
	[TestClass]
	public class RenderCollectionFunctionsTests
	{
		[TestMethod]
		public void Render_CountCollection_EmptyColection_0()
		{
			var template = new Template(@"${ count(Collection) }");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Collection",
						new ArrayModelValue(
							Enumerable.Range(1, 0)
								.Select(x => new PrimitiveModelValue(x))))));

			var expected = @"0";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_CountCollection_NonEmptyCollection_CorrectCount()
		{
			var template = new Template(@"${ count(Collection) }");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Collection",
						new ArrayModelValue(
							Enumerable.Range(1, 7)
								.Select(x => new PrimitiveModelValue(x))))));

			var expected = @"7";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_ForBlockTableRows_LoopOnRowsOnly_9Elements3Columns()
		{
			var template = new Template(@"
				@{ for row in tableRows(Collection, 3) }
					Row
				@{ end for }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Collection",
						new ArrayModelValue(
							Enumerable.Range(1, 9)
								.Select(x => new PrimitiveModelValue(x))))));

			var expected = @"
					Row
					Row
					Row
			";

			TemplateAssert.AreOutputsEquivalent(expected, result);
		}

		[TestMethod]
		public void Render_ForBlockTableRows_LoopOnRowsOnly_0Elements3Columns()
		{
			var template = new Template(@"
				@{ for row in tableRows(Collection, 3) }
					Row
				@{ end for }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Collection",
						new ArrayModelValue(
							Enumerable.Range(1, 0)
								.Select(x => new PrimitiveModelValue(x))))));

			var expected = @"";

			TemplateAssert.AreOutputsEquivalent(expected, result);
		}
		
		[TestMethod]
		public void Render_ForBlockTableRows_LoopOnCellsWithValueOutput_9Elements3Columns()
		{
			var template = new Template(@"
				@{ for row in tableRows(Collection, 3) }
					@{ for cell in row.Cells }
						${ cell.Value }
					@{ end for }
				@{ end for }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Collection",
						new ArrayModelValue(
							Enumerable.Range(1, 9)
								.Select(x => new PrimitiveModelValue(x))))));

			var expected = @"
				1
				2
				3
				4
				5
				6
				7
				8
				9
			";

			TemplateAssert.AreOutputsEquivalent(expected, result);
		}

		[TestMethod]
		public void Render_ForBlockTableRows_LoopOnCellsWithValueOutput_7Elements3Columns()
		{
			var template = new Template(@"
				@{ for row in tableRows(Collection, 3) }
					@{ for cell in row.Cells }
						@{ if cell.Value != null }
							${ cell.Value }
						@{ else }
							No element
						@{ end if }
					@{ end for }
				@{ end for }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Collection",
						new ArrayModelValue(
							Enumerable.Range(1, 7)
								.Select(x => new PrimitiveModelValue(x))))));

			var expected = @"
				1
				2
				3
				4
				5
				6
				7
				No element
				No element
			";

			TemplateAssert.AreOutputsEquivalent(expected, result);
		}

		[TestMethod]
		public void Render_ForBlockTableRows_LoopOnCellsWithValueOutput_1Element3Columns()
		{
			var template = new Template(@"
				@{ for row in tableRows(Collection, 3) }
					@{ for cell in row.Cells }
						@{ if cell.Value != null }
							${ cell.Value }
						@{ else }
							No element
						@{ end if }
					@{ end for }
				@{ end for }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Collection",
						new ArrayModelValue(
							Enumerable.Range(1, 1)
								.Select(x => new PrimitiveModelValue(x))))));

			var expected = @"
				1
				No element
				No element
			";

			TemplateAssert.AreOutputsEquivalent(expected, result);
		}

		[TestMethod]
		public void Render_ForBlockTableRows_LoopOnCellsWithValueOutput_1Column()
		{
			var template = new Template(@"
				@{ for row in tableRows(Collection, 1) }
					@{ for cell in row.Cells }
						@{ if cell.Value != null }
							${ cell.Value }
						@{ else }
							No element
						@{ end if }
					@{ end for }
				@{ end for }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Collection",
						new ArrayModelValue(
							Enumerable.Range(1, 3)
								.Select(x => new PrimitiveModelValue(x))))));

			var expected = @"
				1
				2
				3
			";

			TemplateAssert.AreOutputsEquivalent(expected, result);
		}

		[TestMethod]
		public void Render_ForBlockTableRows_SingleRow_IsFirst_IsLast()
		{
			var template = new Template(@"
				@{ for row in tableRows(Collection, 3) }
					${ if (row.isFirst, ""First row"", ""Non-first row"") }
					${ if (row.isLast, ""Last row"", ""Non-last row"") }
				@{ end for }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Collection",
						new ArrayModelValue(
							Enumerable.Range(1, 3)
								.Select(x => new PrimitiveModelValue(x))))));

			var expected = @"
				First row
				Last row
			";

			TemplateAssert.AreOutputsEquivalent(expected, result);
		}

		[TestMethod]
		public void Render_ForBlockTableRows_ThreeRows_IsFirstIsLastCorrect()
		{
			var template = new Template(@"
				@{ for row in tableRows(Collection, 3) }
					----
					${ if (row.isFirst, ""First row"", ""Non-first row"") }
					${ if (row.isLast, ""Last row"", ""Non-last row"") }
				@{ end for }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Collection",
						new ArrayModelValue(
							Enumerable.Range(1, 9)
								.Select(x => new PrimitiveModelValue(x))))));

			var expected = @"
				----
				First row
				Non-last row
				----
				Non-first row
				Non-last row
				----
				Non-first row
				Last row
			";

			TemplateAssert.AreOutputsEquivalent(expected, result);
		}

		[TestMethod]
		public void Render_ForBlockTableRows_SingleRow_Index1()
		{
			var template = new Template(@"
				@{ for row in tableRows(Collection, 3) }
					${ row.Index }
				@{ end for }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Collection",
						new ArrayModelValue(
							Enumerable.Range(1000, 3)
								.Select(x => new PrimitiveModelValue(x))))));

			var expected = @"
				1
			";

			TemplateAssert.AreOutputsEquivalent(expected, result);
		}

		[TestMethod]
		public void Render_ForBlockTableRows_ThreeRows_CorrectIndices()
		{
			var template = new Template(@"
				@{ for row in tableRows(Collection, 3) }
					${ row.Index }
				@{ end for }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Collection",
						new ArrayModelValue(
							Enumerable.Range(1000, 9)
								.Select(x => new PrimitiveModelValue(x))))));

			var expected = @"
				1
				2
				3
			";

			TemplateAssert.AreOutputsEquivalent(expected, result);
		}

		[TestMethod]
		public void Render_ForBlockTableRows_SingleCell_IsFirst_IsLast()
		{
			var template = new Template(@"
				@{ for row in tableRows(Collection, 1) }
					@{ for cell in row.Cells }
						${ if (cell.isFirst, ""First cell"", ""Non-first cell"") }
						${ if (cell.isLast, ""Last cell"", ""Non-last cell"") }
					@{ end for }
				@{ end for }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Collection",
						new ArrayModelValue(
							Enumerable.Range(1, 1)
								.Select(x => new PrimitiveModelValue(x))))));

			var expected = @"
				First cell
				Last cell
			";

			TemplateAssert.AreOutputsEquivalent(expected, result);
		}

		[TestMethod]
		public void Render_ForBlockTableRows_FullRowOfCellsWithValues_IsFirstIsLast()
		{
			var template = new Template(@"
				@{ for row in tableRows(Collection, 3) }
					@{ for cell in row.Cells }
						----
						${ if (cell.isFirst, ""First cell"", ""Non-first cell"") }
						${ if (cell.isLast, ""Last cell"", ""Non-last cell"") }
					@{ end for }
				@{ end for }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Collection",
						new ArrayModelValue(
							Enumerable.Range(1, 3)
								.Select(x => new PrimitiveModelValue(x))))));

			var expected = @"
				----
				First cell
				Non-last cell
				----
				Non-first cell
				Non-last cell
				----
				Non-first cell
				Last cell
			";

			TemplateAssert.AreOutputsEquivalent(expected, result);
		}

		[TestMethod]
		public void Render_ForBlockTableRows_CellsWithoutValues_IsFirstIsLast()
		{
			var template = new Template(@"
				@{ for row in tableRows(Collection, 3) }
					@{ for cell in row.Cells }
						----
						${ if (cell.isFirst, ""First cell"", ""Non-first cell"") }
						${ if (cell.isLast, ""Last cell"", ""Non-last cell"") }
					@{ end for }
				@{ end for }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Collection",
						new ArrayModelValue(
							Enumerable.Range(1, 1)
								.Select(x => new PrimitiveModelValue(x))))));

			var expected = @"
				----
				First cell
				Non-last cell
				----
				Non-first cell
				Non-last cell
				----
				Non-first cell
				Last cell
			";

			TemplateAssert.AreOutputsEquivalent(expected, result);
		}

		[TestMethod]
		public void Render_ForBlockTableRows_FullRowOfCellsSomeWithoutValues_AllIndexes()
		{
			var template = new Template(@"
				@{ for row in tableRows(Collection, 5) }
					@{ for cell in row.Cells }
						${ cell.Index }
					@{ end for }
				@{ end for }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Collection",
						new ArrayModelValue(
							Enumerable.Range(1000, 3)
								.Select(x => new PrimitiveModelValue(x))))));

			var expected = @"
				1
				2
				3
				4
				5
			";

			TemplateAssert.AreOutputsEquivalent(expected, result);
		}

		[TestMethod]
		public void Render_ForBlockTableRows_OneFullRowOnePartialRow_ValueCount()
		{
			var template = new Template(@"
				@{ for row in tableRows(Collection, 5) }
					${ row.ValueCount}
				@{ end for }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Collection",
						new ArrayModelValue(
							Enumerable.Range(1000, 8)
								.Select(x => new PrimitiveModelValue(x))))));

			var expected = @"
				5
				3
			";

			TemplateAssert.AreOutputsEquivalent(expected, result);
		}

		[TestMethod]
		public void Render_ForBlockTableRows_CompositeValue_NullCheck()
		{
			var template = new Template(@"
				@{ for row in tableRows(Collection, 3) }
					@{ for cell in row.Cells }
						@{ if cell.Value != null }
							${ cell.Value.Name }	
						@{ else if cell.Value = null }
							Null
						@{ end if }
					@{ end for }
				@{ end for }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Collection",
						new ArrayModelValue(
							Enumerable.Range(1, 2)
								.Select(x => new CompositeModelValue(
									new ModelField("Name", "Name_" + x)))))));

			var expected = @"
				Name_1
				Name_2
				Null
			";

			TemplateAssert.AreOutputsEquivalent(expected, result);
		}



		[TestMethod]
		public void Render_ForBlockTableRows_EmptyCellValue_Exception()
		{
			var template = new Template(@"
				@{ for row in tableRows(Collection, 2) }
					@{ for cell in row.Cells }
						${ cell.Value.Name }
					@{ end for }					
				@{ end for }
			");

			try
			{
				template.Render(
					new CompositeModelValue(
						new ModelField(
							"Collection",
							new ArrayModelValue(
								new CompositeModelValue(
									new ModelField("Name", "Nikita"))))));
			}
			catch (UnrenderableTemplateModelException exception)
			{
				Assert.AreEqual(
					"An attempt to use the value of \"cell.Value\" expression which happens to be null",
					exception.Message);
				return;
			}

			Assert.Fail("Expected exception did not occur");
		}

		[TestMethod]
		public void Render_ForBlockTableRows_NullPropagation_CheckingForNull_Valid()
		{
			var template = new Template(@"
				@{ for row in tableRows(Collection, 2) }
					@{ for cell in row.Cells }
						@{ if cell.Value.Level1.Level2.Leaf = null }
							${ cell.index } is null
						@{ end if }
					@{ end for }					
				@{ end for }
			");


			var result = template.Render(
				new CompositeModelValue(
					new ModelField(
						"Collection",
						new ArrayModelValue(
							new CompositeModelValue(
								new ModelField(
									"Level1",
									new CompositeModelValue(
										new ModelField(
											"Level2",
											new CompositeModelValue(
												new ModelField("Leaf", "leaf1"))))))))));

			var expected = @"
				2 is null
			";

			TemplateAssert.AreOutputsEquivalent(expected, result);
		}

		[TestMethod]
		public void Render_ForBlockTableRows_NullPropagation_CheckingForNotNull_Valid()
		{
			var template = new Template(@"
				@{ for row in tableRows(Collection, 2) }
					@{ for cell in row.Cells }
						@{ if cell.Value.Level1.Level2.Leaf != null }
							${ cell.index } is not null
						@{ end if }
					@{ end for }					
				@{ end for }
			");


			var result = template.Render(
				new CompositeModelValue(
					new ModelField(
						"Collection",
						new ArrayModelValue(
							new CompositeModelValue(
								new ModelField(
									"Level1",
									new CompositeModelValue(
										new ModelField(
											"Level2",
											new CompositeModelValue(
												new ModelField("Leaf", "leaf1"))))))))));

			var expected = @"
				1 is not null
			";

			TemplateAssert.AreOutputsEquivalent(expected, result);
		}
	}
}
