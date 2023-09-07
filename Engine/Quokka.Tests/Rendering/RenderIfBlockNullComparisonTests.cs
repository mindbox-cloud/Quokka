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

using Mindbox.Quokka.Abstractions;

namespace Mindbox.Quokka.Tests
{
	[TestClass]
	public class RenderIfBlockNullComparisonTests
	{
		[TestMethod]
		public void Render_IfNullComparison_CheckIfPrimitiveNull_Null()
		{
			var template = new Template(@"
				@{ if A = null }
					Correct.
				@{ end if }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("A", new PrimitiveModelValue(null))));

			var expected = @"
				
					Correct.
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_IfNullComparison_CheckIfPrimitiveNull_NotNull()
		{
			var template = new Template(@"
				@{ if A = null }
					Correct.
				@{ end if }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("A", new PrimitiveModelValue(5))));

			var expected = @"
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_IfNullComparison_CheckIfPrimitiveNotNull_NotNull()
		{
			var template = new Template(@"
				@{ if A != null }
					Correct.
				@{ end if }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("A", "Some value")));

			var expected = @"
				
					Correct.
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_IfNullComparison_CheckIfPrimitiveNotNull_Null()
		{
			var template = new Template(@"
				@{ if A != null }
					Correct.
				@{ end if }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("A", new PrimitiveModelValue(null))));

			var expected = @"
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_IfNullComparison_CheckIfNull_CaseInsensitivity()
		{
			var template = new Template(@"
				@{ if A = NuLL }
					Correct.
				@{ end if }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("A", new PrimitiveModelValue(null))));

			var expected = @"
				
					Correct.
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_IfNullComparison_CheckIfCompositeFieldNull_Null()
		{
			var template = new Template(@"
				@{ if A.Child.Property = null }
					Correct.
				@{ end if }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("A",
						new CompositeModelValue(
							new ModelField("Child",
								new CompositeModelValue(
									new ModelField("Property", new PrimitiveModelValue(null))))))));

			var expected = @"
				
					Correct.
				
			";

			Assert.AreEqual(expected, result);
		}
		
		[TestMethod]
		public void Render_IfNullComparison_CheckIfCompositeFieldNull_NotNull()
		{
			var template = new Template(@"
				@{ if A.Child.Property = null }
					Correct.
				@{ end if }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("A",
						new CompositeModelValue(
							new ModelField("Child",
								new CompositeModelValue(
									new ModelField("Property", "Andy")))))));

			var expected = @"
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_IfNullComparison_NullAccessInsideNullCheck()
		{
			var template = new Template(@"
				@{ if A.Property != null }
					${ A.Property }
				@{ end if }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("A",
						new CompositeModelValue(
							new ModelField("Property", new PrimitiveModelValue(null))))));

			var expected = @"
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_IfNullComparison_NonNullAccessInsideNullCheck()
		{
			var template = new Template(@"
				@{ if A.Property != null }
					${ A.Property }
				@{ end if }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("A",
						new CompositeModelValue(
							new ModelField("Property", "T-Shirt")))));

			var expected = @"
				
					T-Shirt
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_IfNullComparison_ParentScopeVariable()
		{
			var template = new Template(@"
				@{for purchase in Order.Purchases}
					@{ if (Order.DeliveryCost != null) }
						${ purchase.Product }
					@{ end if }
				@{ end for }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Order", new CompositeModelValue(
												new ModelField("DeliveryCost", 5190),
												new ModelField("Purchases", new ArrayModelValue(
													new CompositeModelValue(
														new ModelField("Product", "T-Shirt"))))
								))
					
				));
			var expected = @"				
				T-Shirt				
			";

			Assert.AreEqual(expected.Trim(), result.Trim());
		}

		[TestMethod]
		public void Render_IfNullComparison_CheckIfMethodValueNull_Null()
		{
			var template = new Template(@"
				@{ if A.Child.Method() != null }
					Not null
				@{ else if A.Child.Method() = null }
					Null
				@{ end if }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("A",
						new CompositeModelValue(
							new ModelField("Child",
								new CompositeModelValue(
									new ModelMethod("Method", new PrimitiveModelValue(null))))))));

			var expected = @"				
					Null				
			";

			TemplateAssert.AreOutputsEquivalent(expected, result);
		}

		[TestMethod]
		public void Render_IfNullComparison_CheckIfMethodValueNull_NotNull()
		{
			var template = new Template(@"
				@{ if A.Child.Method() != null }
					Not null
				@{ else if A.Child.Method() = null }
					Null
				@{ end if }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("A",
						new CompositeModelValue(
							new ModelField("Child",
								new CompositeModelValue(
									new ModelMethod("Method", "Andy")))))));

			var expected = @"
				Not null
			";

			TemplateAssert.AreOutputsEquivalent(expected, result);
		}

		[TestMethod]
		public void Render_IfNullComparison_CheckIfFunctionResultIsNotNull_Null()
		{
			var template = new DefaultTemplateFactory(new[] { new ReturnNullIfTrueFunction() })
				.CreateTemplate(@"
					@{ if ReturnNullIfTrue(A) != null }
						Not null.
					@{ else }
						Null.
					@{ end if }
				");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("a", new PrimitiveModelValue(false))));

			var expected = @"				
					Not null.				
			";

			TemplateAssert.AreOutputsEquivalent(expected, result);
		}

		[TestMethod]
		public void Render_IfNullComparison_CheckIfFunctionResultIsNotNull_NotNull()
		{
			var template = new DefaultTemplateFactory(new[] { new ReturnNullIfTrueFunction() })
				.CreateTemplate(@"
					@{ if ReturnNullIfTrue(A) != null }
						Not null.
					@{ else }
						Null.
					@{ end if }
				");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("a", new PrimitiveModelValue(true))));

			var expected = @"				
					Null.				
			";

			TemplateAssert.AreOutputsEquivalent(expected, result);
		}

		private class ReturnNullIfTrueFunction : ScalarTemplateFunction<bool, string>
		{
			public ReturnNullIfTrueFunction()
				: base(
					"ReturnNullIfTrue",
					new BoolFunctionArgument("flag"))
			{
			}

			public override string Invoke(RenderSettings settings, bool value)
			{
				return value ? null : "";
			}
		}
	}
}
