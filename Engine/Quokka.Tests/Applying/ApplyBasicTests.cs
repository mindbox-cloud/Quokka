
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
				template.Apply(new CompositeModelValue()));
		}

		[TestMethod]
		public void Apply_EmptyString()
		{
			var template = new Template("");

			Assert.AreEqual(
				"",
				template.Apply(new CompositeModelValue()));
		}

		[TestMethod]
		public void Apply_Comment()
		{
			var template = new Template("Visible @{* Not Visible *} Visible");

			Assert.AreEqual(
				"Visible  Visible",
				template.Apply(new CompositeModelValue()));
		}

		[TestMethod]
		public void Apply_StringConstantOutput()
		{
			var template = new Template("${ \"Constant value  \" }");

			Assert.AreEqual(
				"Constant value  ",
				template.Apply(new CompositeModelValue()));
		}

		[TestMethod]
		public void Apply_BooleanTrueOutput()
		{
			var template = new Template("${ A or A }");

			Assert.AreEqual(
				"True",
				template.Apply(
					new CompositeModelValue(
						new ModelField("A", true))));
		}

		[TestMethod]
		public void Apply_BooleanFalseOutput()
		{
			var template = new Template("${ not A }");

			Assert.AreEqual(
				"False",
				template.Apply(
					new CompositeModelValue(
						new ModelField("A", true))));
		}
	}
}
