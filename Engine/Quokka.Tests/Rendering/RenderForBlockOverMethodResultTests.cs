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

using Mindbox.Quokka.Tests;

namespace Mindbox.Quokka
{
	[TestClass]
    public class RenderForBlockOverMethodResultTests
	{
		[TestMethod]
		public void Render_ForBlock_OnMethodResult_ArrayOfPrimitives()
		{
			var template = new Template(@"
				@{ for item in Input.GetValues() }
					${ item }
				@{ end for }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField(
						"Input",
						new CompositeModelValue(
							new ModelMethod(
								"GetValues",
								new ArrayModelValue(
									new PrimitiveModelValue(1),
									new PrimitiveModelValue(2),
									new PrimitiveModelValue(3)))))));

			var expected = @"				
					1				
					2				
					3				
			";

			TemplateAssert.AreOutputsEquivalent(expected, result);
		}

		[TestMethod]
		public void Render_ForBlock_OnMethodChainResult()
		{
			var template = new Template(@"
				@{ for item in Items.FilterNonDeleted().Take(5) }
					${ item }
				@{ end for }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField(
						"Items",
						new CompositeModelValue(
							new ModelMethod(
								"FilterNonDeleted",
								new CompositeModelValue(
									new ModelMethod(
										"Take",
										new object[] { 5 },
										new ArrayModelValue(
											new PrimitiveModelValue(1),
											new PrimitiveModelValue(2),
											new PrimitiveModelValue(3)))))))));

			var expected = @"				
					1				
					2				
					3				
			";

			TemplateAssert.AreOutputsEquivalent(expected, result);
		}

		[TestMethod]
		public void Render_ForBlock_IteratingOverLoopVariableMethod()
		{
			var template = new Template(@"
				@{ for group in Objects.GetGroups() }
					@{ for item	in group.GetItems('all') }
						${ group.Name }: ${ item.Name }
					@{ end for }
				@{ end for }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField(
						"Objects",
						new CompositeModelValue(
							new ModelMethod(
								"GetGroups",
								new ArrayModelValue(
									new CompositeModelValue(
										new[] { new ModelField("Name", "Group1") },
										new[]
										{
											new ModelMethod(
												"GetItems",
												new object[] { "all" },
												new ArrayModelValue(
													new CompositeModelValue(new ModelField("Name", "Item1")),
													new CompositeModelValue(new ModelField("Name", "Item2"))))
										}),
									new CompositeModelValue(
										new[] { new ModelField("Name", "Group2") },
										new[]
										{
											new ModelMethod(
												"GetItems",
												new object[] { "all" },
												new ArrayModelValue(
													new CompositeModelValue(new ModelField("Name", "Item3")),
													new CompositeModelValue(new ModelField("Name", "Item4"))))
										})))))));

			var expected = @"				
					Group1: Item1				
					Group1: Item2	
					Group2: Item3
					Group2: Item4
			";

			TemplateAssert.AreOutputsEquivalent(expected, result);
		}
	}
}
