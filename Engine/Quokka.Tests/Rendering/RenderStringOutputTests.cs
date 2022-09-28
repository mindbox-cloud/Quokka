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
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mindbox.Quokka.Tests
{
	[TestClass]
	public class RenderStringOutputTests
	{
		[TestMethod]
		public void Render_StringOutput_ConstantWithConstantConcatenation()
		{
			var template = new Template("${ \"tratata\"&\"murmur\" }");

			var result = template.Render(
				new CompositeModelValue());

			Assert.AreEqual("tratatamurmur", result);
		}

		[TestMethod]
		public void Render_StringOutput_VariableWithConstantConcatenation()
		{
			var template = new Template("${ variable & \"murmur\" }");

			var result = template.Render(
				new CompositeModelValue(
					new[] { new ModelField("variable", new PrimitiveModelValue(3)) }
				));

			Assert.AreEqual("3murmur", result);
		}

		[TestMethod]
		public void Render_StringOutput_MultipleConcatenations()
		{
			var template = new Template("${ variable & \"murmur\" & variable2 }");

			var result = template.Render(
				new CompositeModelValue(
					new[] {
						new ModelField("variable", new PrimitiveModelValue(3)),
						new ModelField("variable2", new PrimitiveModelValue("tratata")),
					}
				));

			Assert.AreEqual("3murmurtratata", result);
		}

		[TestMethod]
		public void Render_StringOutput_FunctionCallWithVariableConcatenation()
		{
			var template = new Template("${ formatDecimal(variable, \"N2\") & variable }");

			var result = template.Render(
				new CompositeModelValue(
					new[] { new ModelField("variable", new PrimitiveModelValue(2.53511m)) }
				));

			Assert.AreEqual("2.542.53511", result);
		}
	}
}
