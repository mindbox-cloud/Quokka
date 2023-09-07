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

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Mindbox.Quokka.Abstractions;

namespace Mindbox.Quokka.Tests
{
	[TestClass]
	public class Sha512HashFunctionTests
	{
		[TestMethod]
		public void Invoke_ReturnsValidHash()
		{
			var email = "strange@email.com";
			var sha256 = new Sha512HashFunction();

			var hash = sha256.Invoke(RenderSettings.Default, email);
			Assert.AreEqual(
				"36A612F958405DB0EB1D456BE3C25867CC9895D1A10696205F7655728E94229362F0511AF4029692D72F1C421A5213F5AFC20B1CAF5271CFB6D1018624B17CE7",
				hash);
		}
	}
}
