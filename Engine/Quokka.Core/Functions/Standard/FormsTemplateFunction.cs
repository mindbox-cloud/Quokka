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

using Mindbox.Quokka.Abstractions;

namespace Mindbox.Quokka
{
	internal class FormsTemplateFunction : ScalarTemplateFunction<decimal, string, string, string, string>
	{
		public FormsTemplateFunction()
			: base(
				"forms",
				new DecimalFunctionArgument("quantity"),
				new StringFunctionArgument("singularForm"),
				new StringFunctionArgument("dualForm"),
				new StringFunctionArgument("pluralForm"))
		{
		}

		public override string Invoke(RenderSettings settings, decimal quantity, string singularForm, string dualForm, string pluralForm)
		{
			var intQuantity = Convert.ToInt32(quantity);
			return LanguageUtility.GetQuantityForm(intQuantity, singularForm, dualForm, pluralForm);
		}
	}
}