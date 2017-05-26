using Microsoft.VisualStudio.TestTools.UnitTesting;

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
	}
}
