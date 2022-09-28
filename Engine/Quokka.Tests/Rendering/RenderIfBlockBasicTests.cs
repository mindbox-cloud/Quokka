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

namespace Mindbox.Quokka.Tests
{
	[TestClass]
	public class RenderIfBlockBasicTests
	{
		[TestMethod]
		public void Render_IfSimpleConditionIsTrue()
		{
			var template = new Template(@"
				@{ if IsTest }
					It's a test
				@{ end if }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("IsTest", true)));

			var expected = @"
				
					It's a test
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_IfSimpleConditionIsFalse()
		{
			var template = new Template(@"
				@{ if IsTest }
					It's a test
				@{ end if }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("IsTest", false)));

			var expected = @"
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_IfSimpleConditionWithElse()
		{
			var template = new Template(@"
				@{ if IsTest }
					It's a test
				@{ else }
					It's not a test
				@{ end if }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("IsTest", true)));

			var expected = @"
				
					It's a test
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_IfSimpleConditionWithElseIsFalse()
		{
			var template = new Template(@"
				@{ if IsTest }
					It's a test
				@{ else }
					It's not a test
				@{ end if }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("IsTest", false)));

			var expected = @"
				
					It's not a test
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_If_TwoBranches_BothTrue_Branch1()
		{
			var template = new Template(@"
				@{ if IsTest }
					It's a test
				@{ else if IsStaging }
					It's staging
				@{ end if }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("IsTest", true),
					new ModelField("IsStaging", true)));

			var expected = @"
				
					It's a test
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_If_TwoBranches_OnlyFirstTrue_Branch1()
		{
			var template = new Template(@"
				@{ if IsTest }
					It's a test
				@{ else if IsStaging }
					It's staging
				@{ end if }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("IsTest", true),
					new ModelField("IsStaging", false)));

			var expected = @"
				
					It's a test
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_If_TwoBranches_OnlySecondTrue_Branch2()
		{
			var template = new Template(@"
				@{ if IsTest }
					It's a test
				@{ else if IsStaging }
					It's staging
				@{ end if }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("IsTest", false),
					new ModelField("IsStaging", true)));

			var expected = @"
				
					It's staging
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_If_TwoBranches_BothFalse_Branch2()
		{
			var template = new Template(@"
				@{ if IsTest }
					It's a test
				@{ else if IsStaging }
					It's staging
				@{ end if }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("IsTest", false),
					new ModelField("IsStaging", false)));

			var expected = @"
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_If_NBranchesWithoutElse()
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

			var result = template.Render(
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
		public void Render_If_NBranchesWithElse_BranchIsTrue()
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

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("IsRed", false),
					new ModelField("IsGreen", true)));

			var expected = @"
				
					Green
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_If_NBranchesWithElse_NoBranchIsTrue()
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

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("IsRed", false),
					new ModelField("IsGreen", false)));

			var expected = @"
				
					Unknown color
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_IfSimpleCondition_Parentheses()
		{
			var template = new Template(@"
				@{ if (((IsTest))) }
					It's a test
				@{ end if }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("IsTest", true)));

			var expected = @"
				
					It's a test
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_IfCondition_ThirLevelMember()
		{
			var template = new Template(@"
				@{ if Context.Values.IsTest }
					It's a test
				@{ else if Context.Values.IsStaging }
					It's staging
				@{ end if }
			");

			var result = template.Render(
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
		public void Render_If_InstructionsCaseInsensitivity()
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

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("IsRed", false),
					new ModelField("IsGreen", true)));

			var expected = @"
				
					Green
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_If_EmptyBlocks()
		{
			var template = new Template(@"@{ if IsRed }@{ else if IsGreen }@{ else }@{ end if }");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("IsRed", false),
					new ModelField("IsGreen", true)));

			var expected = @"";

			Assert.AreEqual(expected, result);
		}
	}
}
