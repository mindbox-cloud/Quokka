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
using System.Linq;
using System.Text;

namespace Mindbox.Quokka
{

	public static class ByteUtility
	{

		public static string ToHexString(byte value)
		{
			return value.ToString("X2", CultureInfo.InvariantCulture);
		}

		public static string ToHexString(IEnumerable<byte> values)
		{
			if (values == null)
				throw new ArgumentNullException(nameof(values));

			var result = new StringBuilder();
			foreach (var value in values)
				result.Append(ToHexString(value));

			return result.ToString();
		}

		public static byte[] HexStringToByteArray(string hex)
		{
			return Enumerable.Range(0, hex.Length)
							.Where(x => x % 2 == 0)
							.Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
							.ToArray();
		}
	}

}