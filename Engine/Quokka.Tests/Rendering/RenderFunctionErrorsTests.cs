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

using System;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Mindbox.Quokka.Abstractions;

namespace Mindbox.Quokka.Tests
{
	[TestClass]
	public class RenderFunctionErrorsTests
	{
		[TestMethod]
		public void Render_IsEmpty_ArgumentFails_ReportsTheArgumentError()
		{
			var exception = CatchRenderError(
				new Template("@{ if isEmpty(formatDecimal(1 / Zero, '0')) }empty@{ end if }"),
				new CompositeModelValue(new ModelField("Zero", 0m)));

			Assert.IsInstanceOfType(exception, typeof(ArithmeticOperationException));
			Assert.AreEqual("1 / Zero", exception.Expression);
		}

		[TestMethod]
		public void Render_TableRows_ArgumentFails_ReportsTheArgumentError()
		{
			var exception = CatchRenderError(
				CreateTemplate("@{ for row in tableRows(Collection, fail(2)) }row@{ end for }"),
				CreateCollectionModel());

			AssertFunctionFailed(exception, "fail(2)");
		}

		[TestMethod]
		public void Render_Count_ArgumentFails_ReportsTheArgumentError()
		{
			var exception = CatchRenderError(
				CreateTemplate("${ count(tableRows(Collection, fail(2))) }"),
				CreateCollectionModel());

			AssertFunctionFailed(exception, "fail(2)");
		}

		[TestMethod]
		public void Render_FunctionWithArgumentsFails_ReportsTheFunctionError()
		{
			var exception = CatchRenderError(
				CreateTemplate("${ failWithText(toUpper('a')) }"),
				new CompositeModelValue());

			AssertFunctionFailed(exception, "failWithText(toUpper('a'))");
			Assert.AreEqual(
				"Function invocation resulted in error in \"failWithText(toUpper('a'))\" at 1:3",
				exception.Message);
		}

		[TestMethod]
		public void Render_FunctionWithArgumentsReportsRuntimeError_ReportsTheFunctionMessage()
		{
			var exception = CatchRenderError(
				CreateTemplate("${ rejectText(toUpper('a')) }"),
				new CompositeModelValue());

			AssertFunctionFailed(exception, "rejectText(toUpper('a'))");
			Assert.AreEqual(
				"Function invocation resulted in error: Text is rejected in \"rejectText(toUpper('a'))\" at 1:3",
				exception.Message);
		}

		[TestMethod]
		public void Render_If_ChosenValueIsFailingFunction_ReportsTheErrorOfThatFunction()
		{
			var exception = CatchRenderError(
				CreateTemplate("${ if(Count > 0, failWithText('a'), '') }"),
				new CompositeModelValue(new ModelField("Count", 1m)));

			AssertFunctionFailed(exception, "failWithText('a')");
		}

		private static ITemplate CreateTemplate(string templateText)
		{
			return new DefaultTemplateFactory(
					new TemplateFunction[]
					{
						new FailingIntegerFunction(),
						new FailingTextFunction(),
						new RejectingTextFunction()
					})
				.CreateTemplate(templateText);
		}

		private static ICompositeModelValue CreateCollectionModel()
		{
			return new CompositeModelValue(
				new ModelField("Collection",
					new ArrayModelValue(
						Enumerable.Range(1, 3)
							.Select(x => new PrimitiveModelValue(x)))));
		}

		private static void AssertFunctionFailed(UnrenderableTemplateModelException exception, string expectedExpression)
		{
			Assert.AreEqual(typeof(UnrenderableTemplateModelException), exception.GetType());
			Assert.AreEqual(
				UnrenderableTemplateModelException.FunctionFailedErrorText,
				exception.Data[QuokkaExceptionData.ErrorText]);
			Assert.AreEqual(expectedExpression, exception.Expression);
		}

		private static UnrenderableTemplateModelException CatchRenderError(ITemplate template, ICompositeModelValue model)
		{
			try
			{
				template.Render(model);
			}
			catch (UnrenderableTemplateModelException exception)
			{
				return exception;
			}

			Assert.Fail("Rendering was expected to fail");
			return null;
		}

		private class FailingIntegerFunction : ScalarTemplateFunction<int, int>
		{
			public FailingIntegerFunction()
				: base("fail", new IntegerFunctionArgument("value"))
			{
			}

			public override int Invoke(RenderSettings settings, int value)
			{
				throw new InvalidOperationException("Error");
			}
		}

		private class FailingTextFunction : ScalarTemplateFunction<string, string>
		{
			public FailingTextFunction()
				: base("failWithText", new StringFunctionArgument("value"))
			{
			}

			public override string Invoke(RenderSettings settings, string value)
			{
				throw new InvalidOperationException("Error");
			}
		}

		private class RejectingTextFunction : ScalarTemplateFunction<string, string>
		{
			public RejectingTextFunction()
				: base("rejectText", new StringFunctionArgument("value"))
			{
			}

			public override string Invoke(RenderSettings settings, string value)
			{
				throw new FunctionCallRuntimeException("Text is rejected", null);
			}
		}
	}
}
