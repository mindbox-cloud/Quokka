
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Quokka.Tests
{
	[TestClass]
	public class ApplyBasicTests
	{
		[TestMethod]
		public void Apply_SingleConstantBlock()
		{
			var template = new Template("Happy new year!");

			Assert.AreEqual(
				"Happy new year!",
				template.Apply(new CompositeParameterValue()));
		}

		[TestMethod]
		public void Apply_EmptyString()
		{
			var template = new Template("");

			Assert.AreEqual(
				"",
				template.Apply(new CompositeParameterValue()));
		}

		[TestMethod]
		public void Apply_CommentFullSyntax()
		{
			var template = new Template("Visible @{comment} Not Visible @{ end comment } Visible");

			Assert.AreEqual(
				"Visible  Visible",
				template.Apply(new CompositeParameterValue()));
		}
	}
}
