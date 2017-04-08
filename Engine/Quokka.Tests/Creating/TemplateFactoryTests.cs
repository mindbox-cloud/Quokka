using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mindbox.Quokka.Tests
{
	[TestClass]
	public class TemplateFactoryTests
	{
		[TestMethod]
		public void TemplateFactory_Create_NoErrors()
		{
			var template = new DefaultTemplateFactory()
				.CreateTemplate("Simple template");

			Assert.AreEqual(
				"Simple template",
				template.Render(new CompositeModelValue()));
		}

		[TestMethod]
		[ExpectedException(typeof(TemplateContainsErrorsException))]
		public void TemplateFactory_Create_Error()
		{
			new DefaultTemplateFactory().CreateTemplate("${");
		}

		[TestMethod]
		public void TemplateFactory_TryCreate_NoErrors()
		{
			IList<ITemplateError> errors;
			var template = new DefaultTemplateFactory()
				.TryCreateTemplate("Simple template", out errors);

			Assert.IsFalse(errors.Any());
			Assert.AreEqual(
				"Simple template",
				template.Render(new CompositeModelValue()));
		}

		[TestMethod]
		public void TemplateFactory_TryCreate_Error()
		{
			IList<ITemplateError> errors;
			var template = new DefaultTemplateFactory()
				.TryCreateTemplate("${", out errors);

			Assert.AreEqual(1, errors.Count);
			Assert.IsNull(template);
		}

		[TestMethod]
		public void TemplateFactory_Create_AddedCustomFunction()
		{
			IList<ITemplateError> errors;
			var template = new DefaultTemplateFactory(new TemplateFunction[] { new TestCustomFunction() })
				.TryCreateTemplate("${ TestSubstitution(\"line\") }", out errors);

			Assert.IsFalse(errors.Any());
			Assert.AreEqual(
				"Test",
				template.Render(new CompositeModelValue()));
		}

		private class TestCustomFunction : ScalarTemplateFunction<string, string>
		{
			public TestCustomFunction()
				: base("TestSubstitution", new StringFunctionArgument("str"))
			{
			}

			public override string Invoke(string argument)
			{
				return "Test";
			}
		}
	}
}
