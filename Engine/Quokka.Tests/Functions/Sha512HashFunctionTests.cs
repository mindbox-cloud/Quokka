using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mindbox.Quokka.Tests
{
	[TestClass]
	public class Sha512HashFunctionTests
	{
		[TestMethod]
		public void Invoke_ReturnsValidHash()
		{
			var email = "strange@email.com";
			var sha256 = new Sha512HashFunction();

			var hash = sha256.Invoke(email);
			Assert.AreEqual(
				"36A612F958405DB0EB1D456BE3C25867CC9895D1A10696205F7655728E94229362F0511AF4029692D72F1C421A5213F5AFC20B1CAF5271CFB6D1018624B17CE7",
				hash);
		}
	}
}
