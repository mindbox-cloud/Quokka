using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Quokka.Tests
{
	[TestClass]
	public class RenderBasicTests
	{
		[TestMethod]
		public void Render_SingleConstantBlock()
		{
			var template = new Template("Happy new year!");

			Assert.AreEqual(
				"Happy new year!",
				template.Render(new CompositeModelValue()));
		}

		[TestMethod]
		public void Render_EmptyString()
		{
			var template = new Template("");

			Assert.AreEqual(
				"",
				template.Render(new CompositeModelValue()));
		}

		[TestMethod]
		public void Render_Comment()
		{
			var template = new Template("Visible @{* Not Visible *} Visible");

			Assert.AreEqual(
				"Visible  Visible",
				template.Render(new CompositeModelValue()));
		}

		[TestMethod]
		public void Render_StringConstantOutput()
		{
			var template = new Template("${ \"Constant value  \" }");

			Assert.AreEqual(
				"Constant value  ",
				template.Render(new CompositeModelValue()));
		}

		[TestMethod]
		public void Render_BooleanTrueOutput()
		{
			var template = new Template("${ A or A }");

			Assert.AreEqual(
				"True",
				template.Render(
					new CompositeModelValue(
						new ModelField("A", true))));
		}

		[TestMethod]
		public void Render_BooleanFalseOutput()
		{
			var template = new Template("${ not A }");

			Assert.AreEqual(
				"False",
				template.Render(
					new CompositeModelValue(
						new ModelField("A", true))));
		}

		[TestMethod]
		public void Render_DoubleQuotedString()
		{
			var template = new Template(@"${ ""Some 'value'"" }");

			Assert.AreEqual(
				"Some 'value'",
				template.Render(
					new CompositeModelValue()));
		}

		[TestMethod]
		public void Render_SingleQuotedString()
		{
			var template = new Template(@"${ 'Some ""value""' }");

			Assert.AreEqual(
				"Some \"value\"",
				template.Render(
					new CompositeModelValue()));
		}
	}
}
