using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Mindbox.Quokka.Tests;

namespace Mindbox.Quokka
{
	[TestClass]
    public class RenderIfBlockVariantValuesTests
    {
		[TestMethod]
	    public void Render_IfCondition_MethodResult()
	    {
			var template = new Template(@"
				@{ if Recipient.GetValue('IsMale') }
					Male
				@{ end if }");

		    var result = template.Render(
			    new CompositeModelValue(
				    new ModelField(
					    "Recipient",
					    new CompositeModelValue(
						    new ModelMethod("GetValue", new object[] { "IsMale" }, true)))));

		    var expected = @"
				Male
			";

		    TemplateAssert.AreOutputsEquivalent(expected, result);
		}
    }
}
