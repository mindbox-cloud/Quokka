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
	public class IsConstantTests
	{
		[TestMethod]
		public void TemplateIsConstant_EmptyTemplate_True()
		{
			var template = new Template("");
			Assert.IsTrue(template.IsConstant);
		}

		[TestMethod]
		public void TemplateIsConstant_SingleConstantTemplate_True()
		{
			var template = new Template("Hello");
			Assert.IsTrue(template.IsConstant);
		}

		[TestMethod]
		public void TemplateIsConstant_OutputBlock_False()
		{
			var template = new Template("${ Name }");
			Assert.IsFalse(template.IsConstant);
		}

		[TestMethod]
		public void TemplateIsConstant_OutputBlockInsideConstants_False()
		{
			var template = new Template("Hello, ${ Name }!");
			Assert.IsFalse(template.IsConstant);
		}
	}
}
