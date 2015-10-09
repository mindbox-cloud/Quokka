using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Quokka.Tests
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
				new CompositeModelValue(
					new ModelField("IsTest", true)));

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
				new CompositeModelValue(
					new ModelField("IsTest", false)));

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
				new CompositeModelValue(
					new ModelField("IsTest", true)));

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
				new CompositeModelValue(
					new ModelField("IsTest", false)));

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
				new CompositeModelValue(
					new ModelField("IsTest", true),
					new ModelField("IsStaging", true)));

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
				new CompositeModelValue(
					new ModelField("IsTest", true),
					new ModelField("IsStaging", false)));

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
				new CompositeModelValue(
					new ModelField("IsTest", false),
					new ModelField("IsStaging", true)));

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
				new CompositeModelValue(
					new ModelField("IsTest", false),
					new ModelField("IsStaging", false)));

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
				new CompositeModelValue(
					new ModelField("IsRed", false),
					new ModelField("IsGreen", false),
					new ModelField("IsBlue", true),
					new ModelField("IsYellow", false)));

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
				new CompositeModelValue(
					new ModelField("IsRed", false),
					new ModelField("IsGreen", true)));

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
				new CompositeModelValue(
					new ModelField("IsRed", false),
					new ModelField("IsGreen", false)));

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
				new CompositeModelValue(
					new ModelField("IsTest", true)));

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
				new CompositeModelValue(
					new ModelField("Context",
						new CompositeModelValue(
							new ModelField("Values",
								new CompositeModelValue(
									new ModelField("IsTest", false),
									new ModelField("IsStaging", true)))))));

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
				new CompositeModelValue(
					new ModelField("IsRed", false),
					new ModelField("IsGreen", true)));

			var expected = @"
				
					Green
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Apply_If_EmptyBlocks()
		{
			var template = new Template(@"@{ if IsRed }@{ else if IsGreen }@{ else }@{ end if }");

			var result = template.Apply(
				new CompositeModelValue(
					new ModelField("IsRed", false),
					new ModelField("IsGreen", true)));

			var expected = @"";

			Assert.AreEqual(expected, result);
		}
	}
}
