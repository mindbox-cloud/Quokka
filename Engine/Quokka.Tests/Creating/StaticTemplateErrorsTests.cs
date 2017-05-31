using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mindbox.Quokka
{
	[TestClass]
    public class StaticTemplateErrorsTests
    {
	    [TestMethod]
	    public void CreateTemplate_NonConstantMethodValue_SemanticError()
	    {
		    new DefaultTemplateFactory()
			    .TryCreateTemplate(
				    "${ Root.GetValue(A) }",
				    out IList<ITemplateError> errors);

		    Assert.AreEqual(1, errors.Count);
	    }
	}
}
