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
