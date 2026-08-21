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

using Mindbox.Quokka.Generated;

namespace Mindbox.Quokka
{
	internal class VisitingContext
	{
		private readonly Func<VisitingContext, IQuokkaVisitor<StaticBlock>> staticBlockFactoryMethod;

		private readonly HashSet<string> nonIdempotentMethodNames;

		private readonly Dictionary<(string Name, string Arguments), int> nonIdempotentMethodCallCounts = new();

		public SyntaxErrorListener ErrorListener { get; }

		public VisitingContext(
			SyntaxErrorListener errorListener,
			Func<VisitingContext, IQuokkaVisitor<StaticBlock>> staticBlockFactoryMethod,
			IEnumerable<string> nonIdempotentMethodNames = null)
		{
			this.staticBlockFactoryMethod = staticBlockFactoryMethod;
			this.nonIdempotentMethodNames = new HashSet<string>(
				nonIdempotentMethodNames ?? Array.Empty<string>(),
				StringComparer.OrdinalIgnoreCase);
			ErrorListener = errorListener;
		}

		public IQuokkaVisitor<StaticBlock> CreateStaticBlockVisitor()
		{
			return staticBlockFactoryMethod(this);
		}

		public int? GetNextCallOrdinal(string methodName, string argumentsSource)
		{
			if (!nonIdempotentMethodNames.Contains(methodName))
				return null;

			var callKey = (methodName.ToLowerInvariant(), (argumentsSource ?? string.Empty).ToLowerInvariant());
			var callOrdinal = nonIdempotentMethodCallCounts.TryGetValue(callKey, out var previousCallOrdinal)
				? previousCallOrdinal + 1
				: 1;

			nonIdempotentMethodCallCounts[callKey] = callOrdinal;

			return callOrdinal;
		}
	}
}
