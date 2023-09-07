// // Copyright 2022 Mindbox Ltd
// //
// // Licensed under the Apache License, Version 2.0 (the "License");
// // you may not use this file except in compliance with the License.
// // You may obtain a copy of the License at
// //
// //     http://www.apache.org/licenses/LICENSE-2.0
// //
// // Unless required by applicable law or agreed to in writing, software
// // distributed under the License is distributed on an "AS IS" BASIS,
// // WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// // See the License for the specific language governing permissions and
// // limitations under the License.

using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Mindbox.Quokka.Abstractions;

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

			public override string Invoke(RenderSettings settings, string argument)
			{
				return "Test";
			}
		}
	}
}
