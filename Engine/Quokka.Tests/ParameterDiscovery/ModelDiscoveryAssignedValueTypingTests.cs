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
	public class ModelDiscoveryAssignedValueTypingTests
	{
		private const string AmountResetToZeroTemplate =
			"@{ set amount = Order.TotalAmount }@{ if amount < 0 }@{ set amount = 0 }@{ end if }${ amount * 2 }";

		[TestMethod]
		public void WideningDisabled_IntegerAssignedToVariableBoundToField_FieldIsInteger()
		{
			var model = CreateModel(AmountResetToZeroTemplate, widenAssignedValueTypes: false);

			AssertOrderFieldType(model, "TotalAmount", TypeDefinition.Integer);
		}

		[TestMethod]
		public void WideningEnabled_IntegerLiteralAssignedToVariableBoundToField_FieldIsDecimal()
		{
			var model = CreateModel(AmountResetToZeroTemplate, widenAssignedValueTypes: true);

			AssertOrderFieldType(model, "TotalAmount", TypeDefinition.Decimal);
		}

		[DataTestMethod]
		[DataRow("@{ set amount = Order.TotalAmount }@{ set amount = 0 }${ amount }")]
		[DataRow("@{ set amount = Order.TotalAmount }@{ set amount = 0 }@{ set amount = 0.5 }${ amount }")]
		[DataRow("@{ set amount = Order.TotalAmount }@{ set amount = -1 }${ amount * 2 }")]
		[DataRow("@{ set amount = Order.TotalAmount }@{ set amount = 1 + 1 }${ amount * 2 }")]
		[DataRow("@{ set amount = Order.TotalAmount }@{ set amount = count(Order.Lines) }${ amount * 2 }")]
		[DataRow("@{ set amount = Order.TotalAmount }@{ set amount = length(\"abc\") }${ amount * 2 }")]
		[DataRow("@{ set source = Order.TotalAmount }@{ set amount = source }@{ set amount = 0 }${ amount * 2 }")]
		[DataRow("@{ set amount = Order.TotalAmount }@{ set other = 0 }@{ set other = amount }${ other * 2 }")]
		public void WideningEnabled_IntegerValueAssignedToVariableBoundToField_FieldIsDecimal(string templateText)
		{
			var model = CreateModel(templateText, widenAssignedValueTypes: true);

			AssertOrderFieldType(model, "TotalAmount", TypeDefinition.Decimal);
		}

		[DataTestMethod]
		[DataRow("@{ set amount = Order.TotalAmount }${ substring(\"abcdef\", amount) }")]
		[DataRow("@{ set amount = Order.TotalAmount }@{ set amount = 1.5 }${ substring(\"abcdef\", amount) }")]
		[DataRow("@{ set source = Order.TotalAmount }@{ set amount = source }${ substring(\"abcdef\", amount) }")]
		public void WideningEnabled_VariableBoundToFieldReadAsInteger_FieldIsInteger(string templateText)
		{
			var model = CreateModel(templateText, widenAssignedValueTypes: true);

			AssertOrderFieldType(model, "TotalAmount", TypeDefinition.Integer);
		}

		[TestMethod]
		public void WideningEnabled_StringAssignedToVariableBoundToField_FieldIsString()
		{
			var model = CreateModel(
				"@{ set amount = Order.TotalAmount }@{ set amount = \"abc\" }${ amount }",
				widenAssignedValueTypes: true);

			AssertOrderFieldType(model, "TotalAmount", TypeDefinition.String);
		}

		[TestMethod]
		public void WideningEnabled_VariableOnlyOutput_FieldIsPrimitive()
		{
			var model = CreateModel("@{ set amount = Order.TotalAmount }${ amount }", widenAssignedValueTypes: true);

			AssertOrderFieldType(model, "TotalAmount", TypeDefinition.Primitive);
		}

		[DataTestMethod]
		[DataRow("@{ set amount = \"abc\" }@{ set amount = 0 }${ amount }")]
		[DataRow("@{ set amount = 0 }${ substring(\"abcdef\", amount) }@{ set amount = \"abc\" }")]
		[DataRow("@{ set amount = \"abc\" }${ amount + 1 }")]
		public void WideningEnabled_ConflictingVariableUsages_TemplateHasErrors(string templateText)
		{
			var template = new DefaultTemplateFactory()
				.TryCreateTemplate(templateText, widenAssignedValueTypes: true, out var errors);

			Assert.IsNull(template);
			Assert.AreNotEqual(0, errors.Count);
		}

		[TestMethod]
		public void Factory_OverloadWithoutWideningFlag_KeepsPreviousTyping()
		{
			var model = new DefaultTemplateFactory().TryCreateTemplate(AmountResetToZeroTemplate, out _).GetModelDefinition();

			AssertOrderFieldType(model, "TotalAmount", TypeDefinition.Integer);
		}

		[TestMethod]
		public void Factory_ThrowingOverloadWithWideningEnabled_FieldIsDecimal()
		{
			var model = new DefaultTemplateFactory()
				.CreateTemplate(AmountResetToZeroTemplate, widenAssignedValueTypes: true)
				.GetModelDefinition();

			AssertOrderFieldType(model, "TotalAmount", TypeDefinition.Decimal);
		}

		[TestMethod]
		public void HtmlTemplate_WideningEnabled_IntegerLiteralAssignedToVariableBoundToField_FieldIsDecimal()
		{
			var template = new DefaultTemplateFactory()
				.TryCreateHtmlTemplate(AmountResetToZeroTemplate, widenAssignedValueTypes: true, out var errors);

			Assert.AreEqual(0, errors.Count);
			AssertOrderFieldType(template.GetModelDefinition(), "TotalAmount", TypeDefinition.Decimal);
		}

		private static ICompositeModelDefinition CreateModel(string templateText, bool widenAssignedValueTypes)
		{
			var template = new DefaultTemplateFactory()
				.TryCreateTemplate(templateText, widenAssignedValueTypes, out var errors);

			Assert.AreEqual(0, errors.Count, string.Join("; ", errors));
			return template.GetModelDefinition();
		}

		private static void AssertOrderFieldType(ICompositeModelDefinition model, string fieldName, TypeDefinition expectedType)
		{
			var order = (ICompositeModelDefinition)model.Fields["Order"];
			var field = (IPrimitiveModelDefinition)order.Fields[fieldName];

			Assert.AreEqual(expectedType, field.Type);
		}
	}
}
