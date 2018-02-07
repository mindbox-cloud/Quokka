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
	}
}
