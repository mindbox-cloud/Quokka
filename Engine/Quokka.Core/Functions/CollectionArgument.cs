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
	public class CollectionArgument : TemplateFunctionArgument
	{
		public CollectionArgument(string name)
			: base(name)
		{
		}

		internal override TypeDefinition Type => TypeDefinition.Array;

		internal override ArgumentValueValidationResult ValidateConstantValue(VariableValueStorage value)
		{
			// Shouldn't be called because this argument couldn't possibly receive a static scalar value known at compile-time
			throw new NotImplementedException();
		}
	}
}
