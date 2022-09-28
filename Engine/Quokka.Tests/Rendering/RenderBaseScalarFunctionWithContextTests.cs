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

namespace Mindbox.Quokka.Tests
{
	[TestClass]
	public class RenderBaseScalarFunctionWithContextTests
	{
		[TestMethod]
		public void Render_ScalarFunctionWithContext_NoContextProvided_Fail()
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

			var exception = Assert.ThrowsException<UnrenderableTemplateModelException>(
				() => template.Render(new CompositeModelValue()));

			var innerException = exception.InnerException as InvalidOperationException;
			Assert.IsNotNull(innerException);

			Assert.AreEqual($"Call context of type {typeof(TestContext).FullName} wasn't registered", innerException.Message);

			Assert.IsFalse(wasFunctionCalled);
		}

		[TestMethod]
		public void Render_ScalarFunctionWithContext_ContextWithWrongTypeProvided_Fail()
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

			var exception = Assert.ThrowsException<UnrenderableTemplateModelException>(
				() => template.Render(new CompositeModelValue(), CallContextContainer.Create(new AnotherTestContext())));

			var innerException = exception.InnerException as InvalidOperationException;
			Assert.IsNotNull(innerException);

			Assert.AreEqual(
				$"Call context of type {typeof(TestContext).FullName} wasn't registered",
				innerException.Message);

			Assert.IsFalse(wasFunctionCalled);
		}

		[TestMethod]
		public void Render_ScalarFunctionWithContext_ContextProvided_Success()
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

			var result = template.Render(
				new CompositeModelValue(),
				CallContextContainer.Create(
					new TestContext
					{
						TestInt = 10
					}));

			Assert.AreEqual($"[10][Venus]", result);
			Assert.IsTrue(wasFunctionCalled);
		}

		[TestMethod]
		public void Render_ScalarFunctionWithContext_ContextOfDerivedTypeProvided_Success()
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

			var result = template.Render(
				new CompositeModelValue(),
				CallContextContainer.Create<TestContext>(
					new ChildTestContext
					{
						TestInt = 10
					}));

			Assert.AreEqual($"[10][Venus]", result);
			Assert.IsTrue(wasFunctionCalled);
		}
		
		[TestMethod]
		public void Render_ScalarFunctionWithContext_2Arguments_Success()
		{
			var template = new Template(
				"${ Concat2('Venus', 'Mars') }",
				new FunctionRegistry(new[]
				{
					new TestFunc2(),
				}));

			var result = template.Render(
				new CompositeModelValue(),
				CallContextContainer.Create<TestContext>(
					new ChildTestContext
					{
						TestInt = 10
					}));

			Assert.AreEqual($"[10][Venus][Mars]", result);
		}

		private class TestFunc1 : ContextScalarTemplateFunction<TestContext, string, string>
		{
			private readonly Action callBack;

			public TestFunc1(Action callBack)
				: base(
					"Concat1",
					new StringFunctionArgument("arg1"))
			{
				this.callBack = callBack;
			}

			protected override string Invoke(TestContext context, string value1)
			{
				callBack();
				return $"[{context.TestInt}][{value1}]";
			}
		}

		private class TestFunc2 : ContextScalarTemplateFunction<TestContext, string, string, string>
		{
			public TestFunc2()
				: base(
					"Concat2",
					new StringFunctionArgument("arg1"), 
					new StringFunctionArgument("arg2"))
			{
			}

			protected override string Invoke(TestContext context, string value1, string value2)
			{
				return $"[{context.TestInt}][{value1}][{value2}]";
			}
		}

		private class TestContext
		{
			public int TestInt { get; set; }
		}

		private class AnotherTestContext
		{
			public int TestInt { get; set; }
		}

		private class ChildTestContext : TestContext
		{

		}
	}
}
