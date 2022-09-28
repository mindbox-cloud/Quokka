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
	public static class LanguageUtility
	{
		/// <summary>
		/// Возвращает нужную форму слова для использования с числительным во фразах вида
		/// "одна майка", "две майки", "четырёх маек", "ста восьми маек".
		/// </summary>
		/// <param name="quantity">Количество (нужно только для выбора нужной формы,
		/// само количество в возвращаемую строку не добавляется).</param>
		/// <param name="singularForm">Форма единственного числа в нужном падеже
		/// (падеж определяется контекстом, в котором фраза используется в предложении).</param>
		/// <param name="dualForm">Форма множественного числа в нужном падеже
		/// (падеж определяется контекстом, в котором фраза используется в предложении)
		/// или форма единственного числа в родительном падеже, если фраза используется в контексте
		/// именительного или винительного падежа в предложении.
		/// Например, форма "майки" во фразе "вижу 2 майки" (фраза в контексте винительного падежа,
		/// слово в родительном падеже, единственном числе)
		/// или форма "майками" во фразе "горжусь своими 3 майками"
		/// (фраза в контексте творительного падежа,
		/// слово в творительном падеже, множественном числе).</param>
		/// <param name="pluralForm">Форма множественного числа в нужном падеже
		/// (падеж определяется контекстом, в котором фраза используется в предложении)
		/// или форма множественного числа в родительном падеже, если фраза используется в контексте
		/// именительного или винительного падежа в предложении.
		/// Например, форма "маек" во фразе "вижу 5 маек" (фраза в контексте винительного падежа,
		/// слово в родительном падеже, множественном числе)
		/// или форма "майками" во фразе "горжусь своими 5 майками"
		/// (фраза в контексте творительного падежа,
		/// слово в творительном падеже, множественном числе).</param>
		/// <returns>Нужная форма слова.</returns>
		public static T GetQuantityForm<T>(
			int quantity,
			T singularForm,
			T dualForm,
			T pluralForm)
		{
			var last2Digits =
				quantity > 0 ? quantity % 100 :
				quantity == int.MinValue ? Math.Abs(int.MinValue + 100) % 100 :
				Math.Abs(quantity) % 100;
			var lastDigit = last2Digits % 10;
			var lastButOneDigit = last2Digits / 10;
			var lastDigitIs2Or3Or4 = lastDigit == 2 || lastDigit == 3 || lastDigit == 4;

			return
				lastButOneDigit == 1 ? pluralForm :
				lastDigit == 1 ? singularForm :
				lastDigitIs2Or3Or4 ? dualForm :
				pluralForm;
		}
	}
}