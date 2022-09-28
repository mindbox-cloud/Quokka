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
using System.Collections.ObjectModel;
using System.Linq;

namespace Mindbox.Quokka
{
	/// <summary>
	/// The collection of members (can represent a collection of root-level variables in the scope
	/// or a member collection of a single composite variable).
	/// </summary>
	internal class MemberCollection<TMemberKey>
		where TMemberKey : IEquatable<TMemberKey>
	{
		private static readonly IEqualityComparer<TMemberKey> equalityComparer;

		static MemberCollection()
		{
			MemberCollection<string>.equalityComparer = StringComparer.OrdinalIgnoreCase;
		}

		private readonly Dictionary<TMemberKey, ValueUsageSummary> items;

		public IReadOnlyCollection<KeyValuePair<TMemberKey, ValueUsageSummary>> Items => items;

		public MemberCollection()
			: this(new Dictionary<TMemberKey, ValueUsageSummary>(equalityComparer))
		{
		}

		private MemberCollection(Dictionary<TMemberKey, ValueUsageSummary> items)
		{
			this.items = items;
		}
		
		public bool CheckIfMemberExists(TMemberKey key)
		{
			return items.ContainsKey(key);
		}
		
		public ValueUsageSummary CreateOrUpdateMember(TMemberKey memberKey, ValueUsage valueUsage)
		{
			if (!items.TryGetValue(memberKey, out ValueUsageSummary definition))
			{
				definition = new ValueUsageSummary(memberKey.ToString());
				items.Add(memberKey, definition);
			}

			definition.AddUsage(valueUsage);
			return definition;
		}

		public ValueUsageSummary TryGetMemberUsageSummary(TMemberKey key)
		{
			return items.TryGetValue(key, out ValueUsageSummary usageSummary) 
				? usageSummary 
				: null;
		}

		public static MemberCollection<TMemberKey> Merge(
			string ownerFullName, 
			IEnumerable<MemberCollection<TMemberKey>> collections)
		{
			var members = collections
				.SelectMany(collection => collection.items)
				.GroupBy(
					item => item.Key,
					(name, values) => new
					{
						ResultDefinition = ValueUsageSummary.Merge(
							$"{ownerFullName}.{name}",
							values.Select(v => v.Value).ToList()),
						ResultKey = name
					},
					equalityComparer)
				.ToDictionary(
					definition => definition.ResultKey,
					definition => definition.ResultDefinition,
					equalityComparer);

			return new MemberCollection<TMemberKey>(members);
		}
	}
}
