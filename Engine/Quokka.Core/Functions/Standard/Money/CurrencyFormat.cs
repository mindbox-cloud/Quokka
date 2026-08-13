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

namespace Mindbox.Quokka
{
	internal sealed class CurrencyFormat
	{
		public CurrencyFormat(
			string narrowSymbol,
			string symbol,
			int decimalPlaces,
			CurrencyNumberStyle numberStyle = CurrencyNumberStyle.CommaGroupDotDecimal,
			bool symbolFollowsAmount = false,
			bool spaceAfterSymbol = false)
		{
			NarrowSymbol = narrowSymbol;
			Symbol = symbol;
			DecimalPlaces = decimalPlaces;
			SymbolFollowsAmount = symbolFollowsAmount;
			SpaceAfterSymbol = spaceAfterSymbol;
			AmountFormat = CurrencyAmountFormats.ForDecimalPlaces(decimalPlaces);
			NumberFormat = CurrencyNumberFormats.Get(numberStyle);
		}

		public string NarrowSymbol { get; }

		public string Symbol { get; }

		public int DecimalPlaces { get; }

		public bool SymbolFollowsAmount { get; }

		public bool SpaceAfterSymbol { get; }

		public string AmountFormat { get; }

		public NumberFormatInfo NumberFormat { get; }
	}
}
