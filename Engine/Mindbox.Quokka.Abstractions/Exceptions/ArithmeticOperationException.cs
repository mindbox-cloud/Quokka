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
	/// <summary>
	/// This exception occurs when an arithmetic operation within a template produces a result
	/// which can't be used as a template value: an infinite one, a not-a-number one or an out of range one.
	/// </summary>
	[Serializable]
	public class ArithmeticOperationException : UnrenderableTemplateModelException
	{
		/// <summary>
		/// The error text without the reason, the expression and the location appended to it.
		/// </summary>
		public const string ErrorText = "Arithmetic operation result could not be evaluated";

		/// <summary>
		/// The reason the result could not be evaluated.
		/// </summary>
		public ArithmeticErrorReason Reason { get; }

		public ArithmeticOperationException(
			ArithmeticErrorReason reason,
			string expression,
			Location location,
			Exception inner)
			: base(ErrorText, GetReasonText(reason), expression, location, inner)
		{
			Reason = reason;

			Data[QuokkaExceptionData.Reason] = reason.ToString();
		}

		/// <summary>
		/// The reason in the same wording the exception message uses.
		/// </summary>
		public static string GetReasonText(ArithmeticErrorReason reason)
		{
			switch (reason)
			{
				case ArithmeticErrorReason.DivisionByZero:
					return "division by zero";

				case ArithmeticErrorReason.NotANumber:
					return "the result is not a number";

				case ArithmeticErrorReason.ResultOutOfRange:
					return "the result is out of the supported number range";

				default:
					throw new ArgumentOutOfRangeException(nameof(reason), reason, null);
			}
		}
	}
}
