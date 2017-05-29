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
    public class ModelDiscoveryMethodsArraysTests
    {
		[TestMethod]
	    public void ModelDiscovery_MethodAsEnumerable_IterationWithoutUsages()
	    {
		    var modelDefinition = new Template(@"
				@{ for item in Object.GetElements() }
					Check.
				@{ end for }
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
									    new MethodCallDefinition("GetElements", Array.Empty<IMethodArgumentDefinition>()),
									    new ArrayModelDefinition(new PrimitiveModelDefinition(TypeDefinition.Unknown))
								    }
							    })
					    }
				    }),
			    modelDefinition);
		}

	    [TestMethod]
	    public void ModelDiscovery_MethodAsEnumerable_ItemOutput()
	    {
		    var modelDefinition = new Template(@"
				@{ for item in Object.GetElements() }
					${ item }
				@{ end for }
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
									    new MethodCallDefinition("GetElements", Array.Empty<IMethodArgumentDefinition>()),
									    new ArrayModelDefinition(new PrimitiveModelDefinition(TypeDefinition.Primitive))
								    }
							    })
					    }
				    }),
			    modelDefinition);
		}

	    [TestMethod]
	    public void ModelDiscovery_MethodAsEnumerable_ItemMethodCallOutput()
	    {
		    var modelDefinition = new Template(@"
				@{ for item in Object.GetElements() }
					${ item.ToString() }
				@{ end for }
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
									    new MethodCallDefinition("GetElements", Array.Empty<IMethodArgumentDefinition>()),
									    new ArrayModelDefinition(
										    new CompositeModelDefinition(
											    methods: new Dictionary<IMethodCallDefinition, IModelDefinition>
											    {
												    {
													    new MethodCallDefinition("ToString", Array.Empty<IMethodArgumentDefinition>()),
													    new PrimitiveModelDefinition(TypeDefinition.Primitive)
												    }
											    }))

								    }
							    })
					    }
				    }),
			    modelDefinition);
	    }

		[TestMethod]
	    public void ModelDiscovery_MethodAsEnumerable_ItemMemberOutput()
	    {
		    var modelDefinition = new Template(@"
				@{ for item in Object.GetElements() }
					${ item.Description }
				@{ end for }
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
									    new MethodCallDefinition("GetElements", Array.Empty<IMethodArgumentDefinition>()),
									    new ArrayModelDefinition(
										    new CompositeModelDefinition(
											    new Dictionary<string, IModelDefinition>
											    {
												    ["Description"] = new PrimitiveModelDefinition(TypeDefinition.Primitive)
											    },
											    null))
								    }
							    })
					    }
				    }),
			    modelDefinition);
	    }

	    [TestMethod]
	    public void ModelDiscovery_MethodAsEnumerable_MultipleEnumerations_SameMethodCall_DifferentMembers()
	    {
		    var modelDefinition = new Template(@"
				@{ for item in Object.GetElements('main', 3) }
					${ item.Description }
				@{ end for }

				@{ for x in Object.GetElements('main', 3) }
					${ x.UniqueId }
				@{ end for }
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
									    new MethodCallDefinition("GetElements", new []
									    {
										    new MethodArgumentDefinition(TypeDefinition.String, "main"), 
										    new MethodArgumentDefinition(TypeDefinition.Integer, 3) 
									    }),
									    new ArrayModelDefinition(
										    new CompositeModelDefinition(
											    new Dictionary<string, IModelDefinition>
											    {
												    ["Description"] = new PrimitiveModelDefinition(TypeDefinition.Primitive),
												    ["UniqueId"] = new PrimitiveModelDefinition(TypeDefinition.Primitive)
											    },
											    null))
								    }
							    })
					    }
				    }),
			    modelDefinition);
		}

	    [TestMethod]
	    public void ModelDiscovery_MethodChainAsEnumerable_ItemMemberOutput()
	    {
		    var modelDefinition = new Template(@"
				@{ for item in Object.GetElements().OrderBy('Id') }
					${ item.Description }
				@{ end for }
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
									    new MethodCallDefinition("GetElements", Array.Empty<IMethodArgumentDefinition>()),
									    new CompositeModelDefinition(
										    methods: new Dictionary<IMethodCallDefinition, IModelDefinition>
										    {
											    {
												    new MethodCallDefinition(
													    "OrderBy",
													    new[] { new MethodArgumentDefinition(TypeDefinition.String, "Id") }),
												    new ArrayModelDefinition(
													    new CompositeModelDefinition(
														    new Dictionary<string, IModelDefinition>
														    {
															    ["Description"] = new PrimitiveModelDefinition(TypeDefinition.Primitive)
														    },
														    null))
											    }
										    })
								    }
							    })
					    }
				    }),
			    modelDefinition);
		}

	    [TestMethod]
	    public void ModelDiscovery_MethodAsEnumerable_NestedLoopOnMethodResult()
	    {
		    var modelDefinition = new Template(@"
				@{ for item in Object.GetElements() }
					@{ for subItem in item.GetSubElements('all') }
						${ subItem.Id }
					@{ end for }
				@{ end for }
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
									    new MethodCallDefinition("GetElements", Array.Empty<IMethodArgumentDefinition>()),
									    new ArrayModelDefinition(
										    new CompositeModelDefinition(
												methods: new Dictionary<IMethodCallDefinition, IModelDefinition>
											    {
												    {
													    new MethodCallDefinition("GetSubElements", new []
													    {
														    new MethodArgumentDefinition(TypeDefinition.String, "all"),
													    }),
													    new ArrayModelDefinition(
														    new CompositeModelDefinition(
															    new Dictionary<string, IModelDefinition>
															    {
																    ["Id"] = new PrimitiveModelDefinition(TypeDefinition.Primitive)
															    },
															    null))
												    }
											    }))
								    }
							    })
					    }
				    }),
			    modelDefinition);
	    }
	}
}
