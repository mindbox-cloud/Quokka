using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mindbox.Quokka
{
	[TestClass]
    public class RenderExceptionsTests
    {
		[TestMethod]
		[ExpectedException(typeof(UnrenderableTemplateModelException))]
	    public void Render_FunctonIvocationError_UnrendereableException()
		{
			var template = new DefaultTemplateFactory(new[] { new FaultyFunction() })
				.CreateTemplate("${ fail() }");

			template.Render(new CompositeModelValue());
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
