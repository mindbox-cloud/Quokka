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
				@{ for item in groupList(Collection, 3) }
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

			Assert.AreEqual(expected, result);
		}
	}
}
