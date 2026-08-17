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
using System.Collections.Concurrent;
using NodaMoney;

namespace Mindbox.Quokka
{
	internal static class CurrencyFormats
	{
		private static readonly ConcurrentDictionary<string, CurrencyFormat> knownFormats =
			new ConcurrentDictionary<string, CurrencyFormat>(StringComparer.OrdinalIgnoreCase);

		public static CurrencyFormat TryGet(string currencyCode)
		{
			if (string.IsNullOrEmpty(currencyCode))
				return null;

			if (knownFormats.TryGetValue(currencyCode, out var knownFormat))
				return knownFormat;

			if (!CurrencyInfo.TryFromCode(currencyCode, out var currency))
				return null;

			var format = new CurrencyFormat(currency.Symbol, currency.InternationalSymbol, currency.DecimalDigits);
			knownFormats[currencyCode] = format;
			return format;
		}
	}
}
