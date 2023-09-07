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
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Mindbox.Quokka.Abstractions;

namespace Mindbox.Quokka.Tests
{
	[TestClass]
	public class RenderBaseScalarFunctionTests
	{
		[TestMethod]
		public void Render_GnericScalarFunction_1Argument()
		{
			bool wasFunctionCalled = false;
			Action callBack =
				() =>
				{
					wasFunctionCalled = true;
				};

			var template = new Template(
				"${ Concat1('Venus') }",
				new FunctionRegistry(new []
				{
					new TestFunc1(callBack), 
				}));

			var result = template.Render(new CompositeModelValue());

			Assert.AreEqual("[Venus]", result);
			Assert.IsTrue(wasFunctionCalled);
		}

		[TestMethod]
		public void Render_GnericScalarFunction_2Arguments()
		{
			bool wasFunctionCalled = false;
			Action callBack =
				() =>
				{
					wasFunctionCalled = true;
				};

			var template = new Template(
				"${ Concat2('Venus', 'Mercury') }",
				new FunctionRegistry(new[]
				{
					new TestFunc2(callBack),
				}));

			var result = template.Render(new CompositeModelValue());

			Assert.AreEqual("[Venus][Mercury]", result);
			Assert.IsTrue(wasFunctionCalled);
		}

		[TestMethod]
		public void Render_GnericScalarFunction_3Arguments()
		{
			bool wasFunctionCalled = false;
			Action callBack =
				() =>
				{
					wasFunctionCalled = true;
				};

			var template = new Template(
				"${ Concat3('Venus', 'Mercury', 'Jupiter') }",
				new FunctionRegistry(new[]
				{
					new TestFunc3(callBack),
				}));

			var result = template.Render(new CompositeModelValue());

			Assert.AreEqual("[Venus][Mercury][Jupiter]", result);
			Assert.IsTrue(wasFunctionCalled);
		}

		[TestMethod]
		public void Render_GnericScalarFunction_4Arguments()
		{
			bool wasFunctionCalled = false;
			Action callBack =
				() =>
				{
					wasFunctionCalled = true;
				};

			var template = new Template(
				"${ Concat4('Venus', 'Mercury', 'Jupiter', 'Saturn') }",
				new FunctionRegistry(new[]
				{
					new TestFunc4(callBack),
				}));

			var result = template.Render(new CompositeModelValue());

			Assert.AreEqual("[Venus][Mercury][Jupiter][Saturn]", result);
			Assert.IsTrue(wasFunctionCalled);
		}

		private class TestFunc1 : ScalarTemplateFunction<string, string>
		{
			private readonly Action callBack;

			public TestFunc1(Action callBack)
				: base(
					"Concat1",
					new StringFunctionArgument("arg1"))
			{
				this.callBack = callBack;
			}

			public override string Invoke(string value1)
			{
				callBack();
				return $"[{value1}]";
			}
		}

		private class TestFunc2 : ScalarTemplateFunction<string, string, string>
		{
			private readonly Action callBack;

			public TestFunc2(Action callBack)
				: base(
					"Concat2",
					new StringFunctionArgument("arg1"),
					new StringFunctionArgument("arg2"))
			{
				this.callBack = callBack;
			}

			public override string Invoke(RenderSettings settings, string value1, string value2)
			{
				callBack();
				return $"[{value1}][{value2}]";
            }
		}

		private class TestFunc3 : ScalarTemplateFunction<string, string, string, string>
		{
			private readonly Action callBack;

			public TestFunc3(Action callBack)
				: base(
					"Concat3",
					new StringFunctionArgument("arg1"),
					new StringFunctionArgument("arg2"),
					new StringFunctionArgument("arg3"))
			{
				this.callBack = callBack;
			}

			public override string Invoke(string value1, string value2, string value3)
			{
				callBack();
				return $"[{value1}][{value2}][{value3}]";
			}
		}

		private class TestFunc4 : ScalarTemplateFunction<string, string, string, string, string>
		{
			private readonly Action callBack;

			public TestFunc4(Action callBack)
				: base(
					"Concat4",
					new StringFunctionArgument("arg1"),
					new StringFunctionArgument("arg2"),
					new StringFunctionArgument("arg3"),
					new StringFunctionArgument("arg4"))
			{
				this.callBack = callBack;
			}

			public override string Invoke(string value1, string value2, string value3, string value4)
			{
				callBack();
				return $"[{value1}][{value2}][{value3}][{value4}]";
			}
		}
	}
}
