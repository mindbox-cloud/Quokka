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

using System.Globalization;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Mindbox.Quokka.Abstractions;

namespace Mindbox.Quokka.Tests
{
	[TestClass]
	public class FormatMoneyTemplateFunctionTests
	{
		[TestMethod]
		[DataRow("USD", "$1,234,567.89")]
		[DataRow("GBP", "£1,234,567.89")]
		[DataRow("SGD", "$1,234,567.89")]
		[DataRow("RUB", "1 234 567,89 ₽")]
		[DataRow("PLN", "1 234 567,89 zł")]
		[DataRow("EUR", "1.234.567,89 €")]
		[DataRow("BRL", "R$1.234.567,89")]
		[DataRow("CHF", "CHF 1'234'567.89")]
		[DataRow("INR", "₹12,34,567.89")]
		public void FormatMoney_GroupsDigitsTheWayTheCurrencyDoes(string currencyCode, string expected)
		{
			Assert.AreEqual(expected, RenderMoney("1234567.89", currencyCode));
		}

		[TestMethod]
		[DataRow("JPY", "¥9,072")]
		[DataRow("KRW", "₩9,072")]
		[DataRow("VND", "9.072 ₫")]
		[DataRow("HUF", "9 072 Ft")]
		[DataRow("IDR", "Rp 9.072")]
		public void FormatMoney_WithZeroDecimalCurrency_DropsDecimals(string currencyCode, string expected)
		{
			Assert.AreEqual(expected, RenderMoney("9072.00", currencyCode));
		}

		[TestMethod]
		public void FormatMoney_WithThreeDecimalCurrency_KeepsThreeDecimals()
		{
			Assert.AreEqual("KWD 1,234.560", RenderMoney("1234.56", "KWD"));
		}

		[TestMethod]
		[DataRow("USD", "-$1,234.56")]
		[DataRow("RUB", "-1 234,56 ₽")]
		[DataRow("KWD", "-KWD 1,234.560")]
		public void FormatMoney_WithNegativeAmount_PutsTheSignFirst(string currencyCode, string expected)
		{
			Assert.AreEqual(expected, RenderMoney("-1234.56", currencyCode));
		}

		[TestMethod]
		[DataRow("USD", "US$1,234.56")]
		[DataRow("CAD", "CA$1,234.56")]
		[DataRow("AUD", "A$1,234.56")]
		[DataRow("RUB", "1 234,56 RUB")]
		[DataRow("THB", "THB 1,234.56")]
		public void FormatMoney_WithSymbolDisplayMode_DisambiguatesSharedSymbols(string currencyCode, string expected)
		{
			Assert.AreEqual(expected, RenderMoney("1234.56", currencyCode, "symbol"));
		}

		[TestMethod]
		[DataRow("EUR", "1.234,56 €")]
		[DataRow("GBP", "£1,234.56")]
		public void FormatMoney_WithSymbolDisplayMode_KeepsUnambiguousSymbols(string currencyCode, string expected)
		{
			Assert.AreEqual(expected, RenderMoney("1234.56", currencyCode, "symbol"));
		}

		[TestMethod]
		[DataRow("USD", "1,234.56 USD")]
		[DataRow("usd", "1,234.56 USD")]
		[DataRow("xyz", "1,234.56 XYZ")]
		public void FormatMoney_WithCodeDisplayMode_AppendsUppercasedIsoCode(string currencyCode, string expected)
		{
			Assert.AreEqual(expected, RenderMoney("1234.56", currencyCode, "code"));
		}

		[TestMethod]
		[DataRow("jpy")]
		[DataRow(" JPY ")]
		public void FormatMoney_WithLowercaseOrPaddedCode_ResolvesTheSameFormat(string currencyCode)
		{
			Assert.AreEqual("¥9,072", RenderMoney("9072.00", currencyCode));
		}

		[TestMethod]
		public void FormatMoney_WithUnknownCurrencyCode_FallsBackToAmountAndCode()
		{
			Assert.AreEqual("1,234.56 XYZ", RenderMoney("1234.56", "XYZ"));
		}

		[TestMethod]
		public void FormatMoney_WithoutCurrencyCode_RendersBareAmount()
		{
			var template = new Template("${ formatMoney(Amount, Currency) }");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Amount", 1234.56m),
					new ModelField("Currency", (string)null)));

			Assert.AreEqual("1,234.56", result);
		}

		[TestMethod]
		[DataRow("1.005", "$1.01")]
		[DataRow("2.005", "$2.01")]
		[DataRow("-1.005", "-$1.01")]
		public void FormatMoney_RoundsHalfAwayFromZero(string amount, string expected)
		{
			Assert.AreEqual(expected, RenderMoney(amount, "USD"));
		}

		[TestMethod]
		[DataRow("en-US")]
		[DataRow("ru-RU")]
		[DataRow("de-DE")]
		public void FormatMoney_IsIndependentOfRenderCulture(string locale)
		{
			var template = new Template("${ formatMoney(Amount, 'USD') }${ formatMoney(Amount, 'EUR') }");

			var result = template.Render(
				new CompositeModelValue(new ModelField("Amount", 1234.56m)),
				new RenderSettings { CultureInfo = new CultureInfo(locale) });

			Assert.AreEqual("$1,234.56" + "1.234,56 €", result);
		}

		[TestMethod]
		public void FormatMoney_SeparatesWithPlainSpaces_SoSmsStaysInTheGsmAlphabet()
		{
			var result = RenderMoney("1234.56", "RUB");

			Assert.AreEqual("1 234,56 ₽", result);
			Assert.IsFalse(result.Contains('\u00A0'));
		}

		[TestMethod]
		public void FormatMoney_WithDisplayModeFromModel_FallsBackToNarrowSymbolWhenUnsupported()
		{
			var template = new Template("${ formatMoney(Amount, 'USD', DisplayMode) }");

			var result = template.Render(
				new CompositeModelValue(
					new ModelField("Amount", 1234.56m),
					new ModelField("DisplayMode", "fancy")));

			Assert.AreEqual("$1,234.56", result);
		}

		[TestMethod]
		[ExpectedException(typeof(TemplateContainsErrorsException))]
		public void FormatMoney_WithUnsupportedConstantDisplayMode_ReportsStaticError()
		{
			new Template("${ formatMoney(Amount, 'USD', 'fancy') }");
		}

		private static string RenderMoney(string amount, string currencyCode, string displayMode = null)
		{
			var displayModeArgument = displayMode == null ? string.Empty : $", '{displayMode}'";
			var template = new Template($"${{ formatMoney(Amount, '{currencyCode}'{displayModeArgument}) }}");

			return template.Render(
				new CompositeModelValue(
					new ModelField("Amount", decimal.Parse(amount, CultureInfo.InvariantCulture))));
		}
	}
}
