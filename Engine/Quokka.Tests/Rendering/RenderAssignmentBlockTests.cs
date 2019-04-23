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
		[ExpectedException(typeof(InvalidTemplateModelException))]
		public void Render_AssignmentOfArrayElementProperty_IncompatibleTypes_Throws()
		{
			var template = new Template(@"
				@{ set maxprice = 0 }
				@{ for item in SomeArray}
					@{ if 1 > maxprice }
						@{ set maxprice = item.SomeOtherParameter }
					@{ end if }
				@{ end for }
				${ maxprice }");

			template.Render(
				new CompositeModelValue(
					new ModelField("SomeArray", new ArrayModelValue(
						new CompositeModelValue(
							new ModelField("SomeParameter", new PrimitiveModelValue("TEST")))))
					));
		}

		[TestMethod]
		public void Render_AssignmentOfArrayElementProperty_UsageOfDecimalParameterWithIntInitializedVariable()
		{
			var template = new Template(@"
				@{ set maxprice = 0 }
				@{ if 1 > maxprice }
					@{ set maxprice = SomeParameter }
				@{ end if }
				${ maxprice }");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("SomeParameter", new PrimitiveModelValue(1.1m)))
				);

			TemplateAssert.AreOutputsEquivalent("1,1", result);
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

		private class FaultyFunction : ScalarTemplateFunction
		{
			public FaultyFunction()
				: base("fail", typeof(int))
			{
			}

			internal override object GetScalarInvocationResult(
				RenderContext renderContext,
				IList<VariableValueStorage> argumentsValues)
			{
				throw new Exception("Error");
			}
		}
	}
}
