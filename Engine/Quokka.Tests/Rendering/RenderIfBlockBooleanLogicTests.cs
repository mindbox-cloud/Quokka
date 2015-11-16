using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Quokka.Tests
{
	[TestClass]
	public class RenderIfBlockBooleanLogicTests
	{
		[TestMethod]
		public void Render_IfLogic_TrueAndTrue()
		{
			var template = new Template(@"
				@{ if A and B }
					Correct.
				@{ end if }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("A", true),
					new ModelField("B", true)));

			var expected = @"
				
					Correct.
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_IfLogic_TrueAndFalse()
		{
			var template = new Template(@"
				@{ if A and B }
					Correct.
				@{ end if }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("A", true),
					new ModelField("B", false)));

			var expected = @"
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_IfLogic_FalseAndTrue()
		{
			var template = new Template(@"
				@{ if A and B }
					Correct.
				@{ end if }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("A", false),
					new ModelField("B", true)));

			var expected = @"
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_IfLogic_FalseAndFalse()
		{
			var template = new Template(@"
				@{ if A and B }
					Correct.
				@{ end if }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("A", false),
					new ModelField("B", false)));

			var expected = @"
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_IfLogic_TrueAndTrueAndTrue()
		{
			var template = new Template(@"
				@{ if A and B and C }
					Correct.
				@{ end if }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("A", true),
					new ModelField("B", true),
					new ModelField("C", true)));

			var expected = @"
				
					Correct.
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_IfLogic_TrueAndTrueAndTrueAndFalse()
		{
			var template = new Template(@"
				@{ if A and B and C and D }
					Correct.
				@{ end if }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("A", true),
					new ModelField("B", true),
					new ModelField("C", true),
					new ModelField("D", false)));

			var expected = @"
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_IfLogic_TrueOrTrue()
		{
			var template = new Template(@"
				@{ if A or B }
					Correct.
				@{ end if }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("A", true),
					new ModelField("B", true)));

			var expected = @"
				
					Correct.
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_IfLogic_TrueOrFalse()
		{
			var template = new Template(@"
				@{ if A or B }
					Correct.
				@{ end if }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("A", true),
					new ModelField("B", false)));

			var expected = @"
				
					Correct.
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_IfLogic_FalseOrTrue()
		{
			var template = new Template(@"
				@{ if A or B }
					Correct.
				@{ end if }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("A", false),
					new ModelField("B", true)));

			var expected = @"
				
					Correct.
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_IfLogic_FalseOrFalse()
		{
			var template = new Template(@"
				@{ if A or B }
					Correct.
				@{ end if }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("A", false),
					new ModelField("B", false)));

			var expected = @"
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_IfLogic_FalseOrFalseOrTrue()
		{
			var template = new Template(@"
				@{ if A or B or C }
					Correct.
				@{ end if }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("A", false),
					new ModelField("B", false),
					new ModelField("C", true)));

			var expected = @"
				
					Correct.
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_IfLogic_NotTrue()
		{
			var template = new Template(@"
				@{ if not A }
					Correct.
				@{ end if }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("A", true)));

			var expected = @"
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_IfLogic_NotFalse()
		{
			var template = new Template(@"
				@{ if not A }
					Correct.
				@{ end if }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("A", false)));

			var expected = @"
				
					Correct.
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_IfLogic_FalseOrTrueAndFalse_Precedence()
		{
			var template = new Template(@"
				@{ if A or B and C }
					Correct.
				@{ end if }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("A", false),
					new ModelField("B", true),
					new ModelField("C", false)));

			var expected = @"
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_IfLogic_FalseAndTrueOrFalse_Precedence()
		{
			var template = new Template(@"
				@{ if A and B or C }
					Correct.
				@{ end if }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("A", false),
					new ModelField("B", true),
					new ModelField("C", false)));

			var expected = @"
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_IfLogic_FalseOrNotTrue_Precedence()
		{
			var template = new Template(@"
				@{ if A or not B }
					Correct.
				@{ end if }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("A", false),
					new ModelField("B", true)));

			var expected = @"
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_IfLogic_FalseOrNotFalse_Precedence()
		{
			var template = new Template(@"
				@{ if A or not B }
					Correct.
				@{ end if }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("A", false),
					new ModelField("B", false)));

			var expected = @"
				
					Correct.
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_IfLogic_TrueAndNotTrue_Precedence()
		{
			var template = new Template(@"
				@{ if A and not B }
					Correct.
				@{ end if }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("A", true),
					new ModelField("B", true)));

			var expected = @"
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_IfLogic_NotFalseAndTrue_Precedence()
		{
			var template = new Template(@"
				@{ if not A and B }
					Correct.
				@{ end if }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("A", false),
					new ModelField("B", true)));

			var expected = @"
				
					Correct.
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_IfLogic_TrueAnd_FalseOrTrue_Parentheses()
		{
			var template = new Template(@"
				@{ if A and (B or C) }
					Correct.
				@{ end if }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("A", true),
					new ModelField("B", false),
					new ModelField("C", true)));

			var expected = @"
				
					Correct.
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_IfLogic_Not_TrueOrFalse_Parentheses()
		{
			var template = new Template(@"
				@{ if not(A or B) }
					Correct.
				@{ end if }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("A", true),
					new ModelField("B", false)));

			var expected = @"
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_IfLogic_ComplexExpression()
		{
			var template = new Template(@"
				@{ if Recipient.IsMale or A or B and C or (D or E) or not(F) and (G or (H and K)) and (((bingo))) }
					Trust me, it's true.
				@{ end if }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Recipient",
						new CompositeModelValue(
							new ModelField("IsMale", false))),
					new ModelField("A", false),
					new ModelField("B", false),
					new ModelField("C", false),
					new ModelField("D", false),
					new ModelField("E", false),
					new ModelField("F", false),
					new ModelField("G", true),
					new ModelField("H", true),
					new ModelField("K", false),
					new ModelField("bingo", true)));

			var expected = @"
				
					Trust me, it's true.
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_If_BooleanFunction_IfCondition_Tests()
		{
			var template = new Template(@"
				@{ if isEmpty(value) }
					Empty.
				@{ end if }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("value", "")));

			var expected = @"
				
					Empty.
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_If_BooleanFunction_ElseCondition_Tests()
		{
			var template = new Template(@"
				@{ if isEmpty(value) }
					Empty.
				@{ else }
					Not empty.
				@{ end if }
			");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("value", "miu")));

			var expected = @"
				
					Not empty.
				
			";

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Render_If_NonBooleanFunction()
		{
			var template = new Template(@"
				@{ if toUpper(value) }
					Empty.
				@{ end if }
			", new FunctionRegistry(Template.GetStandardFunctions()), false);

			Assert.AreEqual(1, template.Errors.Count);
			Assert.AreEqual("Недопустимый тип результата функции toUpper. Ожидался System.Boolean, а она возвращает System.String", 
				((SemanticError)template.Errors[0]).Message);
		}
	}
}
