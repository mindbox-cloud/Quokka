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

			Assert.AreEqual($"Can't get call context from empty {nameof(CallContextContainer)}", innerException.Message);

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
				() => template.Render(new CompositeModelValue(), CallContextContainer.WithValue(new AnotherTestContext())));

			var innerException = exception.InnerException as InvalidOperationException;
			Assert.IsNotNull(innerException);

			Assert.AreEqual(
				$"Call context contains object of type {typeof(AnotherTestContext)}, " +
					$"which is not compatible with requested type {typeof(TestContext)}",
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
				CallContextContainer.WithValue(
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
				CallContextContainer.WithValue(
					new ChildTestContext
					{
						TestInt = 10
					}));

			Assert.AreEqual($"[10][Venus]", result);
			Assert.IsTrue(wasFunctionCalled);
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

			public override string Invoke(TestContext context, string value1)
			{
				callBack();
				return $"[{context.TestInt}][{value1}]";
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
