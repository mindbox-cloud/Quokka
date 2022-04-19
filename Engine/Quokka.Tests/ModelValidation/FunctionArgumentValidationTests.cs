using Microsoft.VisualStudio.TestTools.UnitTesting;

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

			public override decimal Invoke(decimal value)
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