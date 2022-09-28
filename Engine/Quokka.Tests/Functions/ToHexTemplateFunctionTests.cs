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

namespace Mindbox.Quokka.Tests
{
	[TestClass]
	public class ToHexTemplateFunctionTests
	{
		[TestMethod]
		public void Invoke_ReturnsValidHexValue()
		{
			var email = "https://strange@email.com?some=parame&other#things";
			var hexValue = new ToHexTemplateFunction();

			var hash = hexValue.Invoke(email);
			Assert.AreEqual(
				"68747470733A2F2F737472616E676540656D61696C2E636F6D3F736F6D653D706172616D65266F74686572237468696E6773",
				hash);
		}
	}
}
