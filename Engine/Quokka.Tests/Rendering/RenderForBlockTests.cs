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

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mindbox.Quokka.Tests
{
	[TestClass]
	public class RenderForBlockTests
	{
		[TestMethod]
		public void Render_ForBlock_SimpleFor_CollectionOfPrimitives_NoItemOutput()
		{
			var template = new Template(@"
				@{ for item in Collection }
					List element
				@{ end for }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Collection",
						new ArrayModelValue(
							new PrimitiveModelValue(1),
							new PrimitiveModelValue(2),
							new PrimitiveModelValue(3)))));

			var expected = @"				
					List element				
					List element				
					List element				
			";

			TemplateAssert.AreOutputsEquivalent(expected, result);
		}

		[TestMethod]
		public void Render_ForBlock_SimpleFor_CollectionOfPrimitives_ItemOutput()
		{
			var template = new Template(@"
				@{ for item in Collection }
					${ item }
				@{ end for }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Collection",
						new ArrayModelValue(
							new PrimitiveModelValue(1),
							new PrimitiveModelValue(2),
							new PrimitiveModelValue(3)))));

			var expected = @"				
					1				
					2				
					3				
			";

			TemplateAssert.AreOutputsEquivalent(expected, result);
		}

		[TestMethod]
		public void Render_ForBlock_SimpleFor_CollectionOfComposites_NoItemOutput()
		{
			var template = new Template(@"
				@{ for item in Collection }
					List element
				@{ end for }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Collection",
						new ArrayModelValue(
							new CompositeModelValue(
								new ModelField("Name", "Carl")),
							new CompositeModelValue(
								new ModelField("Name", "Ashley")),
							new CompositeModelValue(
								new ModelField("Name", "Malcolm"))))));

			var expected = @"				
					List element				
					List element				
					List element				
			";

			TemplateAssert.AreOutputsEquivalent(expected, result);
		}

		[TestMethod]
		public void Render_ForBlock_SimpleFor_CollectionOfComposites_FirstLevelMemberOutput()
		{
			var template = new Template(@"
				@{ for item in Collection }
					${ item.Name }
				@{ end for }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Collection",
						new ArrayModelValue(
							new CompositeModelValue(
								new ModelField("Name", "Carl")),
							new CompositeModelValue(
								new ModelField("Name", "Ashley")),
							new CompositeModelValue(
								new ModelField("Name", "Malcolm"))))));

			var expected = @"				
					Carl				
					Ashley				
					Malcolm				
			";

			TemplateAssert.AreOutputsEquivalent(expected, result);
		}

		[TestMethod]
		public void Render_ForBlock_SimpleFor_EmptyCollection()
		{
			var template = new Template(@"
				(start)@{ for item in Collection }
					List element
				@{ end for }(end)
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Collection",
						new ArrayModelValue())));

			var expected = @"
				(start)(end)
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_ForBlock_SimpleFor_CollectionAsANthLevelMember()
		{
			var template = new Template(@"
				@{ for item in Context.Data.Elements }
					${ item }
				@{ end for }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Context",
						new CompositeModelValue(
							new ModelField("Data",
								new CompositeModelValue(
									new ModelField("Elements",
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
		public void Render_ForBlock_SimpleFor_IfOnElement()
		{
			var template = new Template(@"
				@{ for item in Elements }
					@{ if item != 2 }
						${ item }
					@{ end if }
				@{ end for }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Elements",
						new ArrayModelValue(
							new PrimitiveModelValue(1),
							new PrimitiveModelValue(2),
							new PrimitiveModelValue(3)))));

			var expected = @"
						1
						3
			";

			TemplateAssert.AreOutputsEquivalent(expected, result);
		}

		[TestMethod]
		public void Render_ForBlock_MultipleForsOnSameCollection()
		{
			var template = new Template(@"
				@{ for item in Collection }
					${ item }
				@{ end for }
				@{ for item in Collection }
					${ item }
				@{ end for }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Collection",
						new ArrayModelValue(
							new PrimitiveModelValue(1),
							new PrimitiveModelValue(2),
							new PrimitiveModelValue(3)))));

			var expected = @"				
					1				
					2				
					3
					1				
					2				
					3				
			";

			TemplateAssert.AreOutputsEquivalent(expected, result);
		}

		[TestMethod]
		public void Render_ForBlock_CaseInsensitivity()
		{
			var template = new Template(@"
				@{ FoR item In Collection }
					List element
				@{ eNd fOr }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("coLLectiON",
						new ArrayModelValue(
							new PrimitiveModelValue(1),
							new PrimitiveModelValue(2),
							new PrimitiveModelValue(3)))));

			var expected = @"				
					List element				
					List element				
					List element				
			";

			TemplateAssert.AreOutputsEquivalent(expected, result);
		}

		[TestMethod]
		public void Render_ForBlock_EmptyBlock()
		{
			var template = new Template(@"@{ for item in Collection }@{ end for }");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Collection",
						new ArrayModelValue(
							new PrimitiveModelValue(1),
							new PrimitiveModelValue(2),
							new PrimitiveModelValue(3)))));

			var expected = @"";

			Assert.AreEqual(expected, result);
		}



		[TestMethod]
		public void Render_ForBlock_IterationOverArrayWithMethods()
		{
			var template = new Template(@"
				@{ for item in Collection }
					${ item }
				@{ end for }

				@{ for item in Collection.Take(1) }
					${ item.Key }
				@{ end for }

				@{ for item in Collection.ConvertToIntegers() }
					${ item + 5 }
				@{ end for }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField(
						"Collection",
						new ArrayModelValue(
							methods: new List<IModelMethod>
							{
								new ModelMethod(
									"Take",
									new object[] { 1 },
									new ArrayModelValue(
										new CompositeModelValue(new ModelField("Key", "One")))),
								new ModelMethod(
									"ConvertToIntegers",
									Array.Empty<object>(),
									new ArrayModelValue(
										new PrimitiveModelValue(1),
										new PrimitiveModelValue(2)))
							},
							elements: new List<IModelValue>
							{
								new PrimitiveModelValue("One"),
								new PrimitiveModelValue("Two")
							}))));

			var expected = @"
				One
				Two
	
				One

				6
				7
			";

			TemplateAssert.AreOutputsEquivalent(expected, result);
		}
	}
}
