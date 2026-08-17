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
	/// <summary>
	/// The reason why an arithmetic operation result could not be evaluated.
	/// Meant to be used by the calling code to build its own message, e.g. a localized one.
	/// </summary>
	public enum ArithmeticErrorReason
	{
		/// <summary>
		/// The result is infinite. Within a template this practically always means a division by zero;
		/// an overflow of intermediate values to infinity is also reported this way.
		/// </summary>
		DivisionByZero,

		/// <summary>
		/// The result is not a number, e.g. when zero is divided by zero.
		/// </summary>
		NotANumber,

		/// <summary>
		/// The result is a finite number, but it is too large to be represented as a template value.
		/// </summary>
		ResultOutOfRange
	}
}
