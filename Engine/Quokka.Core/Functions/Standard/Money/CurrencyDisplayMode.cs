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
	internal enum CurrencyDisplayMode
	{
		NarrowSymbol,
		Symbol,
		Code
	}

	internal static class CurrencyDisplayModes
	{
		public const string NarrowSymbolName = "narrowSymbol";
		public const string SymbolName = "symbol";
		public const string CodeName = "code";

		public static bool TryParse(string value, out CurrencyDisplayMode mode)
		{
			var name = value?.Trim();
			if (string.IsNullOrEmpty(name) || Matches(name, NarrowSymbolName))
			{
				mode = CurrencyDisplayMode.NarrowSymbol;
				return true;
			}

			if (Matches(name, SymbolName))
			{
				mode = CurrencyDisplayMode.Symbol;
				return true;
			}

			if (Matches(name, CodeName))
			{
				mode = CurrencyDisplayMode.Code;
				return true;
			}

			mode = CurrencyDisplayMode.NarrowSymbol;
			return false;
		}

		public static CurrencyDisplayMode ParseOrDefault(string value) =>
			TryParse(value, out var mode) ? mode : CurrencyDisplayMode.NarrowSymbol;

		private static bool Matches(string value, string name) =>
			string.Equals(value, name, StringComparison.OrdinalIgnoreCase);
	}
}
