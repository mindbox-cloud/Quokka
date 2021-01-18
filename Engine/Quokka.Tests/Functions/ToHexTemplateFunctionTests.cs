using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mindbox.Quokka.Tests
{
	[TestClass]
	public class ToHexTemplateFunctionTests
	{
		[TestMethod]
		public void Invoke_ReturnsValidHexValue()
		{
			var email = "https://strange@email.com?some=parame&other#things";
			var hexValue = new ToHexTemplateFunction();

			var hash = hexValue.Invoke(email);
			Assert.AreEqual(
				"68747470733A2F2F737472616E676540656D61696C2E636F6D3F736F6D653D706172616D65266F74686572237468696E6773",
				hash);
		}
	}
}
