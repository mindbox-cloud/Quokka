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
		private const string MinusSign = "-";
		private const string Space = " ";

		public static string Format(decimal amount, string currencyCode, CurrencyDisplayMode displayMode)
		{
			var code = currencyCode?.Trim();
			var format = CurrencyFormats.TryGet(code);
			var decimalPlaces = format?.DecimalPlaces ?? CurrencyAmountFormats.DefaultDecimalPlaces;
			var absolute = Math.Round(Math.Abs(amount), decimalPlaces, MidpointRounding.AwayFromZero);

			var formattedAmount = absolute.ToString(
				format?.AmountFormat ?? CurrencyAmountFormats.ForDecimalPlaces(decimalPlaces),
				CurrencyAmountFormats.NumberFormat);

			var sign = amount < 0 && absolute != decimal.Zero ? MinusSign : string.Empty;

			if (format == null)
				return string.IsNullOrEmpty(code)
					? sign + formattedAmount
					: sign + formattedAmount + Space + code.ToUpperInvariant();

			if (displayMode == CurrencyDisplayMode.Code)
				return sign + code.ToUpperInvariant() + Space + formattedAmount;

			var symbol = displayMode == CurrencyDisplayMode.Symbol ? format.Symbol : format.NarrowSymbol;
			var lastCharacter = symbol[symbol.Length - 1];
			var separator = format.SpaceAfterSymbol || char.IsLetter(lastCharacter) || lastCharacter == '.'
				? Space
				: string.Empty;

			return sign + symbol + separator + formattedAmount;
		}
	}
}
