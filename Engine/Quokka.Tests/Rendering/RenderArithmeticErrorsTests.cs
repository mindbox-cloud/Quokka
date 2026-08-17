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

using Mindbox.Quokka.Abstractions;

namespace Mindbox.Quokka.Tests
{
	[TestClass]
	public class RenderArithmeticErrorsTests
	{
		[TestMethod]
		public void Render_DivisionByZero_ReportsReasonExpressionAndLocation()
		{
			var template = new Template("Average check: ${ Total / OrderCount }");

			var exception = Assert.ThrowsException<ArithmeticOperationException>(
				() => template.Render(
					new CompositeModelValue(
						new ModelField("Total", 500),
						new ModelField("OrderCount", 0))));

			Assert.AreEqual(ArithmeticErrorReason.DivisionByZero, exception.Reason);
			Assert.AreEqual("Total / OrderCount", exception.Expression);
			Assert.AreEqual(1, exception.Location.Line);
			Assert.AreEqual(18, exception.Location.Column);
			Assert.AreEqual(
				"Arithmetic operation result could not be evaluated: division by zero "
					+ "in \"Total / OrderCount\" at 1:18",
				exception.Message);
		}

		[TestMethod]
		public void Render_ZeroDividedByZero_ReportsNotANumber()
		{
			var template = new Template("${ Total / OrderCount }");

			var exception = Assert.ThrowsException<ArithmeticOperationException>(
				() => template.Render(
					new CompositeModelValue(
						new ModelField("Total", 0),
						new ModelField("OrderCount", 0))));

			Assert.AreEqual(ArithmeticErrorReason.NotANumber, exception.Reason);
			Assert.AreEqual(
				"Arithmetic operation result could not be evaluated: the result is not a number "
					+ "in \"Total / OrderCount\" at 1:3",
				exception.Message);
		}

		[TestMethod]
		public void Render_ResultTooLargeToBeRepresented_ReportsResultOutOfRange()
		{
			var template = new Template("${ First * Second }");

			var exception = Assert.ThrowsException<ArithmeticOperationException>(
				() => template.Render(
					new CompositeModelValue(
						new ModelField("First", 1000000000000000m),
						new ModelField("Second", 1000000000000000m))));

			Assert.AreEqual(ArithmeticErrorReason.ResultOutOfRange, exception.Reason);
			Assert.AreEqual(
				"Arithmetic operation result could not be evaluated: the result is out of the supported number range "
					+ "in \"First * Second\" at 1:3",
				exception.Message);
		}

		[TestMethod]
		public void Render_DivisionByZero_ReportsLocationWithinMultilineTemplate()
		{
			var template = new Template(
				"Hello!\r\n"
					+ "Your average check is ${ Total / OrderCount }.\r\n"
					+ "Bye!");

			var exception = Assert.ThrowsException<ArithmeticOperationException>(
				() => template.Render(
					new CompositeModelValue(
						new ModelField("Total", 500),
						new ModelField("OrderCount", 0))));

			Assert.AreEqual(2, exception.Location.Line);
			Assert.AreEqual(25, exception.Location.Column);
		}

		[TestMethod]
		public void Render_DivisionByZeroWithinLargerExpression_ReportsWholeExpression()
		{
			var template = new Template("${ Total / OrderCount + 1 }");

			var exception = Assert.ThrowsException<ArithmeticOperationException>(
				() => template.Render(
					new CompositeModelValue(
						new ModelField("Total", 500),
						new ModelField("OrderCount", 0))));

			Assert.AreEqual("Total / OrderCount + 1", exception.Expression);
		}

		[TestMethod]
		public void Render_DivisionByZero_FillsExceptionDataWithMessageParts()
		{
			var template = new Template("${ Total / OrderCount }");

			var exception = Assert.ThrowsException<ArithmeticOperationException>(
				() => template.Render(
					new CompositeModelValue(
						new ModelField("Total", 500),
						new ModelField("OrderCount", 0))));

			Assert.AreEqual(
				"Arithmetic operation result could not be evaluated",
				exception.Data[QuokkaExceptionData.ErrorText]);
			Assert.AreEqual("DivisionByZero", exception.Data[QuokkaExceptionData.Reason]);
			Assert.AreEqual("Total / OrderCount", exception.Data[QuokkaExceptionData.Expression]);
			Assert.AreEqual("1:3", exception.Data[QuokkaExceptionData.Location]);
			Assert.AreEqual(1, exception.Data[QuokkaExceptionData.Line]);
			Assert.AreEqual(3, exception.Data[QuokkaExceptionData.Column]);
		}

		[TestMethod]
		public void Render_DivisionByZero_IsStillCaughtAsUnrenderableTemplateModelException()
		{
			var template = new Template("${ Total / OrderCount }");

			var exception = Assert.ThrowsException<ArithmeticOperationException>(
				() => template.Render(
					new CompositeModelValue(
						new ModelField("Total", 500),
						new ModelField("OrderCount", 0))));

			Assert.IsInstanceOfType(exception, typeof(UnrenderableTemplateModelException));
		}

		[TestMethod]
		public void Render_NullValueError_FillsExceptionDataWithLocation()
		{
			var template = new Template("${ Total }");

			var exception = Assert.ThrowsException<UnrenderableTemplateModelException>(
				() => template.Render(
					new CompositeModelValue(
						new ModelField("Total", new PrimitiveModelValue(null)))));

			Assert.AreEqual("1:3", exception.Data[QuokkaExceptionData.Location]);
			Assert.AreEqual(1, exception.Data[QuokkaExceptionData.Line]);
			Assert.AreEqual(3, exception.Data[QuokkaExceptionData.Column]);
		}

		[TestMethod]
		public void Render_DivisionByZeroInFunctionArgument_ReportsArithmeticError()
		{
			var template = new DefaultTemplateFactory(new[] { new EchoFunction() })
				.CreateTemplate("${ echo(Total / OrderCount) }");

			var exception = Assert.ThrowsException<ArithmeticOperationException>(
				() => template.Render(
					new CompositeModelValue(
						new ModelField("Total", 500),
						new ModelField("OrderCount", 0))));

			Assert.AreEqual(ArithmeticErrorReason.DivisionByZero, exception.Reason);
			Assert.AreEqual("Total / OrderCount", exception.Expression);
		}

		[TestMethod]
		public void Render_FailedExpressionLongerThanLimit_TruncatesReportedExpression()
		{
			var template = new Template(
				"${ TheFirstVeryLongVariableName + TheSecondVeryLongVariableName "
					+ "+ TheThirdVeryLongVariableName + TheFourthVeryLongVariableName / DivisorWhichHappensToBeZero }");

			var exception = Assert.ThrowsException<ArithmeticOperationException>(
				() => template.Render(
					new CompositeModelValue(
						new ModelField("TheFirstVeryLongVariableName", 1),
						new ModelField("TheSecondVeryLongVariableName", 2),
						new ModelField("TheThirdVeryLongVariableName", 3),
						new ModelField("TheFourthVeryLongVariableName", 4),
						new ModelField("DivisorWhichHappensToBeZero", 0))));

			Assert.AreEqual(101, exception.Expression.Length);
			Assert.IsTrue(exception.Expression.EndsWith("…"));
		}

		private class EchoFunction : ScalarTemplateFunction<decimal, decimal>
		{
			public EchoFunction()
				: base("echo", new DecimalFunctionArgument("number"))
			{
			}

			public override decimal Invoke(RenderSettings settings, decimal value)
			{
				return value;
			}
		}
	}
}
