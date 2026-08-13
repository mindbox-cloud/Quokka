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

namespace Mindbox.Quokka
{
	internal static class CurrencyAmountFormats
	{
		public const int DefaultDecimalPlaces = 2;

		private static readonly string[] formatsByDecimalPlaces = ["N0", "N1", "N2", "N3", "N4"];

		private static readonly IReadOnlyDictionary<string, int> decimalPlacesByCodeOutsideTheTable =
			new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
			{
				["AFN"] = 0,
				["ALL"] = 0,
				["BIF"] = 0,
				["CLF"] = 4,
				["DJF"] = 0,
				["GNF"] = 0,
				["IQD"] = 0,
				["IRR"] = 0,
				["KMF"] = 0,
				["LAK"] = 0,
				["LYD"] = 3,
				["MGA"] = 0,
				["MMK"] = 0,
				["PKR"] = 0,
				["PYG"] = 0,
				["RWF"] = 0,
				["SOS"] = 0,
				["SYP"] = 0,
				["TND"] = 3,
				["UGX"] = 0,
				["VUV"] = 0,
				["XAF"] = 0,
				["XOF"] = 0,
				["XPF"] = 0,
				["YER"] = 0
			};

		public static string ForDecimalPlaces(int decimalPlaces) => formatsByDecimalPlaces[decimalPlaces];

		public static int DecimalPlacesForUnlistedCode(string currencyCode) =>
			currencyCode != null && decimalPlacesByCodeOutsideTheTable.TryGetValue(currencyCode, out var decimalPlaces)
				? decimalPlaces
				: DefaultDecimalPlaces;
	}
}
