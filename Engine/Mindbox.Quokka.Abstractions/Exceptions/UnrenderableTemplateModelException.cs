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
	/// This exceptions occurs when the model technically fits the requirements, but some runtime operations can't be performed.
	/// Examples would be trying to output or use in a condition a null value without checking variable for null first,
	/// trying to divide by zero etc.
	/// </summary>
	[Serializable]
	public class UnrenderableTemplateModelException : TemplateException
	{
		/// <summary>
		/// The error text of a null value usage, without the expression and the location appended to it.
		/// </summary>
		public const string NullValueErrorText = "An attempt to use a null value";

		/// <summary>
		/// The error text of a variable whose value is not present in the model,
		/// without the expression and the location appended to it.
		/// </summary>
		public const string ValueNotFoundErrorText = "Value for variable not found";

		/// <summary>
		/// The error text of a failed template function call, without the failure details,
		/// the expression and the location appended to it.
		/// </summary>
		public const string FunctionFailedErrorText = "Function invocation resulted in error";

		public Location Location { get; }

		/// <summary>
		/// The source text of the failed expression as it is written in the template,
		/// e.g. "cart.total / cart.itemCount". <c>Null</c> if the text can't be restored.
		/// </summary>
		public string Expression { get; }

		public UnrenderableTemplateModelException(string message, Location location)
			: base(message)
		{
			Location = location;
			FillLocationData(location);
		}

		public UnrenderableTemplateModelException(string message, Exception inner, Location location)
			: base(message, inner)
		{
			Location = location;
			FillLocationData(location);
		}

		public UnrenderableTemplateModelException(
			string errorText,
			string details,
			string expression,
			Location location,
			Exception inner)
			: base(BuildMessage(errorText, details, expression, location), inner)
		{
			Location = location;
			Expression = expression;

			Data[QuokkaExceptionData.ErrorText] = errorText;

			if (expression != null)
				Data[QuokkaExceptionData.Expression] = expression;

			FillLocationData(location);
		}

		private static string BuildMessage(string errorText, string details, string expression, Location location)
		{
			var message = errorText;

			if (!string.IsNullOrWhiteSpace(details))
				message += $": {details}";

			if (!string.IsNullOrWhiteSpace(expression))
				message += $" in \"{expression}\"";

			if (location != null)
				message += $" at {location}";

			return message;
		}

		private void FillLocationData(Location location)
		{
			if (location == null)
				return;

			Data[QuokkaExceptionData.Location] = location.ToString();
			Data[QuokkaExceptionData.Line] = location.Line;
			Data[QuokkaExceptionData.Column] = location.Column;
		}
	}
}
