using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Quokka
{
	[TestClass]
	public class ApplyIfBlockBasicTests
	{
		[TestMethod]
		public void Apply_IfSimpleConditionIsTrue()
		{
			var template = new Template(@"
				@{ if IsTest }
					It's a test
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("IsTest", true)));

			var expected = @"
				
					It's a test
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfSimpleConditionIsFalse()
		{
			var template = new Template(@"
				@{ if IsTest }
					It's a test
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("IsTest", false)));

			var expected = @"
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfSimpleConditionWithElse()
		{
			var template = new Template(@"
				@{ if IsTest }
					It's a test
				@{ else }
					It's not a test
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("IsTest", true)));

			var expected = @"
				
					It's a test
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfSimpleConditionWithElseIsFalse()
		{
			var template = new Template(@"
				@{ if IsTest }
					It's a test
				@{ else }
					It's not a test
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("IsTest", false)));

			var expected = @"
				
					It's not a test
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_If_TwoBranches_BothTrue_Branch1()
		{
			var template = new Template(@"
				@{ if IsTest }
					It's a test
				@{ else if IsStaging }
					It's staging
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("IsTest", true),
					new ParameterField("IsStaging", true)));

			var expected = @"
				
					It's a test
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_If_TwoBranches_OnlyFirstTrue_Branch1()
		{
			var template = new Template(@"
				@{ if IsTest }
					It's a test
				@{ else if IsStaging }
					It's staging
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("IsTest", true),
					new ParameterField("IsStaging", false)));

			var expected = @"
				
					It's a test
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_If_TwoBranches_OnlySecondTrue_Branch2()
		{
			var template = new Template(@"
				@{ if IsTest }
					It's a test
				@{ else if IsStaging }
					It's staging
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("IsTest", false),
					new ParameterField("IsStaging", true)));

			var expected = @"
				
					It's staging
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_If_TwoBranches_BothFalse_Branch2()
		{
			var template = new Template(@"
				@{ if IsTest }
					It's a test
				@{ else if IsStaging }
					It's staging
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("IsTest", false),
					new ParameterField("IsStaging", false)));

			var expected = @"
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_If_NBranchesWithoutElse()
		{
			var template = new Template(@"
				@{ if IsRed }
					Red
				@{ else if IsGreen }
					Green
				@{ else if IsBlue }
					Blue
				@{ else if IsYellow }
					Yellow
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("IsRed", false),
					new ParameterField("IsGreen", false),
					new ParameterField("IsBlue", true),
					new ParameterField("IsYellow", false)));

			var expected = @"
				
					Blue
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_If_NBranchesWithElse_BranchIsTrue()
		{
			var template = new Template(@"
				@{ if IsRed }
					Red
				@{ else if IsGreen }
					Green
				@{ else }
					Unknown color
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("IsRed", false),
					new ParameterField("IsGreen", true)));

			var expected = @"
				
					Green
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_If_NBranchesWithElse_NoBranchIsTrue()
		{
			var template = new Template(@"
				@{ if IsRed }
					Red
				@{ else if IsGreen }
					Green
				@{ else }
					Unknown color
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("IsRed", false),
					new ParameterField("IsGreen", false)));

			var expected = @"
				
					Unknown color
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfSimpleCondition_Parentheses()
		{
			var template = new Template(@"
				@{ if (((IsTest))) }
					It's a test
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("IsTest", true)));

			var expected = @"
				
					It's a test
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_IfCondition_ThirLevelMember()
		{
			var template = new Template(@"
				@{ if Context.Values.IsTest }
					It's a test
				@{ else if Context.Values.IsStaging }
					It's staging
				@{ end if }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("Context",
						new CompositeParameterValue(
							new ParameterField("Values",
								new CompositeParameterValue(
									new ParameterField("IsTest", false),
									new ParameterField("IsStaging", true)))))));

			var expected = @"
				
					It's staging
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_If_InstructionsCaseInsensitivity()
		{
			var template = new Template(@"
				@{ If IsRed }
					Red
				@{ eLsE iF IsGreen }
					Green
				@{ ElSe }
					Unknown color
				@{ ENd IF }
			");

			var result = template.Apply(
				new CompositeParameterValue(
					new ParameterField("IsRed", false),
					new ParameterField("IsGreen", true)));

			var expected = @"
				
					Green
				
			";

			Assert.AreEqual(expected, result);
		}
	}
}
