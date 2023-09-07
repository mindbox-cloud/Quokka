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

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Mindbox.Quokka.Abstractions;

namespace Mindbox.Quokka.Tests
{
	[TestClass]
	public class FunctionArgumentValidationTests
	{
		private class TestFunction : ScalarTemplateFunction<decimal, decimal>
		{
			public TestFunction() 
				: base("validate",
					  new DecimalFunctionArgument("value", valueValidator: Validate))
			{
			}

			private static ArgumentValueValidationResult Validate(decimal arg)
			{
				return arg >= 5
					? new ArgumentValueValidationResult(true)
					: new ArgumentValueValidationResult(false, "Test");
			}

			public override decimal Invoke(RenderSettings settings, decimal value)
			{
				return value;
			}
		}

		[TestMethod]
		[ExpectedException(typeof(TemplateContainsErrorsException))]
		public void InvalidArgument_Fails()
		{
			var template = new Template(
				@"${ validate(4) }",
				new FunctionRegistry(new[] { new TestFunction() }));

			template.Render(new CompositeModelValue());
		}

		[TestMethod]
		public void ValidArgument_Ok()
		{
			var template = new Template(
				@"${ validate(5) }", 
				new FunctionRegistry(new[] { new TestFunction() }));

			var result = template.Render(new CompositeModelValue());
			Assert.AreEqual("5", result);
		}

		[TestMethod]
		public void InvliadArgument_InCondition_Error()
		{
			var template = new Template(@"
				@{ if toUpper(value) }
					Empty.
				@{ end if }
			", new FunctionRegistry(Template.GetStandardFunctions()), false);

			Assert.AreEqual(1, template.Errors.Count);
			Assert.AreEqual("Function toUpper has incorrect result type. Expected Boolean, but got String",
				((SemanticError)template.Errors[0]).Message);
		}
	}
}