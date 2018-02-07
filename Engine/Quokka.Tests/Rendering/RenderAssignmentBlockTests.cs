using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mindbox.Quokka.Tests
{
	[TestClass]
	public class RenderAssignmentBlockTests
	{
		[TestMethod]
		public void Render_AssignmentBlock_ArithmeticExpression()
		{
			var template = new Template(@"@{ set a = 353 + 255 }${ a }");

			var result = template.Render(
				new CompositeModelValue());
			Assert.AreEqual("608", result);
		}

		[TestMethod]
		public void Render_AssignmentBlock_MultipleAssignments()
		{
			var template = new Template(@"
				@{ set a = 1 }
				@{ set a = 2 }
				${ a }");

			var result = template.Render(
				new CompositeModelValue());
			TemplateAssert.AreOutputsEquivalent("2", result);
		}

		[TestMethod]
		public void Render_AssignmentBlock_OutOfScopeAssignments()
		{
			var template = new Template(@"
				@{ set a = 55 }
				@{ for p in ps }
					@{ set a = a + 5 }
				@{ end for }
				${ a }");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("ps",
						new ArrayModelValue(
							new PrimitiveModelValue(1),
							new PrimitiveModelValue(2),
							new PrimitiveModelValue(3)))));


			TemplateAssert.AreOutputsEquivalent("70", result);
		}

		[TestMethod]
		public void Render_AssignmentBlock_()
		{
			var template = new DefaultTemplateFactory(new[] { new FaultyFunction() })
				.CreateTemplate(@"
					@{ set a = b + c }
					${ a.Name }");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Recipient",
						new CompositeModelValue(
							new ModelField("Name",
								new PrimitiveModelValue("Roma"))))));


			TemplateAssert.AreOutputsEquivalent("Roma", result);
		}

		private class FaultyFunction : ScalarTemplateFunction
		{
			public FaultyFunction()
				: base("fail", typeof(int))
			{
			}

			internal override object GetScalarInvocationResult(IList<VariableValueStorage> argumentsValues)
			{
				throw new Exception("Error");
			}
		}
	}
}
