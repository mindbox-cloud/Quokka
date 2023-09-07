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
using System.Linq;

using Mindbox.Quokka.Abstractions;

namespace Mindbox.Quokka
{
	internal class CapitalizeTemplateFunction : ScalarTemplateFunction<string, string>
	{
		public CapitalizeTemplateFunction()
			: base("capitalize", new StringFunctionArgument("value"))
		{
		}

		public override string Invoke(RenderSettings settings, string value)
		{
			if (value == null)
				throw new ArgumentNullException(nameof(value));

			var parts = value.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
				.Select((str, i) => i == 0 ? Capitalize(str) : str);

			return string.Join(" ", parts);
		}
		
		private static string Capitalize(string value)
		{
			if (value == null)
				throw new ArgumentNullException(nameof(value));

			if (value == string.Empty)
				return value;

			return value.Substring(0, 1).ToUpper() + value.Substring(1);
		}
	}
}
