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
		[TestMethod]
		[ExpectedException(typeof(UnrenderableTemplateModelException))]
		public void Render_DivisionByZero_UnrendereableException()
		{
			var template = new DefaultTemplateFactory(new[] { new FaultyFunction() })
				.CreateTemplate("${ 5 / 0 }");

			template.Render(new CompositeModelValue());
		}


		[TestMethod]
		[ExpectedException(typeof(UnrenderableTemplateModelException))]
		public void Render_AssignmentBlock_AssignmentInsideEmptyLoop()
		{
			var template = new Template(@"
				@{ for p in ps }
					@{ set a = 5 }
				@{ end for }

				${ a }");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("ps",
						new ArrayModelValue())));
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
