using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mindbox.Quokka.Tests
{
	[TestClass]
	public class Sha256HashFunctionTests
	{
		[TestMethod]
		public void Invoke_ReturnsValidHash()
		{
			var email = "strange@email.com";
			var sha256 = new Sha256HashFunction();

			var hash = sha256.Invoke(email);
			Assert.AreEqual("44B59FF2773CD61533A1CEBCBF473DB0D7960AC843AA6B346E7FB86E84827B68", hash);
		}
	}
}
