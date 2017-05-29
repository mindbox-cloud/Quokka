using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Mindbox.Quokka.Tests;

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
	    public void Render_MethodCall__MethodNameCaseInsensitivity()
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
		    var template = new Template("${ Computer.StripDots('A.B.C') }");

		    var result = template.Render(
			    new CompositeModelValue(
				    new ModelField(
					    "Computer",
					    new CompositeModelValue(
						    new ModelMethod("StripDots", new object[] { "A.B.C" }, "ABC")))));

		    Assert.AreEqual("ABC", result);
		}

	    [TestMethod]
	    public void Render_MethodCall_StringArgumentCaseInsensitivity()
	    {
		    var template = new Template("${ Chemistry.GetAtomicNumber('Chlorine') }");

		    var result = template.Render(
			    new CompositeModelValue(
				    new ModelField(
						"Chemistry",
					    new CompositeModelValue(
						    new ModelMethod("GetAtomicNumber", new object[] { "chLOriNE" }, 17)))));

		    Assert.AreEqual("17", result);
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
		    var template = new Template("${ Computer.Concat('A', 'BC') }");

		    var result = template.Render(
			    new CompositeModelValue(
				    new ModelField(
					    "Computer",
					    new CompositeModelValue(
						    new ModelMethod(
								"Concat",
							    new object[] { "DE", "FG" },
							    "DEFG"),

						    new ModelMethod(
								"Concat",
							    new object[] { "BC", "A" },
							    "BCA"),

							new ModelMethod(
								"Concat",
							    new object[] { "A", "BC" },
							    "ABC"),

							new ModelMethod(
								"Concat",
							    new object[] { "XY", "Z" },
							    "XYZ")))));

		    Assert.AreEqual("ABC", result);
		}

	    [TestMethod]
	    public void Render_MethodCall_MultipleCallsWithSameArguments()
	    {
		    var template = new Template("${ Input.Min(6, 13) } ${ Input.Min(6, 13) }");

		    var result = template.Render(
			    new CompositeModelValue(
				    new ModelField(
						"Input",
					    new CompositeModelValue(
						    new ModelMethod(
								"Min",
							    new object[] { 6, 13 },
							    6)))));

		    Assert.AreEqual("6 6", result);
		}

	    [TestMethod]
	    public void Render_MethodCall_MultipleCallsWithDifferentArguments()
	    {
		    var template = new Template("${ Input.Min(7, 15) } ${ Input.Min(32, 25) }");

		    var result = template.Render(
			    new CompositeModelValue(
				    new ModelField(
					    "Input",
					    new CompositeModelValue(
						    new ModelMethod(
							    "Min",
							    new object[] { 7, 15 },
							    7),
						    new ModelMethod(
							    "Min",
							    new object[] { 32, 25 },
							    25)))));

		    Assert.AreEqual("7 25", result);
	    }

		[TestMethod]
	    public void Render_MethodCall_CompositeReturned_DifferentFieldsUsed()
	    {
		    var template = new Template(@"
				${ Recipient.GetAddress('main').City }
				${ Recipient.GetAddress('main').ZipCode }
				${ Recipient.GetAddress('main').ToString() }
			");

		    var result = template.Render(
			    new CompositeModelValue(
				    new ModelField(
					    "Recipient",
					    new CompositeModelValue(
						    new ModelMethod(
							    "GetAddress",
							    new object[] { "main" },
							    new CompositeModelValue(
								    new[]
								    {
									    new ModelField("City", "Newark"),
									    new ModelField("ZipCode", 440)
								    },
								    new[]
								    {
									    new ModelMethod("ToString", "Newark, 440")
								    }))))));

		    TemplateAssert.AreOutputsEquivalent(
				@"
					Newark
					440
					Newark, 440
				",
				result);
	    }
	}
}
