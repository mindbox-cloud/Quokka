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

using Mindbox.Quokka.Html;

namespace Mindbox.Quokka.Tests
{
	[TestClass]
	public class RenderFailFunctionTests
	{
		[TestMethod]
		public void Render_Fail_InExecutedIfBranch_StopsRenderingWithTheReason()
		{
			var template = new Template(
				"@{ if Count < 3 }${ fail('RequiredElementsCountNotReached') }@{ end if }Products");

			var exception = CatchRenderError(template, new CompositeModelValue(new ModelField("Count", 1m)));

			Assert.AreEqual(
				UnrenderableTemplateModelException.FunctionFailedErrorText,
				exception.Data[QuokkaExceptionData.ErrorText]);
			Assert.AreEqual("fail('RequiredElementsCountNotReached')", exception.Expression);
			var failure = (TemplateFailedException)exception.InnerException;
			Assert.AreEqual("RequiredElementsCountNotReached", failure.Reason);
		}

		[TestMethod]
		public void Render_Fail_InSkippedIfBranch_RendersTheTemplate()
		{
			var template = new Template(
				"@{ if Count < 3 }${ fail('RequiredElementsCountNotReached') }@{ end if }Products");

			var result = template.Render(new CompositeModelValue(new ModelField("Count", 5m)));

			Assert.AreEqual("Products", result);
		}

		[TestMethod]
		public void Create_Fail_TemplateIsValidAndFunctionIsNotInvoked()
		{
			var template = new DefaultTemplateFactory().TryCreateTemplate("${ fail('X') }", out var errors);

			Assert.IsNotNull(template);
			Assert.AreEqual(0, errors.Count);
		}

		[TestMethod]
		public void Render_Fail_InHtmlTemplate_StopsRenderingWithTheReason()
		{
			var template = new HtmlTemplate("<p>@{ if Count < 3 }${ fail('X') }@{ end if }</p>");

			var exception = CatchRenderError(template, new CompositeModelValue(new ModelField("Count", 1m)));

			Assert.AreEqual("X", ((TemplateFailedException)exception.InnerException).Reason);
		}

		[TestMethod]
		public void Render_Fail_VariableArgument_PassesTheVariableValueAsReason()
		{
			var template = new Template("${ fail(Reason) }");

			var exception = CatchRenderError(template, new CompositeModelValue(new ModelField("Reason", "X")));

			Assert.AreEqual("X", ((TemplateFailedException)exception.InnerException).Reason);
		}

		[TestMethod]
		public void Render_Fail_NullArgument_ReportsArgumentErrorInsteadOfFailure()
		{
			var template = new Template("${ fail(Reason) }");

			var exception = CatchRenderError(
				template,
				new CompositeModelValue(new ModelField("Reason", new PrimitiveModelValue(null))));

			Assert.IsNotInstanceOfType(exception.InnerException, typeof(TemplateFailedException));
		}

		[TestMethod]
		public void TemplateFailedException_NullReason_ReasonIsEmpty()
		{
			var exception = new TemplateFailedException(null);

			Assert.AreEqual(string.Empty, exception.Reason);
		}

		[TestMethod]
		public void Render_Fail_ChosenByIfFunction_StopsRendering()
		{
			var template = new Template("${ if(Count < 3, fail('X'), '') }Products");

			var exception = CatchRenderError(template, new CompositeModelValue(new ModelField("Count", 1m)));

			Assert.AreEqual("X", ((TemplateFailedException)exception.InnerException).Reason);
		}

		[TestMethod]
		public void Render_Fail_NotChosenByIfFunction_RendersTheTemplate()
		{
			var template = new Template("${ if(Count < 3, fail('X'), '') }Products");

			var result = template.Render(new CompositeModelValue(new ModelField("Count", 5m)));

			Assert.AreEqual("Products", result);
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
	}
}