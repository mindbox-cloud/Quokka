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
		public void Render_AssignmentBlock_AssignmentInsideFalseBranch()
		{
			var template = new Template(@"
				@{ if 1 < 0 }
					@{ set a = 5 }
				@{ end if }

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
