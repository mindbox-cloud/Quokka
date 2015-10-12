using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Quokka.Tests
{
	[TestClass]
	public class ApplyStandardFunctionTests
	{
		[TestMethod]
		public void Apply_Function_ReplaceIfEmpty_FirstOptionNotEmpty()
		{
			var template = new Template("${ replaceIfEmpty(\"Marilyn\", \"Keira\") }");

			var result = template.Apply(new CompositeModelValue());

			Assert.AreEqual("Marilyn", result);
		}

		[TestMethod]
		public void Apply_Function_ReplaceIfEmpty_FirstOptionEmpty()
		{
			var template = new Template("${ replaceIfEmpty(\"\", \"Keira\") }");

			var result = template.Apply(new CompositeModelValue());

			Assert.AreEqual("Keira", result);
		}
		
		[TestMethod]
		public void Apply_Function_ReplaceIfEmpty_FirstOptionWhiteSpace()
		{
			var template = new Template("${ replaceIfEmpty(\"  \", \"Keira\") }");

			var result = template.Apply(new CompositeModelValue());

			Assert.AreEqual("Keira", result);
		}

		[TestMethod]
		public void Apply_Function_ReplaceIfEmpty_FirstOptionNonEmptyParameter()
		{
			var template = new Template("${ replaceIfEmpty($Value1, $Value2) }");

			var result = template.Apply(
				new CompositeModelValue(
					new ModelField("Value1", new PrimitiveModelValue("Plan A")),
					new ModelField("Value2", new PrimitiveModelValue("Plan B"))));

			Assert.AreEqual("Plan A", result);
		}

		[TestMethod]
		public void Apply_Function_ReplaceIfEmpty_FirstOptionEmptyParameter()
		{
			var template = new Template("${ replaceIfEmpty($Value1, $Value2) }");

			var result = template.Apply(
				new CompositeModelValue(
					new ModelField("Value1", new PrimitiveModelValue("")),
					new ModelField("Value2", new PrimitiveModelValue("Plan B"))));

			Assert.AreEqual("Plan B", result);
		}
	}
}
