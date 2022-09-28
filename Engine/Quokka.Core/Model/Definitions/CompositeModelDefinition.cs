﻿// // Copyright 2022 Mindbox Ltd
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

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mindbox.Quokka
{
	internal class CompositeModelDefinition : ICompositeModelDefinition
	{
		public static CompositeModelDefinition Empty { get; } =
			new CompositeModelDefinition(
				new ReadOnlyDictionary<string, IModelDefinition>(
					new Dictionary<string, IModelDefinition>()),
				new ReadOnlyDictionary<IMethodCallDefinition, IModelDefinition>(
					new Dictionary<IMethodCallDefinition, IModelDefinition>()));

		public IReadOnlyDictionary<string, IModelDefinition> Fields { get; }
		public IReadOnlyDictionary<IMethodCallDefinition, IModelDefinition> Methods { get; }

		public CompositeModelDefinition(
			IReadOnlyDictionary<string, IModelDefinition> fields = null,
			IReadOnlyDictionary<IMethodCallDefinition, IModelDefinition> methods = null)
		{
			Fields = fields ?? new Dictionary<string, IModelDefinition>();
			Methods = methods ?? new Dictionary<IMethodCallDefinition, IModelDefinition>();
		}
	}
}
