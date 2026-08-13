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
using System.Globalization;

namespace Mindbox.Quokka
{
	internal static class CurrencyNumberFormats
	{
		private const string Space = " ";

		private static readonly NumberFormatInfo commaGroupDotDecimal = Create(",", ".", [3]);
		private static readonly NumberFormatInfo dotGroupCommaDecimal = Create(".", ",", [3]);
		private static readonly NumberFormatInfo spaceGroupCommaDecimal = Create(Space, ",", [3]);
		private static readonly NumberFormatInfo apostropheGroupDotDecimal = Create("'", ".", [3]);
		private static readonly NumberFormatInfo indianGroupDotDecimal = Create(",", ".", [3, 2]);

		public static NumberFormatInfo Default => commaGroupDotDecimal;

		public static NumberFormatInfo Get(CurrencyNumberStyle style) =>
			style switch
			{
				CurrencyNumberStyle.CommaGroupDotDecimal => commaGroupDotDecimal,
				CurrencyNumberStyle.DotGroupCommaDecimal => dotGroupCommaDecimal,
				CurrencyNumberStyle.SpaceGroupCommaDecimal => spaceGroupCommaDecimal,
				CurrencyNumberStyle.ApostropheGroupDotDecimal => apostropheGroupDotDecimal,
				CurrencyNumberStyle.IndianGroupDotDecimal => indianGroupDotDecimal,
				_ => throw new ArgumentOutOfRangeException(nameof(style), style, null)
			};

		private static NumberFormatInfo Create(string groupSeparator, string decimalSeparator, int[] groupSizes) =>
			NumberFormatInfo.ReadOnly(
				new NumberFormatInfo
				{
					NumberGroupSeparator = groupSeparator,
					NumberDecimalSeparator = decimalSeparator,
					NumberGroupSizes = groupSizes
				});
	}
}
