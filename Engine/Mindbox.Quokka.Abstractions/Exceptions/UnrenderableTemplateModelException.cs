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
		public Location Location { get; }

		public UnrenderableTemplateModelException(string message, Location location)
			: base(message)
		{
			Location = location;
		}

		public UnrenderableTemplateModelException(string message, Exception inner, Location location)
			: base(message, inner)
		{
			Location = location;
		}
	}
}
