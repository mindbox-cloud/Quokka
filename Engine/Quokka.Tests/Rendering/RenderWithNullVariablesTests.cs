using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Quokka.Tests
{
	[TestClass]
	public class RenderWithNullVariablesTests
	{
		[TestMethod]
		[ExpectedException(typeof(UnrenderableTemplateModelException))]
		public void Render_OutputNullPrimitive_UnrenderableTemplate()
		{
			var template = new Template(@"
				${ A }
			");

			template.Render(
				new CompositeModelValue(
					new ModelField("A", new PrimitiveModelValue(null))));
		}

		[TestMethod]
		[ExpectedException(typeof(UnrenderableTemplateModelException))]
		public void Render_OutputNullCompositeField_UnrenderableTemplate()
		{
			var template = new Template(@"
				${ A.Property }
			");

			template.Render(
				new CompositeModelValue(
					new ModelField("A",
						new CompositeModelValue(
							new ModelField("Property", new PrimitiveModelValue(null))))));
		}

		[TestMethod]
		[ExpectedException(typeof(UnrenderableTemplateModelException))]
		public void Render_ArithmeticsOnNullPrimitive_UnrenderableTemplate()
		{
			var template = new Template(@"
				@{ if A > 5 }
					Greater than five!
				@{ end if }
			");

			template.Render(
				new CompositeModelValue(
					new ModelField("A", new PrimitiveModelValue(null))));
		}

	}
}
