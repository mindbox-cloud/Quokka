
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
		public void Apply_CommentShortSyntax()
		{
			var template = new Template("Visible @{* Not Visible *} Visible");

			Assert.AreEqual(
				"Visible  Visible",
				template.Apply(new CompositeParameterValue()));
		}
	}
}
