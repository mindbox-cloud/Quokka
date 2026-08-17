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
	/// Keys of the <see cref="System.Exception.Data"/> entries filled in by runtime render errors
	/// (<see cref="UnrenderableTemplateModelException"/> and its descendants).
	/// The entries hold the parts the exception message is built from, so that the calling code
	/// can render its own message (e.g. a localized one) without parsing the message text.
	/// </summary>
	public static class QuokkaExceptionData
	{
		/// <summary>
		/// The error text without any details appended to it, e.g.
		/// "Arithmetic operation result could not be evaluated". Value type is <see cref="string"/>.
		/// </summary>
		public const string ErrorText = "Quokka.ErrorText";

		/// <summary>
		/// The name of the reason enumeration member, e.g. "DivisionByZero". Value type is <see cref="string"/>.
		/// </summary>
		public const string Reason = "Quokka.Reason";

		/// <summary>
		/// The source text of the template expression which failed, e.g. "cart.total / cart.itemCount".
		/// Value type is <see cref="string"/>.
		/// </summary>
		public const string Expression = "Quokka.Expression";

		/// <summary>
		/// The location of the failure within the template in the "line:column" form. Value type is <see cref="string"/>.
		/// </summary>
		public const string Location = "Quokka.Location";

		/// <summary>
		/// The line of the failure within the template. Value type is <see cref="int"/>.
		/// </summary>
		public const string Line = "Quokka.Line";

		/// <summary>
		/// The column of the failure within the template. Value type is <see cref="int"/>.
		/// </summary>
		public const string Column = "Quokka.Column";
	}
}
