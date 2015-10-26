using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Quokka.Tests.Html
{
	[TestClass]
	public class HtmlTemplateBasicTests
	{
		[TestMethod]
		public void Html_PlainTextWithParameterOutput()
		{
			var template = new HtmlTemplate("Hello, ${ Name }");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Name", "Angelina")));

			Assert.AreEqual("Hello, Angelina", result);
		}

		[TestMethod]
		public void Html_SingleAhref_WithoutOutputBlocks()
		{
			var template = new HtmlTemplate("<a href=\"http://example.com\">");

			var result = template.Render(new CompositeModelValue());

			Assert.AreEqual("<a href=\"http://example.com\">", result);
		}

		[TestMethod]
		public void Html_SingleAhref_WithMultipleOutputBlocks()
		{
			var template = new HtmlTemplate("<a href=\"${ \"https\" }://example.com/${ 4 + 13 }\">");

			var result = template.Render(new CompositeModelValue());

			Assert.AreEqual("<a href=\"https://example.com/17\">", result);
		}

		[TestMethod]
		public void Html_SingleAhref_SingleOutputBlock()
		{
			var template = new HtmlTemplate("<a href=\"${ 1 * 2 * 3 * 4 * 5 }\">");

			var result = template.Render(new CompositeModelValue());

			Assert.AreEqual("<a href=\"120\">", result);
		}
	}
}
