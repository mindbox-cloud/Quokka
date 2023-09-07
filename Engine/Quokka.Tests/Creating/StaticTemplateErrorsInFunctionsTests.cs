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

using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Mindbox.Quokka.Abstractions;

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

			public override int Invoke(RenderSettings settings, int value)
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

			public override decimal Invoke(RenderSettings settings, decimal value)
			{
				return value;
			}
		}
	}
}
