using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Quokka.Tests
{
	[TestClass]
	public class ApplyIfBlockArithmeticLogicTests
	{
		[TestMethod]
		public void Apply_IfLogic_MoreThan_True()
		{
			var template = new Template(@"
				@{ if A > 5 }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("A", 6)));

			var expected = @"
				
					Correct.
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfLogic_MoreThan_FalseBeсauseLess()
		{
			var template = new Template(@"
				@{ if A > 5 }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("A", 4)));

			var expected = @"
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfLogic_MoreThan_FalseBeсauseEquals()
		{
			var template = new Template(@"
				@{ if A > 5 }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("A", 5)));

			var expected = @"
				
			";

			Assert.AreEqual(expected, result);
		}
		
		[TestMethod]
		public void Apply_IfLogic_LessThan_True()
		{
			var template = new Template(@"
				@{ if A < 23 }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("A", 22)));

			var expected = @"
				
					Correct.
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfLogic_LessThan_FalseBeсauseMore()
		{
			var template = new Template(@"
				@{ if A < 23 }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("A", 24)));

			var expected = @"
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfLogic_LessThan_FalseBeсauseEquals()
		{
			var template = new Template(@"
				@{ if A < 23 }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("A", 23)));

			var expected = @"
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfLogic_Equals_True()
		{
			var template = new Template(@"
				@{ if A = 2323 }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("A", 2323)));

			var expected = @"
				
					Correct.
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfLogic_Equals_FalseBeсauseMore()
		{
			var template = new Template(@"
				@{ if A = 2323 }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("A", 2324)));

			var expected = @"
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfLogic_Equals_FalseBeсauseLess()
		{
			var template = new Template(@"
				@{ if A = 2323 }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("A", 2322)));

			var expected = @"
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfLogic_Equals_True_Precision()
		{
			var template = new Template(@"
				@{ if (3 / 7) = (9000000 / 21000001) }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue());

			var expected = @"
				
					Correct.
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfLogic_NotEquals_FalseBecauseEquals()
		{
			var template = new Template(@"
				@{ if A != 90 }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("A", 90)));

			var expected = @"
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfLogic_NotEquals_TrueBeсauseMore()
		{
			var template = new Template(@"
				@{ if A != 90 }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("A", 2324)));

			var expected = @"
				
					Correct.
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfLogic_NotEquals_TrueBeсauseLess()
		{
			var template = new Template(@"
				@{ if A != 90 }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("A", 2322)));

			var expected = @"
				
					Correct.
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfLogic_LessThanOrEquals_TrueBecauseLess()
		{
			var template = new Template(@"
				@{ if A <= 23 }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("A", 22)));

			var expected = @"
				
					Correct.
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfLogic_LessThanOrEquals_TrueBecauseEquals()
		{
            var template = new Template(@"
				@{ if A <= 23 }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("A", 23)));

			var expected = @"
				
					Correct.
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfLogic_LessThanOrEquals_False()
		{
			var template = new Template(@"
				@{ if A <= 23 }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("A", 24)));

			var expected = @"
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfLogic_MoreThanOrEquals_TrueBecauseMore()
		{
			var template = new Template(@"
				@{ if A >= 23 }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("A", 24)));

			var expected = @"
				
					Correct.
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfLogic_MoreThanOrEquals_TrueBecauseEquals()
		{
			var template = new Template(@"
				@{ if A >= 23 }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("A", 23)));

			var expected = @"
				
					Correct.
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfLogic_MoreThanOrEquals_False()
		{
			var template = new Template(@"
				@{ if A >= 23 }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("A", 22)));

			var expected = @"
				
			";

			Assert.AreEqual(expected, result);
		}
	}
}
