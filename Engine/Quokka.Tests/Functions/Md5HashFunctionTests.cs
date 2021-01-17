using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mindbox.Quokka.Tests
{
	[TestClass]
	public class Md5HashFunctionTests
	{
		[TestMethod]
		public void Invoke_ReturnsValidHash()
		{
			var email = "strange@email.com";
			var md5 = new Md5HashFunction();

			var hash = md5.Invoke(email);
			Assert.AreEqual("152F2DD5F1F056FC761717EA5965828C", hash);
		}
	}
}
