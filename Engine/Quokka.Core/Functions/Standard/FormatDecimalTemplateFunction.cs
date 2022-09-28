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
	internal class FormatDecimalTemplateFunction : ScalarTemplateFunction<decimal, string, string>
	{
		public FormatDecimalTemplateFunction()
			: base(
				  "formatDecimal",
				  new DecimalFunctionArgument("number"), 
				  new StringFunctionArgument("format", valueValidator: ValidateFormat))
		{
		}

		public override string Invoke(decimal argument1, string argument2)
		{
			return argument1.ToString(argument2, CultureInfo.CurrentCulture);
		}

		private static ArgumentValueValidationResult ValidateFormat(string format)
		{
			try
			{
				default(decimal).ToString(format, CultureInfo.CurrentCulture);
				return new ArgumentValueValidationResult(true);
			}
			catch (FormatException)
			{
				return new ArgumentValueValidationResult(false, "Format string is invalid");
			}
		}
	}
}
