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
	public class SqrtTemplateFunctionTests
	{
		[TestMethod]
		public void Invoke_PerfectSquare4_Returns2()
		{
			var sqrt = new SqrtTemplateFunction();

			var result = sqrt.Invoke(RenderSettings.Default, 4);

			Assert.AreEqual(2m, result);
		}

		[TestMethod]
		public void Invoke_PerfectSquare9_Returns3()
		{
			var sqrt = new SqrtTemplateFunction();

			var result = sqrt.Invoke(RenderSettings.Default, 9);

			Assert.AreEqual(3m, result);
		}

		[TestMethod]
		public void Invoke_PerfectSquare16_Returns4()
		{
			var sqrt = new SqrtTemplateFunction();

			var result = sqrt.Invoke(RenderSettings.Default, 16);

			Assert.AreEqual(4m, result);
		}

		[TestMethod]
		public void Invoke_PerfectSquare25_Returns5()
		{
			var sqrt = new SqrtTemplateFunction();

			var result = sqrt.Invoke(RenderSettings.Default, 25);

			Assert.AreEqual(5m, result);
		}

		[TestMethod]
		public void Invoke_Zero_ReturnsZero()
		{
			var sqrt = new SqrtTemplateFunction();

			var result = sqrt.Invoke(RenderSettings.Default, 0);

			Assert.AreEqual(0m, result);
		}

		[TestMethod]
		public void Invoke_One_ReturnsOne()
		{
			var sqrt = new SqrtTemplateFunction();

			var result = sqrt.Invoke(RenderSettings.Default, 1);

			Assert.AreEqual(1m, result);
		}

		[TestMethod]
		public void Invoke_NonPerfectSquare2_ReturnsApproximateValue()
		{
			var sqrt = new SqrtTemplateFunction();

			var result = sqrt.Invoke(RenderSettings.Default, 2);

			Assert.AreEqual(1.4142135623730951m, result, 0.000000000001m);
		}

		[TestMethod]
		public void Invoke_NonPerfectSquare10_ReturnsApproximateValue()
		{
			var sqrt = new SqrtTemplateFunction();

			var result = sqrt.Invoke(RenderSettings.Default, 10);

			Assert.AreEqual(3.1622776601683795m, result, 0.000000000001m);
		}

		[TestMethod]
		public void Invoke_LargeNumber_ReturnsCorrectValue()
		{
			var sqrt = new SqrtTemplateFunction();

			var result = sqrt.Invoke(RenderSettings.Default, 10000);

			Assert.AreEqual(100m, result);
		}

		[TestMethod]
		public void Invoke_DecimalInput_ReturnsCorrectValue()
		{
			var sqrt = new SqrtTemplateFunction();

			var result = sqrt.Invoke(RenderSettings.Default, 6.25m);

			Assert.AreEqual(2.5m, result);
		}
	}
}
