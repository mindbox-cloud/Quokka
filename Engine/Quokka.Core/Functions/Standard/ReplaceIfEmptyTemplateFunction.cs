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

using Mindbox.Quokka.Abstractions;

namespace Mindbox.Quokka
{
	internal class ReplaceIfEmptyTemplateFunction : ScalarTemplateFunction<string, string, string>
	{
		public ReplaceIfEmptyTemplateFunction()
			: base(
				  "replaceIfEmpty",
				  new StringFunctionArgument("default value"),
				  new StringFunctionArgument("fallback value"))
		{
		}

		public override string Invoke(RenderSettings settings, string argument1, string argument2)
		{
			return !string.IsNullOrWhiteSpace(argument1) ? argument1 : argument2;
		}
	}
}
