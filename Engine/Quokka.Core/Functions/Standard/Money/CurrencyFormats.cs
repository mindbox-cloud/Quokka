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
	internal static class CurrencyFormats
	{
		private const CurrencyNumberStyle DotGroup = CurrencyNumberStyle.DotGroupCommaDecimal;
		private const CurrencyNumberStyle SpaceGroup = CurrencyNumberStyle.SpaceGroupCommaDecimal;
		private const CurrencyNumberStyle Apostrophe = CurrencyNumberStyle.ApostropheGroupDotDecimal;
		private const CurrencyNumberStyle Indian = CurrencyNumberStyle.IndianGroupDotDecimal;

		private static readonly IReadOnlyDictionary<string, CurrencyFormat> formatsByCode =
			new Dictionary<string, CurrencyFormat>(StringComparer.OrdinalIgnoreCase)
			{
				["AED"] = new("AED", "AED", 2),
				["ARS"] = new("$", "ARS", 2, DotGroup),
				["AUD"] = new("$", "A$", 2),
				["BHD"] = new("BHD", "BHD", 3),
				["BRL"] = new("R$", "R$", 2, DotGroup),
				["CAD"] = new("$", "CA$", 2),
				["CHF"] = new("CHF", "CHF", 2, Apostrophe),
				["CLP"] = new("$", "CLP", 0, DotGroup),
				["CNY"] = new("¥", "CN¥", 2),
				["COP"] = new("$", "COP", 0, DotGroup),
				["CZK"] = new("Kč", "CZK", 2, SpaceGroup, symbolFollowsAmount: true),
				["DKK"] = new("kr", "DKK", 2, DotGroup, symbolFollowsAmount: true),
				["EUR"] = new("€", "€", 2, DotGroup, symbolFollowsAmount: true),
				["GBP"] = new("£", "£", 2),
				["HKD"] = new("$", "HK$", 2),
				["HUF"] = new("Ft", "HUF", 0, SpaceGroup, symbolFollowsAmount: true),
				["IDR"] = new("Rp", "IDR", 0, DotGroup),
				["ILS"] = new("₪", "₪", 2),
				["INR"] = new("₹", "₹", 2, Indian),
				["ISK"] = new("kr", "ISK", 0, DotGroup, symbolFollowsAmount: true),
				["JOD"] = new("JOD", "JOD", 3),
				["JPY"] = new("¥", "JP¥", 0),
				["KRW"] = new("₩", "₩", 0),
				["KWD"] = new("KWD", "KWD", 3),
				["MXN"] = new("$", "MX$", 2),
				["MYR"] = new("RM", "MYR", 2),
				["NOK"] = new("kr", "NOK", 2, SpaceGroup, symbolFollowsAmount: true),
				["NZD"] = new("$", "NZ$", 2),
				["OMR"] = new("OMR", "OMR", 3),
				["PHP"] = new("₱", "₱", 2),
				["PLN"] = new("zł", "PLN", 2, SpaceGroup, symbolFollowsAmount: true),
				["RUB"] = new("₽", "RUB", 2, SpaceGroup, symbolFollowsAmount: true),
				["SAR"] = new("SAR", "SAR", 2),
				["SEK"] = new("kr", "SEK", 2, SpaceGroup, symbolFollowsAmount: true),
				["SGD"] = new("$", "S$", 2),
				["THB"] = new("฿", "THB", 2),
				["TRY"] = new("₺", "TRY", 2, DotGroup),
				["TWD"] = new("$", "NT$", 2),
				["UAH"] = new("₴", "UAH", 2, SpaceGroup, symbolFollowsAmount: true),
				["USD"] = new("$", "US$", 2),
				["VND"] = new("₫", "₫", 0, DotGroup, symbolFollowsAmount: true),
				["ZAR"] = new("R", "ZAR", 2, SpaceGroup)
			};

		public static CurrencyFormat TryGet(string currencyCode) =>
			currencyCode != null && formatsByCode.TryGetValue(currencyCode, out var format)
				? format
				: null;
	}
}
