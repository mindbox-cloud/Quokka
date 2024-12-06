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
using System.Collections.Generic;
using System.Linq;

namespace Mindbox.Quokka
{
	public class ArrayModelValue : CompositeModelValue, IArrayModelValue
	{
		public IList<IModelValue> Elements { get; }

		public ArrayModelValue(params IModelValue[] elements)
			: this((IEnumerable<IModelValue>)elements)
		{
		}
		
		public ArrayModelValue(
			IEnumerable<IModelValue> elements,
			IEnumerable<IModelField> fields = null,
			IEnumerable<IModelMethod> methods = null)
			: base(fields, methods)
		{
			ArgumentNullException.ThrowIfNull(elements);

			Elements = elements
				.ToList()
				.AsReadOnly();
		}
	}
}