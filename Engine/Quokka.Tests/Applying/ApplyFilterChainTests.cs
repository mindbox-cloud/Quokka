using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Quokka.Tests
{
	[TestClass]
	public class ApplyFilterChainTests
	{
		[TestMethod]
		public void Apply_FilterChain_SinlgeFilter_SingleParameter()
		{
			var template = new Template("${ \"iceland\" | ToUpper() }");

			var result = template.Apply(new CompositeModelValue());

			Assert.AreEqual("ICELAND", result);
		}

		[TestMethod]
		public void Apply_FilterChain_SingleFilter_MultipleParameters()
		{
			var template = new Template("${ Name | replaceIfEmpty(\"Marla\") }");

			var result = template.Apply(
				new CompositeModelValue(
					new ModelField("Name", "")));

			Assert.AreEqual("Marla", result);
		}

		[TestMethod]
		public void Apply_FilterChain_MultipleFilters()
		{
			var template = new Template("${ Name | replaceIfEmpty(\"Marla\") | ToUpper() }");

			var result = template.Apply(
				new CompositeModelValue(
					new ModelField("Name", "")));

			Assert.AreEqual("MARLA", result);
		}

		[TestMethod]
		public void Apply_FilterChain_If()
		{
			var template = new Template("${ IsTest | if (\"test.example.com\", \"example.com\") }");

			var result = template.Apply(
				new CompositeModelValue(
					new ModelField("IsTest", true)));

			Assert.AreEqual("test.example.com", result);
		}
	}
}
