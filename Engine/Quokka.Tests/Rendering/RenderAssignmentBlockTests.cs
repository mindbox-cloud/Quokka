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
