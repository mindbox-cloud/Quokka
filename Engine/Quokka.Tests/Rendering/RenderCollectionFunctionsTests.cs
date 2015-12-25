using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Quokka.Tests
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
		[ExpectedException(typeof(UnrenderableTemplateModelException))]
		public void Render_ForBlockTableRows_AccessingNullCellValue_Error()
		{
			var template = new Template(@"
				@{ for row in tableRows(Collection, 3) }
					@{ for cell in row.Cells }
						${ cell.Value }
					@{ end for }
				@{ end for }
			");

			template.Render(
				new CompositeModelValue(
					new ModelField("Collection",
						new ArrayModelValue(
							Enumerable.Range(1, 1)
								.Select(x => new PrimitiveModelValue(x))))));
		}
	}
}
