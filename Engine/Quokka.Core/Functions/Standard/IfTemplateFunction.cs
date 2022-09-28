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
	internal class IfTemplateFunction : ScalarTemplateFunction<bool, string, string, string>
	{
		public IfTemplateFunction()
			: base(
				  "if",
				  new BoolFunctionArgument("condition"), 
				  new StringFunctionArgument("trueValue"),
				  new StringFunctionArgument("falseValue"))
		{
		}

		public override string Invoke(bool argument1, string argument2, string argument3)
		{
			return argument1 ? argument2 : argument3;
		}
	}
}
