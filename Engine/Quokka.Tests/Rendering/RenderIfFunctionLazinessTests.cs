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
using Mindbox.Quokka.Html;

namespace Mindbox.Quokka.Tests
{
	[TestClass]
	public class RenderIfFunctionLazinessTests
	{
		private const string OldPriceTemplate =
			"${ if(BasePrice != null and BasePrice > Price, formatDecimal(BasePrice * Count, '0'), 'none') }";

		[TestMethod]
		public void Render_If_ConditionIsFalse_TrueValueIsNotEvaluated()
		{
			var template = new Template(OldPriceTemplate);

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("BasePrice", new PrimitiveModelValue(null)),
					new ModelField("Price", 100m),
					new ModelField("Count", 2m)));

			Assert.AreEqual("none", result);
		}

		[TestMethod]
		public void Render_If_ConditionIsTrue_FalseValueIsNotEvaluated()
		{
			var template = new Template(
				"${ if(BasePrice = null, 'none', formatDecimal(BasePrice * Count, '0')) }");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("BasePrice", new PrimitiveModelValue(null)),
					new ModelField("Count", 2m)));

			Assert.AreEqual("none", result);
		}

		[TestMethod]
		public void Render_If_ConditionIsTrue_ReturnsTrueValue()
		{
			var template = new Template(OldPriceTemplate);

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("BasePrice", 150m),
					new ModelField("Price", 100m),
					new ModelField("Count", 2m)));

			Assert.AreEqual("300", result);
		}

		[TestMethod]
		public void Render_If_ChosenValueFails_ReportsTheSameErrorAsWithoutIf()
		{
			var model = new CompositeModelValue(
				new ModelField("BasePrice", new PrimitiveModelValue(null)),
				new ModelField("Count", 2m));

			var expected = CatchRenderError(
				new Template("${ formatDecimal(BasePrice * Count, '0') }"), model);
			var actual = CatchRenderError(
				new Template("${ if(Count > 0, formatDecimal(BasePrice * Count, '0'), '') }"), model);

			Assert.AreEqual(expected.GetType(), actual.GetType());
			Assert.AreEqual(expected.Data[QuokkaExceptionData.ErrorText], actual.Data[QuokkaExceptionData.ErrorText]);
			Assert.AreEqual(expected.Expression, actual.Expression);
			Assert.AreNotEqual(
				UnrenderableTemplateModelException.FunctionFailedErrorText,
				actual.Data[QuokkaExceptionData.ErrorText]);
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

		[TestMethod]
		public void Render_If_NestedIfs_OnlyChosenValuesAreEvaluated()
		{
			var template = new Template(
				"${ if(A != null and A > 0, formatDecimal(A * 2, '0'), "
					+ "if(B != null and B > 0, formatDecimal(B * 3, '0'), "
					+ "if(C != null, formatDecimal(C * 4, '0'), 'none'))) }");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("A", new PrimitiveModelValue(null)),
					new ModelField("B", new PrimitiveModelValue(null)),
					new ModelField("C", new PrimitiveModelValue(null))));

			Assert.AreEqual("none", result);
		}

		[TestMethod]
		public void Render_If_IfInConditionOfIf_OnlyChosenValuesAreEvaluated()
		{
			var template = new Template(
				"${ if(if(A != null, formatDecimal(A * 2, '0'), 'none') = 'none', 'empty', 'filled') }");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("A", new PrimitiveModelValue(null))));

			Assert.AreEqual("empty", result);
		}

		[TestMethod]
		public void Render_If_FunctionInValueNotChosen_IsNotInvoked()
		{
			var tracker = new TrackingFunction();
			var template = new DefaultTemplateFactory(new[] { tracker })
				.CreateTemplate("${ if(IsTest, track('test'), track('production')) }");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("IsTest", true)));

			Assert.AreEqual("test", result);
			CollectionAssert.AreEqual(new[] { "test" }, tracker.Invocations);
		}

		[TestMethod]
		public void Render_If_ConditionIsEvaluatedOnce()
		{
			var tracker = new TrackingFunction();
			var template = new DefaultTemplateFactory(new[] { tracker })
				.CreateTemplate("${ if(track('condition') = 'condition', 'yes', 'no') }");

			var result = template.Render(new CompositeModelValue());

			Assert.AreEqual("yes", result);
			CollectionAssert.AreEqual(new[] { "condition" }, tracker.Invocations);
		}

		[TestMethod]
		public void Render_OtherFunction_EvaluatesAllArguments()
		{
			var tracker = new TrackingFunction();
			var template = new DefaultTemplateFactory(new[] { tracker })
				.CreateTemplate("${ replaceIfEmpty(track('first'), track('second')) }");

			var result = template.Render(new CompositeModelValue());

			Assert.AreEqual("first", result);
			CollectionAssert.AreEqual(new[] { "first", "second" }, tracker.Invocations);
		}

		[TestMethod]
		public void Render_If_InHtmlAttributes_ValueNotChosenIsNotEvaluated()
		{
			var template = new HtmlTemplate(
				"<img alt=\"${ if(A != null and A > B, formatDecimal(A * B, '0'), '') }\">"
					+ "<a href=\"https://example.com/${ if(A != null and A > B, formatDecimal(A * B, '0'), '') }\">link</a>");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("A", new PrimitiveModelValue(null)),
					new ModelField("B", 2m)));

			Assert.AreEqual("<img alt=\"\"><a href=\"https://example.com/\">link</a>", result);
		}

		[TestMethod]
		[ExpectedException(typeof(TemplateContainsErrorsException))]
		public void CreateTemplate_If_TypeErrorInValueNotChosen_IsStillAnError()
		{
			new Template("${ if(IsTest, 'test', formatDecimal('not a number', '0')) }");
		}

		[TestMethod]
		public void GetModelDefinition_If_FieldsOfBothValuesAreRequired()
		{
			var model = new Template(OldPriceTemplate).GetModelDefinition();

			CollectionAssert.AreEquivalent(
				new[] { "BasePrice", "Price", "Count" },
				new System.Collections.Generic.List<string>(model.Fields.Keys));
		}

		private class TrackingFunction : ScalarTemplateFunction<string, string>
		{
			public System.Collections.Generic.List<string> Invocations { get; } = new();

			public TrackingFunction()
				: base("track", new StringFunctionArgument("value"))
			{
			}

			public override string Invoke(RenderSettings settings, string value)
			{
				Invocations.Add(value);
				return value;
			}
		}
	}
}
