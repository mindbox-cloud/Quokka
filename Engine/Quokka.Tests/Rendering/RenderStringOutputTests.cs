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

			Assert.AreEqual("2,542,53511", result);
		}
	}
}
