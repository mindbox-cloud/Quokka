﻿using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Quokka.Tests
{
	[TestClass]
	public class TemplateFactoryTests
	{
		[TestMethod]
		public void TemplateFactory_Create_NoErrors()
		{
			var template = new TemplateFactory()
				.CreateTemplate("Simple template");

			Assert.AreEqual(
				"Simple template",
				template.Apply(new CompositeModelValue()));
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void TemplateFactory_Create_Error()
		{
			new TemplateFactory().CreateTemplate("${");
		}

		[TestMethod]
		public void TemplateFactory_TryCreate_NoErrors()
		{
			IList<ITemplateError> errors;
			var template = new TemplateFactory()
				.TryCreateTemplate("Simple template", out errors);

			Assert.IsFalse(errors.Any());
			Assert.AreEqual(
				"Simple template",
				template.Apply(new CompositeModelValue()));
		}

		[TestMethod]
		public void TemplateFactory_TryCreate_Error()
		{
			IList<ITemplateError> errors;
			var template = new TemplateFactory()
				.TryCreateTemplate("${", out errors);

			Assert.AreEqual(1, errors.Count);
			Assert.IsNull(template);
		}

		[TestMethod]
		public void TemplateFactory_Create_AddedCustomFunction()
		{
			IList<ITemplateError> errors;
			var template = new TemplateFactory(new TemplateFunction[] { new TestCustomFunction() })
				.TryCreateTemplate("${ TestSubstitution(\"line\") }", out errors);

			Assert.IsFalse(errors.Any());
			Assert.AreEqual(
				"Test",
				template.Apply(new CompositeModelValue()));
		}

		private class TestCustomFunction : TemplateFunction<string, string>
		{
			public TestCustomFunction()
				: base("TestSubstitution", new TemplateFunctionArgument<string>("str"))
			{
			}

			public override string Invoke(string argument)
			{
				return "Test";
			}
		}
	}
}