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

using System.Collections.Generic;

namespace Mindbox.Quokka
{
    public interface IMethodCallDefinition
    {
        string Name { get; }
        IReadOnlyList<IMethodArgumentDefinition> Arguments { get; }

        /// <summary>
        /// The 1-based number of this call among the calls of a non-idempotent method with the same name and arguments,
        /// or <c>null</c> if the method is idempotent and all its identical calls share a single definition.
        /// </summary>
        int? CallOrdinal { get; }
    }
}