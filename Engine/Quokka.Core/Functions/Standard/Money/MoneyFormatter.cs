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

namespace Mindbox.Quokka
{
	internal static class MoneyFormatter
	{
		public const string NarrowSymbolDisplayMode = "narrowSymbol";
		public const string SymbolDisplayMode = "symbol";
		public const string CodeDisplayMode = "code";

		private const string MinusSign = "-";
		private const string Space = " ";

		public static bool IsSupportedDisplayMode(string displayMode) =>
			displayMode == null || IsMode(displayMode, NarrowSymbolDisplayMode)
				|| IsMode(displayMode, SymbolDisplayMode)
				|| IsMode(displayMode, CodeDisplayMode);

		public static string Format(decimal amount, string currencyCode, string displayMode)
		{
			var code = currencyCode?.Trim();
			var format = CurrencyFormats.TryGet(code);
			var decimalPlaces = format?.DecimalPlaces ?? CurrencyAmountFormats.DecimalPlacesForUnlistedCode(code);
			var absolute = Math.Round(Math.Abs(amount), decimalPlaces, MidpointRounding.AwayFromZero);

			var formattedAmount = absolute.ToString(
				format?.AmountFormat ?? CurrencyAmountFormats.ForDecimalPlaces(decimalPlaces),
				format?.NumberFormat ?? CurrencyNumberFormats.Default);

			var sign = amount < 0 && absolute != decimal.Zero ? MinusSign : string.Empty;

			if (format == null)
				return string.IsNullOrEmpty(code)
					? sign + formattedAmount
					: sign + formattedAmount + Space + code.ToUpperInvariant();

			if (IsMode(displayMode, CodeDisplayMode))
				return sign + formattedAmount + Space + code.ToUpperInvariant();

			var symbol = IsMode(displayMode, SymbolDisplayMode) ? format.Symbol : format.NarrowSymbol;

			if (format.SymbolFollowsAmount)
				return sign + formattedAmount + Space + symbol;

			var needsSpace = format.SpaceAfterSymbol || char.IsLetter(symbol[symbol.Length - 1]);

			return needsSpace
				? sign + symbol + Space + formattedAmount
				: sign + symbol + formattedAmount;
		}

		private static bool IsMode(string displayMode, string mode) =>
			string.Equals(displayMode?.Trim(), mode, StringComparison.OrdinalIgnoreCase);
	}
}
