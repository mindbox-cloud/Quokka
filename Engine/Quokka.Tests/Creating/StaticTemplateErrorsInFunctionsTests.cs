using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mindbox.Quokka.Tests
{
	[TestClass]
	public class StaticTemplateErrorTests
	{
		[TestMethod]
		[ExpectedException(typeof(TemplateContainsErrorsException))]
		public void CreateTemplate_InvalidFunctionArgumentCount_TooMany_Error()
		{
			new Template(@"${ toLower(""valid argument"", ""excessive argument"") }");
		}

		[TestMethod]
		[ExpectedException(typeof(TemplateContainsErrorsException))]
		public void CreateTemplate_InvalidFunctionArgumentCount_TooLittle_Error()
		{
			new Template(@"${ toLower() }");
		}
		
		[TestMethod]
		[ExpectedException(typeof(TemplateContainsErrorsException))]
		public void CreateTemplate_InvalidFunctionTypeInForLoop_Error()
		{
			new Template(@"
				@{ for a in toLower(""string"") }
				
				@{ end for }
			");
		}

		[TestMethod]
		[ExpectedException(typeof(TemplateContainsErrorsException))]
		public void CreateTemplate_UnknownFunctionName_Error()
		{
			new Template(@"
				@{ if nonExistingFunction(""value"") }
					Something
				@{ end if }
			");
		}

		[TestMethod]
		[ExpectedException(typeof(TemplateContainsErrorsException))]
		public void CreateTemplate_InvalidStaticArgumentType_Error()
		{
			new Template("${ formatDecimal(5, 0) }");
		}

		[TestMethod]
		public void CreateTemplate_PassingDecimalAsIntArgument_Error()
		{
			new DefaultTemplateFactory(new[] { new IntegerIdentityFunction() })
				.TryCreateTemplate(
					"${ IntegerIdentity(42.5) }",
					out IList<ITemplateError> errors);

			Assert.AreNotEqual(0, errors.Count);
		}

		[TestMethod]
		public void CreateTemplate_PassingDecimalFunctionAsIntParameter_Error()
		{
			new DefaultTemplateFactory(new TemplateFunction[]
				{
					new IntegerIdentityFunction(),
					new DecimalIdentityFunction()
				})
				.TryCreateTemplate(
					"${ IntegerIdentity(DecimalIdentity(3)) }",
					out IList<ITemplateError> errors);

			Assert.AreNotEqual(1, errors.Count);
		}

		[TestMethod]
		public void CreateTemplate_PassingIntFunctionAsIntParameter_NoError()
		{
			new DefaultTemplateFactory(new TemplateFunction[]
				{
					new IntegerIdentityFunction(),
					new DecimalIdentityFunction()
				})
				.TryCreateTemplate(
					"${ IntegerIdentity(IntegerIdentity(6)) }",
					out IList<ITemplateError> errors);

			Assert.AreEqual(0, errors.Count);
		}

		[TestMethod]
		public void CreateTemplate_PassingVariableWithIntValueToIntParameter_NoError()
		{
			new DefaultTemplateFactory(new TemplateFunction[]
				{
					new IntegerIdentityFunction(),
				})
				.TryCreateTemplate(
					@"@{ set someValue = 6 }
					${ IntegerIdentity(someValue) }",
					out IList<ITemplateError> errors);

			Assert.AreEqual(0, errors.Count);
		}

		[TestMethod]
		public void CreateTemplate_UsingIntResultFunctionInArithmeticExpression_NoError()
		{
			new DefaultTemplateFactory(new TemplateFunction[]
				{
					new IntegerIdentityFunction(),
					new DecimalIdentityFunction()
				})
				.TryCreateTemplate(
					"${ IntegerIdentity(5 + IntegerIdentity(6)) }",
					out IList<ITemplateError> errors);

			Assert.AreEqual(0, errors.Count);
		}

		private class IntegerIdentityFunction : ScalarTemplateFunction<int, int>
		{
			public IntegerIdentityFunction()
				: base(
					"IntegerIdentity",
					new IntegerFunctionArgument("intValue"))
			{
			}

			public override int Invoke(int value)
			{
				return value;
			}
		}

		private class DecimalIdentityFunction : ScalarTemplateFunction<decimal, decimal>
		{
			public DecimalIdentityFunction()
				: base(
					"DecimalIdentity",
					new DecimalFunctionArgument("decimalValue"))
			{
			}

			public override decimal Invoke(decimal value)
			{
				return value;
			}
		}
	}
}
