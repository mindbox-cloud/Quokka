using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Quokka.Tests
{
	[TestClass]
	public class RenderForBlockTableTests
	{
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
							new PrimitiveModelValue(1),
							new PrimitiveModelValue(2),
							new PrimitiveModelValue(3)))));

			var expected = @"
				
					Row
				
					Row
				
					Row
				
			";

			Assert.AreEqual(expected, result);
		}
	}
}
