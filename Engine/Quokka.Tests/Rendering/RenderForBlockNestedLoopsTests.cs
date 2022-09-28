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
    public class RenderForBlockNestedLoopsTests
	{

		[TestMethod]
		public void Render_ForBlock_NestedForOnDifferentCollection()
		{
			var template = new Template(@"
				@{ for coef in Coefficients }
					@{ for value in Values }
						${ coef * value }
					@{ end for }
				@{ end for }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Coefficients",
						new ArrayModelValue(
							new PrimitiveModelValue(2),
							new PrimitiveModelValue(3))),
					new ModelField("Values",
						new ArrayModelValue(
							new PrimitiveModelValue(5),
							new PrimitiveModelValue(6)))));

			var expected = @"
				
					
						10
					
						12
					
				
					
						15
					
						18
					
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_ForBlock_NestedForOnElementField()
		{
			var template = new Template(@"
				@{ for value in Values }
					@{ for coef in value.Coefficients }
						${ coef * value.Number }
					@{ end for }
				@{ end for }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Values",
						new ArrayModelValue(
							new CompositeModelValue(
								new ModelField("Number", 2),
								new ModelField("Coefficients",
									new ArrayModelValue(
										new PrimitiveModelValue(5),
										new PrimitiveModelValue(6)))),
							new CompositeModelValue(
								new ModelField("Number", 3),
								new ModelField("Coefficients",
									new ArrayModelValue(
										new PrimitiveModelValue(5),
										new PrimitiveModelValue(6))))))));

			var expected = @"
				
					
						10
					
						12
					
				
					
						15
					
						18
					
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_ForBlock_NestedForOnElementItself()
		{
			var template = new Template(@"
				@{ for array in Arrays }
					@{ for number in array }
						${ number }
					@{ end for }
				@{ end for }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Arrays",
						new ArrayModelValue(
							new ArrayModelValue(
								new PrimitiveModelValue(22),
								new PrimitiveModelValue(24)),
							new ArrayModelValue(
								new PrimitiveModelValue(52),
								new PrimitiveModelValue(54))))));

			var expected = @"
				
					
						22
					
						24
					
				
					
						52
					
						54
					
				
			";

			Assert.AreEqual(expected, result);
		}
	}
}
