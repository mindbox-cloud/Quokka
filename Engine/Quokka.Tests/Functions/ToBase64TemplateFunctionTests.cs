using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mindbox.Quokka.Tests
{
	[TestClass]
	public class ToBase64TemplateFunctionTests
	{
		[TestMethod]
		public void Invoke_ReturnsValidBase64Value()
		{
			var someString = "just-test-string";
			var toBase64 = new ToBase64TemplateFunction();

			var base64Value = toBase64.Invoke(someString);
			Assert.AreEqual(
				"anVzdC10ZXN0LXN0cmluZw==",
				base64Value);
		}
	}
}
