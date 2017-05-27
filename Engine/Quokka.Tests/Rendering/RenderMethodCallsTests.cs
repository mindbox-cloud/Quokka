using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mindbox.Quokka
{
	[TestClass]
	public class RenderMethodCallsTests
    {
	    [TestMethod]
	    public void Render_MethodCall_WithoutArguments_PrimitiveValue()
	    {
		    var template = new Template("${ Object.GetIntValue() }");

		    var result = template.Render(
			    new CompositeModelValue(
				    new ModelField(
					    "Object",
					    new CompositeModelValue(
						    new ModelMethod("GetIntValue", 5)))));

		    Assert.AreEqual("5", result);
	    }

	    [TestMethod]
	    public void Render_MethodCall_WithoutArguments_MethodChain()
	    {
		    var template = new Template("${ Object.GetNumbers().First() }");

		    var result = template.Render(
			    new CompositeModelValue(
				    new ModelField(
					    "Object",
					    new CompositeModelValue(
						    new ModelMethod(
							    "GetNumbers",
							    new CompositeModelValue(
								    new ModelMethod("First", 11)))))));

		    Assert.AreEqual("11", result);
	    }

	    [TestMethod]
	    public void Render_MethodCall_WithoutArguments_CaseInsensitivity()
	    {
		    var template = new Template("${ Object.BEaweSOME() }");

		    var result = template.Render(
			    new CompositeModelValue(
				    new ModelField(
					    "Object",
					    new CompositeModelValue(
						    new ModelMethod("beAwesome", "OK")))));

		    Assert.AreEqual("OK", result);
	    }

		[TestMethod]
	    public void Render_MethodCall_SingleIntegerConstantArgument()
	    {
		    var template = new Template("${ Computer.Square(6) }");

		    var result = template.Render(
			    new CompositeModelValue(
				    new ModelField(
						"Computer",
					    new CompositeModelValue(
						    new ModelMethod("Square", new object [] { 6 }, 36)))));

		    Assert.AreEqual("36", result);
		}

	    [TestMethod]
	    public void Render_MethodCall_SingleDecimalConstantArgument()
	    {
		    var template = new Template("${ Computer.Square(1.11) }");

		    var result = template.Render(
			    new CompositeModelValue(
				    new ModelField(
					    "Computer",
					    new CompositeModelValue(
						    new ModelMethod("Square", new object[] { 1.11m }, 1.2321)))));

		    Assert.AreEqual("1,2321", result);
		}

	    [TestMethod]
	    public void Render_MethodCall_SingleStringConstantArgument()
	    {
		    var template = new Template("${ Computer.StripDots(\"A.B.C\") }");

		    var result = template.Render(
			    new CompositeModelValue(
				    new ModelField(
					    "Computer",
					    new CompositeModelValue(
						    new ModelMethod("StripDots", new object[] { "A.B.C" }, "ABC")))));

		    Assert.AreEqual("ABC", result);
		}

	    [TestMethod]
	    public void Render_MethodCall_MultipleArguments()
	    {
		    var template = new Template("${ Stats.GetValue('January', 2017, 05, 45.5) }");

		    var result = template.Render(
			    new CompositeModelValue(
				    new ModelField(
					    "Stats",
					    new CompositeModelValue(
						    new ModelMethod(
								"GetValue",
							    new object[] { "January", 2017, 05, 45.5m },
							    "Average")))));

		    Assert.AreEqual("Average", result);
		}

	    [TestMethod]
	    public void Render_MethodCall_MultipleCallValuesProvided_CorrectOneIsUsed()
	    {
		    var template = new Template("${ Computer.GetSum(5, 10) }");

		    var result = template.Render(
			    new CompositeModelValue(
				    new ModelField(
					    "Computer",
					    new CompositeModelValue(
						    new ModelMethod(
							    "GetSum",
							    new object[] { 7, 30 },
							    37),

						    new ModelMethod(
							    "GetSum",
							    new object[] { 5, 10 },
							    15),

							new ModelMethod(
							    "GetSum",
							    new object[] { 12, 12 },
							    24)))));

		    Assert.AreEqual("15", result);
	    }
	}
}
