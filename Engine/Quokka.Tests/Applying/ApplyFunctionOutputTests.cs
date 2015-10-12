using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Quokka.Tests
{
	[TestClass]
	public class ApplyFunctionOutputTests
	{
		[TestMethod]
		public void Apply_SingleStringParameter()
		{
			var template = new Template("${ toUpper(Name) }");

			var result = template.Apply(
				new CompositeModelValue(
					new ModelField("Name", "Angelina")));

			Assert.AreEqual("ANGELINA", result);
		}
	}
}
