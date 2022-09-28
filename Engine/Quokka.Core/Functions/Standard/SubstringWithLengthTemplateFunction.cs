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

namespace Mindbox.Quokka
{
	public class SubstringWithLengthTemplateFunction : ScalarTemplateFunction<string, int, int, string>
	{
		public SubstringWithLengthTemplateFunction()
			: base("substring", 
				new StringFunctionArgument("value"), 
				new IntegerFunctionArgument("startIndex",
					valueValidator: value => value >= 1
						? ArgumentValueValidationResult.Valid
						: new ArgumentValueValidationResult(false, "Substring start index can't be less than 1")),
				new IntegerFunctionArgument("length",
					valueValidator: value => value >= 1
						? ArgumentValueValidationResult.Valid
						: new ArgumentValueValidationResult(false, "Substring length can't be less than 1")))
		{
		}

		public override string Invoke(string value, int startIndex, int length)
		{
			// in templates indexing from 1 is assumed
			return value.Substring(startIndex - 1, length);
		}
	}
}