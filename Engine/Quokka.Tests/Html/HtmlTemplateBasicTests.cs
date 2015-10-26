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
	}
}
