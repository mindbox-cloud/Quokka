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
	public class Md5HashFunctionTests
	{
		[TestMethod]
		public void Invoke_ReturnsValidHash()
		{
			var email = "strange@email.com";
			var md5 = new Md5HashFunction();

			var hash = md5.Invoke(RenderSettings.Default, email);
			Assert.AreEqual("152F2DD5F1F056FC761717EA5965828C", hash);
		}
	}
}
