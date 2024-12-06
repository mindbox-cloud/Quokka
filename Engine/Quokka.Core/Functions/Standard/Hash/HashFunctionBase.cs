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
using System.Security.Cryptography;
using System.Text;

using Mindbox.Quokka.Abstractions;

namespace Mindbox.Quokka
{
	internal abstract class HashFunctionBase : ScalarTemplateFunction<string, string>
	{
		protected abstract HashAlgorithm CreateHashAlgorithm();

		public HashFunctionBase(string name)
			: base(
				  name,
				  new StringFunctionArgument("string"))
		{
		}

		public override string Invoke(RenderSettings settings, string argument1)
		{
			return GetHashString(argument1);
		}

		private string GetHashString(string value)
		{
			ArgumentNullException.ThrowIfNull(value);

			using var hashAlgorithm = CreateHashAlgorithm();
			var plainBytes = Encoding.UTF8.GetBytes(value);
			var encodedBytes = hashAlgorithm.ComputeHash(plainBytes);
			return ByteUtility.ToHexString(encodedBytes);
		}
	}
}
