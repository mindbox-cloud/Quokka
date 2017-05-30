using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Mindbox.Quokka.Tests;

namespace Mindbox.Quokka
{
	[TestClass]
    public class ModelDiscoveryMethodsBasicTests
    {
		[TestMethod]
		public void ModelDiscovery_Method_NoParameters()
		{
			var modelDefinition = new Template("${ Object.GetValue() }")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(
					new Dictionary<string, IModelDefinition>
					{
						{
							"Object", new CompositeModelDefinition(
								methods: new Dictionary<IMethodCallDefinition, IModelDefinition>
								{
									{
										new MethodCallDefinition("GetValue", Array.Empty<IMethodArgumentDefinition>()),
										new PrimitiveModelDefinition(TypeDefinition.Primitive)
									}
								})
						}
					}),
				modelDefinition);
		}

	    [TestMethod]
	    public void ModelDiscovery_Method_SingleStringConstantParameter()
	    {
		    var modelDefinition = new Template("${ Object.GetValue('Name') }")
			    .GetModelDefinition();

		    TemplateAssert.AreCompositeModelDefinitionsEqual(
			    new CompositeModelDefinition(
				    new Dictionary<string, IModelDefinition>
				    {
					    {
						    "Object", new CompositeModelDefinition(
							    methods: new Dictionary<IMethodCallDefinition, IModelDefinition>
							    {
								    {
									    new MethodCallDefinition(
										    "GetValue",
										    new[]
										    {
											    new MethodArgumentDefinition(TypeDefinition.String, "Name")
										    }),
									    new PrimitiveModelDefinition(TypeDefinition.Primitive)
								    }
							    })
					    }
				    }),
			    modelDefinition);
		}

	    [TestMethod]
	    public void ModelDiscovery_Method_StringConstantParameter_CaseInsensitivity()
	    {
		    var modelDefinition = new Template(@"
					${ Object.GetValue('rAnDoM cAsInG') }
					${ Object.GetValue('Random Casing') }
				")
			    .GetModelDefinition();

		    TemplateAssert.AreCompositeModelDefinitionsEqual(
			    new CompositeModelDefinition(
				    new Dictionary<string, IModelDefinition>
				    {
					    {
						    "Object", new CompositeModelDefinition(
							    methods: new Dictionary<IMethodCallDefinition, IModelDefinition>
							    {
								    {
									    new MethodCallDefinition(
										    "GetValue",
										    new[]
										    {
											    new MethodArgumentDefinition(TypeDefinition.String, "rAnDoM cAsInG")
										    }),
									    new PrimitiveModelDefinition(TypeDefinition.Primitive)
								    }
							    })
					    }
				    }),
			    modelDefinition);
	    }

		[TestMethod]
	    public void ModelDiscovery_Method_SingleIntegerConstantParameter()
	    {
		    var modelDefinition = new Template("${ Math.Square(5) }")
			    .GetModelDefinition();

		    TemplateAssert.AreCompositeModelDefinitionsEqual(
			    new CompositeModelDefinition(
				    new Dictionary<string, IModelDefinition>
				    {
					    {
						    "Math", new CompositeModelDefinition(
							    methods: new Dictionary<IMethodCallDefinition, IModelDefinition>
							    {
								    {
									    new MethodCallDefinition(
										    "Square",
										    new[]
										    {
											    new MethodArgumentDefinition(TypeDefinition.Integer, 5)
										    }),
									    new PrimitiveModelDefinition(TypeDefinition.Primitive)
								    }
							    })
					    }
				    }),
			    modelDefinition);
		}

	    [TestMethod]
	    public void ModelDiscovery_Method_SingleDecimalConstantParameter()
	    {
		    var modelDefinition = new Template("${ Math.Square(45.53) }")
			    .GetModelDefinition();

		    TemplateAssert.AreCompositeModelDefinitionsEqual(
			    new CompositeModelDefinition(
				    new Dictionary<string, IModelDefinition>
				    {
					    {
						    "Math", new CompositeModelDefinition(
							    methods: new Dictionary<IMethodCallDefinition, IModelDefinition>
							    {
								    {
									    new MethodCallDefinition(
										    "Square",
										    new[]
										    {
											    new MethodArgumentDefinition(TypeDefinition.Decimal, 45.53m)
										    }),
									    new PrimitiveModelDefinition(TypeDefinition.Primitive)
								    }
							    })
					    }
				    }),
			    modelDefinition);
		}

	    [TestMethod]
	    public void ModelDiscovery_Method_ResultIsArithmeticExpression()
	    {
		    var modelDefinition = new Template("${ Math.Sqrt(16) + 5 }")
			    .GetModelDefinition();

		    TemplateAssert.AreCompositeModelDefinitionsEqual(
			    new CompositeModelDefinition(
				    new Dictionary<string, IModelDefinition>
				    {
					    {
						    "Math", new CompositeModelDefinition(
							    methods: new Dictionary<IMethodCallDefinition, IModelDefinition>
							    {
								    {
									    new MethodCallDefinition(
										    "Sqrt",
										    new[]
										    {
											    new MethodArgumentDefinition(TypeDefinition.Integer, 16)
										    }),
									    new PrimitiveModelDefinition(TypeDefinition.Decimal)
								    }
							    })
					    }
				    }),
			    modelDefinition);
		}

	    [TestMethod]
	    public void ModelDiscovery_Method_ResultIsIntegerExpression()
	    {
		    var modelDefinition = new DefaultTemplateFactory(new[] { new IntegerIdentityFunction() })
			    .CreateTemplate("${ IntegerIdentity(Math.GetIntValue()) }")
			    .GetModelDefinition();

		    TemplateAssert.AreCompositeModelDefinitionsEqual(
			    new CompositeModelDefinition(
				    new Dictionary<string, IModelDefinition>
				    {
					    {
						    "Math", new CompositeModelDefinition(
							    methods: new Dictionary<IMethodCallDefinition, IModelDefinition>
							    {
								    {
									    new MethodCallDefinition(
											"GetIntValue",
										    Array.Empty<IMethodArgumentDefinition>()),
									    new PrimitiveModelDefinition(TypeDefinition.Integer)
								    }
							    })
					    }
				    }),
			    modelDefinition);
	    }

		[TestMethod]
	    public void ModelDiscovery_Method_ResultIsBooleanExpression()
	    {
		    var modelDefinition = new Template(@"
				@{ if Object.IsTrue() }
					True
				@{ end if }
				")
			    .GetModelDefinition();

		    TemplateAssert.AreCompositeModelDefinitionsEqual(
			    new CompositeModelDefinition(
				    new Dictionary<string, IModelDefinition>
				    {
					    {
							"Object", new CompositeModelDefinition(
							    methods: new Dictionary<IMethodCallDefinition, IModelDefinition>
							    {
								    {
									    new MethodCallDefinition("IsTrue", Array.Empty<IMethodArgumentDefinition>()),
									    new PrimitiveModelDefinition(TypeDefinition.Boolean)
								    }
							    })
					    }
				    }),
			    modelDefinition);
		}

	    [TestMethod]
	    public void ModelDiscovery_Method_ResultIsStringExpression()
	    {
		    var modelDefinition = new Template(@"
				@{ if Recipient.GetName() != 'Cara' }
					True
				@{ end if }
				")
			    .GetModelDefinition();

		    TemplateAssert.AreCompositeModelDefinitionsEqual(
			    new CompositeModelDefinition(
				    new Dictionary<string, IModelDefinition>
				    {
					    {
						    "Recipient", new CompositeModelDefinition(
							    methods: new Dictionary<IMethodCallDefinition, IModelDefinition>
							    {
								    {
									    new MethodCallDefinition("GetName", Array.Empty<IMethodArgumentDefinition>()),
									    new PrimitiveModelDefinition(TypeDefinition.String)
								    }
							    })
					    }
				    }),
			    modelDefinition);
		}

	    [TestMethod]
	    public void ModelDiscovery_Method_ResultIsComposite()
	    {
		    var modelDefinition = new Template(
				@"${ Root.GetObject().Id }")
			    .GetModelDefinition();

		    TemplateAssert.AreCompositeModelDefinitionsEqual(
			    new CompositeModelDefinition(
				    new Dictionary<string, IModelDefinition>
				    {
					    {
						    "Root", new CompositeModelDefinition(
							    methods: new Dictionary<IMethodCallDefinition, IModelDefinition>
							    {
								    {
									    new MethodCallDefinition("GetObject", Array.Empty<IMethodArgumentDefinition>()),
									    new CompositeModelDefinition(
										    new Dictionary<string, IModelDefinition>
										    {
											    { "Id", new PrimitiveModelDefinition(TypeDefinition.Primitive) }
										    })
								    }
							    })
					    }
				    }),
			    modelDefinition);
	    }

		[TestMethod]
	    public void ModelDiscovery_Method_ResultIsComparedToNull()
	    {
		    var modelDefinition = new Template(@"
				@{ if Object.GetValue() != null }
					True
				@{ end if }
				")
			    .GetModelDefinition();

		    TemplateAssert.AreCompositeModelDefinitionsEqual(
			    new CompositeModelDefinition(
				    new Dictionary<string, IModelDefinition>
				    {
					    {
						    "Object", new CompositeModelDefinition(
							    methods: new Dictionary<IMethodCallDefinition, IModelDefinition>
							    {
								    {
									    new MethodCallDefinition("GetValue", Array.Empty<IMethodArgumentDefinition>()),
									    new PrimitiveModelDefinition(TypeDefinition.Unknown)
								    }
							    })
					    }
				    }),
			    modelDefinition);
		}

		[TestMethod]
	    public void ModelDiscovery_Method_MultipleCallsWithSameParameters()
	    {
		    var modelDefinition = new Template(@"
					${ Items.GetByIndex(5).Value }
					${ Items.GetByIndex(5).Key }
				")
			    .GetModelDefinition();

		    TemplateAssert.AreCompositeModelDefinitionsEqual(
			    new CompositeModelDefinition(
				    new Dictionary<string, IModelDefinition>
				    {
					    {
						    "Items", new CompositeModelDefinition(
							    methods: new Dictionary<IMethodCallDefinition, IModelDefinition>
							    {
								    {
									    new MethodCallDefinition(
										    "GetByIndex",
										    new[]
										    {
											    new MethodArgumentDefinition(TypeDefinition.Integer, 5)
										    }),
									    new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
									    {
											{ "Value", new PrimitiveModelDefinition(TypeDefinition.Primitive) },
											{ "Key", new PrimitiveModelDefinition(TypeDefinition.Primitive) }
									    })
								    }
							    })
					    }
				    }),
			    modelDefinition);
	    }
	    [TestMethod]
	    public void ModelDiscovery_Method_MultipleCallsWithDifferentParameters()
	    {
		    var modelDefinition = new Template(@"
					${ Items.GetByIndex(5).Value }
					${ Items.GetByIndex(5).Key }
				")
			    .GetModelDefinition();

		    TemplateAssert.AreCompositeModelDefinitionsEqual(
			    new CompositeModelDefinition(
				    new Dictionary<string, IModelDefinition>
				    {
					    {
						    "Items", new CompositeModelDefinition(
							    methods: new Dictionary<IMethodCallDefinition, IModelDefinition>
							    {
								    {
									    new MethodCallDefinition(
										    "GetByIndex",
										    new[]
										    {
											    new MethodArgumentDefinition(TypeDefinition.Integer, 5)
										    }),
									    new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
									    {
										    { "Value", new PrimitiveModelDefinition(TypeDefinition.Primitive) },
										    { "Key", new PrimitiveModelDefinition(TypeDefinition.Primitive) }
									    })
								    }
							    })
					    }
				    }),
			    modelDefinition);
	    }

	    private class IntegerIdentityFunction : ScalarTemplateFunction<int, int>
	    {
		    public IntegerIdentityFunction()
			    : base(
				    "IntegerIdentity",
				    new IntegerFunctionArgument("intValue"))
		    {
		    }

		    public override int Invoke(int value)
		    {
			    return value;
		    }
	    }
	}
}
