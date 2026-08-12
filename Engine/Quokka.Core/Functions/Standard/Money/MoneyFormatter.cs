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
using System.Collections.Generic;
using System.Globalization;

namespace Mindbox.Quokka
{
	internal static class MoneyFormatter
	{
		public const string NarrowSymbolDisplayMode = "narrowSymbol";
		public const string SymbolDisplayMode = "symbol";
		public const string CodeDisplayMode = "code";

		private const string MinusSign = "-";
		private const string Space = " ";

		private static readonly IReadOnlyDictionary<CurrencyNumberStyle, NumberFormatInfo> numberFormatsByStyle =
			new Dictionary<CurrencyNumberStyle, NumberFormatInfo>
			{
				[CurrencyNumberStyle.CommaGroupDotDecimal] = CreateNumberFormat(",", ".", [3]),
				[CurrencyNumberStyle.DotGroupCommaDecimal] = CreateNumberFormat(".", ",", [3]),
				[CurrencyNumberStyle.SpaceGroupCommaDecimal] = CreateNumberFormat(Space, ",", [3]),
				[CurrencyNumberStyle.ApostropheGroupDotDecimal] = CreateNumberFormat("'", ".", [3]),
				[CurrencyNumberStyle.IndianGroupDotDecimal] = CreateNumberFormat(",", ".", [3, 2])
			};

		public static bool IsSupportedDisplayMode(string displayMode) =>
			displayMode is NarrowSymbolDisplayMode or SymbolDisplayMode or CodeDisplayMode;

		public static string Format(decimal amount, string currencyCode, string displayMode)
		{
			var trimmedCode = currencyCode?.Trim();
			var format = CurrencyFormats.TryGet(trimmedCode);
			var numberFormat = numberFormatsByStyle[format?.NumberStyle ?? CurrencyNumberStyle.CommaGroupDotDecimal];
			var formattedAmount = Math.Abs(amount).ToString("N" + (format?.DecimalPlaces ?? 2), numberFormat);
			var sign = amount < 0 ? MinusSign : string.Empty;

			if (format == null)
				return string.IsNullOrEmpty(trimmedCode)
					? sign + formattedAmount
					: sign + formattedAmount + Space + trimmedCode.ToUpperInvariant();

			if (displayMode == CodeDisplayMode)
				return sign + formattedAmount + Space + trimmedCode.ToUpperInvariant();

			var symbol = displayMode == SymbolDisplayMode ? format.Symbol : format.NarrowSymbol;

			if (format.SymbolFollowsAmount)
				return sign + formattedAmount + Space + symbol;

			var symbolSeparator = char.IsLetter(symbol[symbol.Length - 1]) ? Space : string.Empty;

			return sign + symbol + symbolSeparator + formattedAmount;
		}

		private static NumberFormatInfo CreateNumberFormat(
			string groupSeparator,
			string decimalSeparator,
			int[] groupSizes) =>
			new()
			{
				NumberGroupSeparator = groupSeparator,
				NumberDecimalSeparator = decimalSeparator,
				NumberGroupSizes = groupSizes
			};
	}
}
