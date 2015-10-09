using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Quokka.Tests
{
	[TestClass]
	public class ApplyIfBlockArithmeticLogicTests
	{
		[TestMethod]
		public void Apply_IfArithmeticLogic_MoreThan_True()
		{
			var template = new Template(@"
				@{ if A > 5 }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeModelValue(
					new ModelField("A", 6)));

			var expected = @"
				
					Correct.
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfArithmeticLogic_MoreThan_FalseBeсauseLess()
		{
			var template = new Template(@"
				@{ if A > 5 }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeModelValue(
					new ModelField("A", 4)));

			var expected = @"
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfArithmeticLogic_MoreThan_FalseBeсauseEquals()
		{
			var template = new Template(@"
				@{ if A > 5 }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeModelValue(
					new ModelField("A", 5)));

			var expected = @"
				
			";

			Assert.AreEqual(expected, result);
		}
		
		[TestMethod]
		public void Apply_IfArithmeticLogic_LessThan_True()
		{
			var template = new Template(@"
				@{ if A < 23 }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeModelValue(
					new ModelField("A", 22)));

			var expected = @"
				
					Correct.
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfArithmeticLogic_LessThan_FalseBeсauseMore()
		{
			var template = new Template(@"
				@{ if A < 23 }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeModelValue(
					new ModelField("A", 24)));

			var expected = @"
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfArithmeticLogic_LessThan_FalseBeсauseEquals()
		{
			var template = new Template(@"
				@{ if A < 23 }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeModelValue(
					new ModelField("A", 23)));

			var expected = @"
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfArithmeticLogic_Equals_True()
		{
			var template = new Template(@"
				@{ if A = 2323 }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeModelValue(
					new ModelField("A", 2323)));

			var expected = @"
				
					Correct.
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfArithmeticLogic_Equals_FalseBeсauseMore()
		{
			var template = new Template(@"
				@{ if A = 2323 }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeModelValue(
					new ModelField("A", 2324)));

			var expected = @"
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfArithmeticLogic_Equals_FalseBeсauseLess()
		{
			var template = new Template(@"
				@{ if A = 2323 }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeModelValue(
					new ModelField("A", 2322)));

			var expected = @"
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfArithmeticLogic_Equals_True_Precision()
		{
			var template = new Template(@"
				@{ if (3 / 7) = (9000000 / 21000001) }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeModelValue());

			var expected = @"
				
					Correct.
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfArithmeticLogic_NotEquals_FalseBecauseEquals()
		{
			var template = new Template(@"
				@{ if A != 90 }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeModelValue(
					new ModelField("A", 90)));

			var expected = @"
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfArithmeticLogic_NotEquals_TrueBeсauseMore()
		{
			var template = new Template(@"
				@{ if A != 90 }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeModelValue(
					new ModelField("A", 2324)));

			var expected = @"
				
					Correct.
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfArithmeticLogic_NotEquals_TrueBeсauseLess()
		{
			var template = new Template(@"
				@{ if A != 90 }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeModelValue(
					new ModelField("A", 2322)));

			var expected = @"
				
					Correct.
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfArithmeticLogic_LessThanOrEquals_TrueBecauseLess()
		{
			var template = new Template(@"
				@{ if A <= 23 }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeModelValue(
					new ModelField("A", 22)));

			var expected = @"
				
					Correct.
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfArithmeticLogic_LessThanOrEquals_TrueBecauseEquals()
		{
            var template = new Template(@"
				@{ if A <= 23 }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeModelValue(
					new ModelField("A", 23)));

			var expected = @"
				
					Correct.
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfArithmeticLogic_LessThanOrEquals_False()
		{
			var template = new Template(@"
				@{ if A <= 23 }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeModelValue(
					new ModelField("A", 24)));

			var expected = @"
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfArithmeticLogic_MoreThanOrEquals_TrueBecauseMore()
		{
			var template = new Template(@"
				@{ if A >= 23 }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeModelValue(
					new ModelField("A", 24)));

			var expected = @"
				
					Correct.
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfArithmeticLogic_MoreThanOrEquals_TrueBecauseEquals()
		{
			var template = new Template(@"
				@{ if A >= 23 }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeModelValue(
					new ModelField("A", 23)));

			var expected = @"
				
					Correct.
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfArithmeticLogic_MoreThanOrEquals_False()
		{
			var template = new Template(@"
				@{ if A >= 23 }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeModelValue(
					new ModelField("A", 22)));

			var expected = @"
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfArithmeticLogic_ComplexExpression()
		{
			// A smoke test
			var template = new Template(@"
				@{ if (24 + 3) + (10 * 25)/5 - (24 * 7 / 8*9) * (242) + A.Value * B.Value.Length = 77 }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeModelValue(
					new ModelField("A",
						new CompositeModelValue(
							new ModelField("Value", 189))),
					new ModelField("B",
						new CompositeModelValue(
							new ModelField("Value",
								new CompositeModelValue(
									new ModelField("Length", 242)))))));

			var expected = @"
				
					Correct.
				
			";

			Assert.AreEqual(expected, result);
		}
	}
}
