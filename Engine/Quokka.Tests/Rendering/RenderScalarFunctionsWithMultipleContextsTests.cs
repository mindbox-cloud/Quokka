using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mindbox.Quokka.Tests
{
	[TestClass]
	public class RenderScalarFunctionsWithMultipleContextsTests
	{
		[TestMethod]
		public void TwoFunctionsUseDifferentCallContexts_BothContextsProvided_Success()
		{
			var functionRegistry = new FunctionRegistry(new TemplateFunction[]
			{
				new Function1("UseContext1", new StringFunctionArgument("NonImportantParameter")), 
				new Function2("UseContext2", new StringFunctionArgument("NonImportantParameter")) 
			});

			var template = new Template(@"
					${ UseContext1(''); }
					${ UseContext2(''); }
				",
				functionRegistry);

			var result = template.Render(
				new CompositeModelValue(),
				new CallContextContainerBuilder()
					.WithCallContext<Context1>(new Context1 { Value = "Venus" })
					.WithCallContext<Context2>(new Context2 { Value = "Mars" })
					.Build());

			TemplateAssert.AreOutputsEquivalent(@"
				Venus
				Mars",
				result);
		}

		class Context1
		{
			public string Value { get; set; }
		}

		class Context2
		{
			public string Value { get; set; }
		}

		class Function1 : ContextScalarTemplateFunction<Context1, string, string>
		{
			public Function1(string name, ScalarArgument<string> argument) 
				: base(name, argument)
			{
			}

			protected override string Invoke(Context1 context, string value)
			{
				return context.Value;
			}
		}

		class Function2 : ContextScalarTemplateFunction<Context2, string, string>
		{
			public Function2(string name, ScalarArgument<string> argument)
				: base(name, argument)
			{
			}

			protected override string Invoke(Context2 context, string value)
			{
				return context.Value;
			}
		}
	}
}
