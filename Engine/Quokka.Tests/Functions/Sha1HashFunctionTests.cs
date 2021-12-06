using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mindbox.Quokka.Tests
{
	[TestClass]
	public class Sha1HashFunctionTests
	{
		[TestMethod]
		public void Invoke_ReturnsValidHash()
		{
			var email = "strange@email.com";
			var sha1 = new Sha1HashFunction();

			var hash = sha1.Invoke(email);
			Assert.AreEqual("4AD63D3592358CE97B2FC7F6C570C73944B278B5", hash);
		}
	}
}
