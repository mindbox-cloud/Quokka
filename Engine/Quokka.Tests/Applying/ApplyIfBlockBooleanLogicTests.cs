using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Quokka.Tests
{
	[TestClass]
	public class ApplyIfBlockBooleanLogicTests
	{
		[TestMethod]
		public void Apply_IfLogic_TrueAndTrue()
		{
			var template = new Template(@"
				@{ if A and B }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("A", true),
					new ParameterField("B", true)));

			var expected = @"
				
					Correct.
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfLogic_TrueAndFalse()
		{
			var template = new Template(@"
				@{ if A and B }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("A", true),
					new ParameterField("B", false)));

			var expected = @"
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfLogic_FalseAndTrue()
		{
			var template = new Template(@"
				@{ if A and B }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("A", false),
					new ParameterField("B", true)));

			var expected = @"
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfLogic_FalseAndFalse()
		{
			var template = new Template(@"
				@{ if A and B }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("A", false),
					new ParameterField("B", false)));

			var expected = @"
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfLogic_TrueAndTrueAndTrue()
		{
			var template = new Template(@"
				@{ if A and B and C }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("A", true),
					new ParameterField("B", true),
					new ParameterField("C", true)));

			var expected = @"
				
					Correct.
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfLogic_TrueAndTrueAndTrueAndFalse()
		{
			var template = new Template(@"
				@{ if A and B and C and D }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("A", true),
					new ParameterField("B", true),
					new ParameterField("C", true),
					new ParameterField("D", false)));

			var expected = @"
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfLogic_TrueOrTrue()
		{
			var template = new Template(@"
				@{ if A or B }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("A", true),
					new ParameterField("B", true)));

			var expected = @"
				
					Correct.
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfLogic_TrueOrFalse()
		{
			var template = new Template(@"
				@{ if A or B }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("A", true),
					new ParameterField("B", false)));

			var expected = @"
				
					Correct.
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfLogic_FalseOrTrue()
		{
			var template = new Template(@"
				@{ if A or B }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("A", false),
					new ParameterField("B", true)));

			var expected = @"
				
					Correct.
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfLogic_FalseOrFalse()
		{
			var template = new Template(@"
				@{ if A or B }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("A", false),
					new ParameterField("B", false)));

			var expected = @"
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfLogic_FalseOrFalseOrTrue()
		{
			var template = new Template(@"
				@{ if A or B or C }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("A", false),
					new ParameterField("B", false),
					new ParameterField("C", true)));

			var expected = @"
				
					Correct.
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfLogic_NotTrue()
		{
			var template = new Template(@"
				@{ if not A }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("A", true)));

			var expected = @"
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfLogic_NotFalse()
		{
			var template = new Template(@"
				@{ if not A }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("A", false)));

			var expected = @"
				
					Correct.
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfLogic_FalseOrTrueAndFalse_Precedence()
		{
			var template = new Template(@"
				@{ if A or B and C }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("A", false),
					new ParameterField("B", true),
					new ParameterField("C", false)));

			var expected = @"
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfLogic_FalseAndTrueOrFalse_Precedence()
		{
			var template = new Template(@"
				@{ if A and B or C }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("A", false),
					new ParameterField("B", true),
					new ParameterField("C", false)));

			var expected = @"
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfLogic_FalseOrNotTrue_Precedence()
		{
			var template = new Template(@"
				@{ if A or not B }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("A", false),
					new ParameterField("B", true)));

			var expected = @"
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfLogic_FalseOrNotFalse_Precedence()
		{
			var template = new Template(@"
				@{ if A or not B }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("A", false),
					new ParameterField("B", false)));

			var expected = @"
				
					Correct.
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfLogic_TrueAndNotTrue_Precedence()
		{
			var template = new Template(@"
				@{ if A and not B }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("A", true),
					new ParameterField("B", true)));

			var expected = @"
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfLogic_NotFalseAndTrue_Precedence()
		{
			var template = new Template(@"
				@{ if not A and B }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("A", false),
					new ParameterField("B", true)));

			var expected = @"
				
					Correct.
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfLogic_TrueAnd_FalseOrTrue_Parentheses()
		{
			var template = new Template(@"
				@{ if A and (B or C) }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("A", true),
					new ParameterField("B", false),
					new ParameterField("C", true)));

			var expected = @"
				
					Correct.
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfLogic_Not_TrueOrFalse_Parentheses()
		{
			var template = new Template(@"
				@{ if not(A or B) }
					Correct.
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("A", true),
					new ParameterField("B", false)));

			var expected = @"
				
			";

			Assert.AreEqual(expected, result);
		}
	}
}
