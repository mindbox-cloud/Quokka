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
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mindbox.Quokka.Tests
{
	[TestClass]
	public class DefaultTemplateFactoryFunctionsTests
	{
		[TestMethod]
		public void Functions_StandardFactory_ListsEveryStandardFunctionWithItsSignature()
		{
			var signatures = new DefaultTemplateFactory().Functions.Select(Describe).ToArray();

			CollectionAssert.AreEqual(
				new[]
				{
					"toUpper(string: String) -> String",
					"toLower(string: String) -> String",
					"replaceIfEmpty(default value: String, fallback value: String) -> String",
					"formatDecimal(number: Decimal, format: String) -> String",
					"formatMoney(amount: Decimal?, currencyCode: String?) -> String",
					"formatMoney(amount: Decimal?, currencyCode: String?, displayMode: String?) -> String",
					"formatDateTime(dateTime: DateTime, format: String) -> String",
					"formatTime(time: TimeSpan, format: String) -> String",
					"if(condition: Boolean, trueValue: String, falseValue: String) -> String",
					"isEmpty(value: Unknown?) -> Boolean",
					"tableRows(list: Array, rowSize: Integer) -> Array",
					"count(list: Array) -> Integer",
					"addDays(date: DateTime, daysAmount: Decimal) -> DateTime",
					"addHours(date: DateTime, hoursAmount: Decimal) -> DateTime",
					"chooseRandomText(inputString: String...1) -> String",
					"floor(value: Decimal) -> Decimal",
					"ceiling(value: Decimal) -> Decimal",
					"substring(value: String, startIndex: Integer) -> String",
					"substring(value: String, startIndex: Integer, length: Integer) -> String",
					"length(string: String) -> Integer",
					"md5(string: String) -> String",
					"sha1(string: String) -> String",
					"sha256(string: String) -> String",
					"sha512(string: String) -> String",
					"toBase64(string: String) -> String",
					"toHexadecimal(string: String) -> String",
					"getDay(dateTime: DateTime) -> Integer",
					"getMonth(dateTime: DateTime) -> Integer",
					"getYear(dateTime: DateTime) -> Integer",
					"appendForms(quantity: Decimal, singularForm: String, dualForm: String, pluralForm: String) -> String",
					"capitalizeAllWords(value: String) -> String",
					"capitalize(value: String) -> String",
					"forms(quantity: Decimal, singularForm: String, dualForm: String, pluralForm: String) -> String",
					"truncate(inputString: String, allowedLength: Decimal) -> String",
					"toUnixTimeStamp(dateTime: DateTime) -> Integer",
					"fail(reason: String) -> String",
					"contains(value: String?, search: String?) -> Boolean",
				},
				signatures,
				string.Join("\n", signatures));
		}

		[TestMethod]
		public void Functions_AdditionalFunctions_FollowStandardOnes()
		{
			var additional = new ToHexTemplateFunction();

			var functions = new DefaultTemplateFactory(new[] { additional }).Functions;

			Assert.AreEqual(38, functions.Count);
			Assert.AreSame(additional, functions[^1]);
		}

		private static string Describe(TemplateFunction function)
		{
			var parameters = function.Parameters.Select(parameter =>
				$"{parameter.Name}: {parameter.Type.Name}{(parameter.AllowsNull ? "?" : "")}").ToList();
			if (function.VariadicMinCount is { } minCount)
				parameters[^1] += $"...{minCount}";

			return $"{function.Name}({string.Join(", ", parameters)}) -> {ReturnTypeName(function.ReturnValueDefinition)}";
		}

		private static string ReturnTypeName(IModelDefinition definition) =>
			definition switch
			{
				IArrayModelDefinition => "Array",
				ICompositeModelDefinition => "Composite",
				IPrimitiveModelDefinition primitive => primitive.Type.Name,
				_ => "?"
			};
	}
}
