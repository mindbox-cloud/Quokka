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

using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mindbox.Quokka.Tests
{
	[TestClass]
	public class ModelDefinitionCombiningTests
	{
		[TestMethod]
		public void DefinitionCombining_SingleModel()
		{
			var definition = new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
			{
				{ "Primitive", new PrimitiveModelDefinition(TypeDefinition.Integer) },
				{
					"Composite", new CompositeModelDefinition(
						new Dictionary<string, IModelDefinition>
						{
							{
								"A",
								new PrimitiveModelDefinition(TypeDefinition.Boolean)
							},
							{
								"B",
								new PrimitiveModelDefinition(TypeDefinition.String)
							}
						})
				},
				{
					"Array", new ArrayModelDefinition(new PrimitiveModelDefinition(TypeDefinition.Boolean))
				}
			});

			var combinedDefinition = new DefaultTemplateFactory().CombineModelDefinition(new[] { definition });

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				definition,
				combinedDefinition);
		}

		[TestMethod]
		public void DefinitionCombining_TwoNonIntersecting_Models()
		{
			var definition1 = new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
			{
				{ "Primitive1", new PrimitiveModelDefinition(TypeDefinition.Integer) },
				{ "Primitive2", new PrimitiveModelDefinition(TypeDefinition.Decimal) }
			});

			var definition2 = new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
			{
				{ "Primitive3", new PrimitiveModelDefinition(TypeDefinition.Integer) },
				{ "Primitive4", new PrimitiveModelDefinition(TypeDefinition.Decimal) }
			});

			var combinedDefinition = new DefaultTemplateFactory().CombineModelDefinition(new[] { definition1, definition2 });

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{ "Primitive1", new PrimitiveModelDefinition(TypeDefinition.Integer) },
					{ "Primitive2", new PrimitiveModelDefinition(TypeDefinition.Decimal) },
					{ "Primitive3", new PrimitiveModelDefinition(TypeDefinition.Integer) },
					{ "Primitive4", new PrimitiveModelDefinition(TypeDefinition.Decimal) }
				}),
				combinedDefinition);
		}

		[TestMethod]
		public void DefinitionCombining_TwoNonIntersecting_SubModels()
		{
			var definition1 = new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
			{
				{
					"Composite", new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
					{
						{ "A", new PrimitiveModelDefinition(TypeDefinition.Integer) },
						{ "B", new PrimitiveModelDefinition(TypeDefinition.Decimal) },
					})
				}
			});

			var definition2 = new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
			{
				{
					"Composite", new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
					{
						{ "C", new PrimitiveModelDefinition(TypeDefinition.Integer) },
						{ "D", new PrimitiveModelDefinition(TypeDefinition.Decimal) },
					})
				}
			});


			var combinedDefinition = new DefaultTemplateFactory().CombineModelDefinition(new[] { definition1, definition2 });

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{
						"Composite", new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
						{
							{ "A", new PrimitiveModelDefinition(TypeDefinition.Integer) },
							{ "B", new PrimitiveModelDefinition(TypeDefinition.Decimal) },
							{ "C", new PrimitiveModelDefinition(TypeDefinition.Integer) },
							{ "D", new PrimitiveModelDefinition(TypeDefinition.Decimal) },
						})
					}
				}),
				combinedDefinition);
		}

		[TestMethod]
		public void DefinitionCombining_TwoNonIntersecting_ArrayCompositeModels()
		{
			var definition1 = new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
			{
				{
					"Array", new ArrayModelDefinition(
						new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
						{

							{ "A", new PrimitiveModelDefinition(TypeDefinition.Integer) },
							{ "B", new PrimitiveModelDefinition(TypeDefinition.Decimal) }
						}))
				}
			});

			var definition2 = new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
			{
				{
					"Array", new ArrayModelDefinition(
						new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
						{

							{ "C", new PrimitiveModelDefinition(TypeDefinition.Integer) },
							{ "D", new PrimitiveModelDefinition(TypeDefinition.Decimal) }
						}))
				}
			});


			var combinedDefinition = new DefaultTemplateFactory().CombineModelDefinition(new[] { definition1, definition2 });

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{
						"Array", new ArrayModelDefinition(
							new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
							{

								{ "A", new PrimitiveModelDefinition(TypeDefinition.Integer) },
								{ "B", new PrimitiveModelDefinition(TypeDefinition.Decimal) },
								{ "C", new PrimitiveModelDefinition(TypeDefinition.Integer) },
								{ "D", new PrimitiveModelDefinition(TypeDefinition.Decimal) }
							}))
					}
				}),
				combinedDefinition);
		}

		[TestMethod]
		public void DefinitionCombining_TwoIntersecting_CompositeSubModels()
		{
			var definition1 = new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
			{
				{
					"Composite", new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
					{
						{ "A", new PrimitiveModelDefinition(TypeDefinition.Integer) },
						{ "B", new PrimitiveModelDefinition(TypeDefinition.Decimal) },
					})
				}
			});

			var definition2 = new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
			{
				{
					"Composite", new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
					{
						{ "A", new PrimitiveModelDefinition(TypeDefinition.Integer) },
						{ "D", new PrimitiveModelDefinition(TypeDefinition.Decimal) },
					})
				}
			});

			var combinedDefinition = new DefaultTemplateFactory().CombineModelDefinition(new[] { definition1, definition2 });

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{
						"Composite", new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
						{
							{ "A", new PrimitiveModelDefinition(TypeDefinition.Integer) },
							{ "B", new PrimitiveModelDefinition(TypeDefinition.Decimal) },
							{ "D", new PrimitiveModelDefinition(TypeDefinition.Decimal) },
						})
					}
				}),
				combinedDefinition);
		}

		[TestMethod]
		public void DefinitionCombining_TwoIntersecting_Models_InconsistentTyping_Errors()
		{
			var definition1 = new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
			{
				{ "Primitive1", new PrimitiveModelDefinition(TypeDefinition.Integer) }
			});

			var definition2 = new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
			{
				{ "Primitive1", new PrimitiveModelDefinition(TypeDefinition.String) }
			});

			IList<ITemplateError> errors;
			var combinedDefinition =
				new DefaultTemplateFactory().TryCombineModelDefinition(new[] { definition1, definition2 },
					out errors);

			Assert.IsNull(combinedDefinition);
			Assert.IsTrue(errors.Single().Message.Contains("Primitive1"));
		}

		[TestMethod]
		public void DefinitionCombining_TwoIntersecting_Models_CompatibleTypes()
		{
			var definition1 = new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
			{
				{ "Primitive1", new PrimitiveModelDefinition(TypeDefinition.Primitive) }
			});

			var definition2 = new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
			{
				{ "Primitive1", new PrimitiveModelDefinition(TypeDefinition.Integer) }
			});
			
			var combinedDefinition = new DefaultTemplateFactory().CombineModelDefinition(new[] { definition1, definition2 });

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{ "Primitive1", new PrimitiveModelDefinition(TypeDefinition.Integer) }
				}),
				combinedDefinition);
		}

		[TestMethod]
		public void DefinitionCombining_TwoNonIntersecting_CompositeWithMethods()
		{
			var definition1 = new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
			{
				{
					"Composite", new CompositeModelDefinition(methods: new Dictionary<IMethodCallDefinition, IModelDefinition>
					{
						{
							new MethodCallDefinition("MultiplyBy", new IMethodArgumentDefinition[]
							{
								new MethodArgumentDefinition(TypeDefinition.Integer, 4)
							}),
							new PrimitiveModelDefinition(TypeDefinition.Integer)
						}
					})
				}
			});

			var definition2 = new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
			{
				{
					"Composite", new CompositeModelDefinition(methods: new Dictionary<IMethodCallDefinition, IModelDefinition>
					{
						{
							new MethodCallDefinition("DivideBy", new IMethodArgumentDefinition[]
							{
								new MethodArgumentDefinition(TypeDefinition.Integer, 7)
							}),
							new PrimitiveModelDefinition(TypeDefinition.Integer)
						}
					})
				}
			});

			var combinedDefinition = new DefaultTemplateFactory().CombineModelDefinition(new[] { definition1, definition2 });

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(
					new Dictionary<string, IModelDefinition>
					{
						{
							"Composite", new CompositeModelDefinition(
								methods: new Dictionary<IMethodCallDefinition, IModelDefinition>

								{
									{
										new MethodCallDefinition(
											"MultiplyBy",
											new IMethodArgumentDefinition[]
											{
												new MethodArgumentDefinition(TypeDefinition.Integer, 4)
											}),
										new PrimitiveModelDefinition(TypeDefinition.Integer)
									},
									{
										new MethodCallDefinition(
											"DivideBy",
											new IMethodArgumentDefinition[]
											{
												new MethodArgumentDefinition(TypeDefinition.Integer, 7)
											}),
										new PrimitiveModelDefinition(TypeDefinition.Integer)
									}
								}
							)
						}
					}),
				combinedDefinition);
		}

		[TestMethod]
		public void DefinitionCombining_TwoIntersecting_CompositeWithMethods()
		{
			var definition1 = new CompositeModelDefinition(
				new Dictionary<string, IModelDefinition>
				{
					{
						"Composite", new CompositeModelDefinition(
							methods: new Dictionary<IMethodCallDefinition, IModelDefinition>
							{
								{
									new MethodCallDefinition(
										"Take",
										new IMethodArgumentDefinition[]
										{
											new MethodArgumentDefinition(TypeDefinition.Integer, 3)
										}),
									new CompositeModelDefinition(
										new Dictionary<string, IModelDefinition>
										{
											{ "A", new PrimitiveModelDefinition(TypeDefinition.Boolean) },
											{ "B", new PrimitiveModelDefinition(TypeDefinition.Decimal) }
										})
								}
							})
					}
				});

			var definition2 = new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
			{
				{
					"Composite", new CompositeModelDefinition(methods: new Dictionary<IMethodCallDefinition, IModelDefinition>
					{
						{
							new MethodCallDefinition(
								"Take",
								new IMethodArgumentDefinition[]
								{
									new MethodArgumentDefinition(TypeDefinition.Integer, 3)
								}),
							new CompositeModelDefinition(
								new Dictionary<string, IModelDefinition>
								{
									{ "C", new PrimitiveModelDefinition(TypeDefinition.DateTime) },
									{ "D", new PrimitiveModelDefinition(TypeDefinition.Integer) }
								})
						}
					})
				}
			});

			var combinedDefinition = new DefaultTemplateFactory().CombineModelDefinition(new[] { definition1, definition2 });

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(
					new Dictionary<string, IModelDefinition>
					{
						{
							"Composite", new CompositeModelDefinition(
								methods: new Dictionary<IMethodCallDefinition, IModelDefinition>

								{
									{
										new MethodCallDefinition(
											"Take",
											new IMethodArgumentDefinition[]
											{
												new MethodArgumentDefinition(TypeDefinition.Integer, 3)
											}),
										new CompositeModelDefinition(
											new Dictionary<string, IModelDefinition>
											{
												{ "A", new PrimitiveModelDefinition(TypeDefinition.Boolean) },
												{ "B", new PrimitiveModelDefinition(TypeDefinition.Decimal) },
												{ "C", new PrimitiveModelDefinition(TypeDefinition.DateTime) },
												{ "D", new PrimitiveModelDefinition(TypeDefinition.Integer) }
											})
									}
								}
							)
						}
					}),
				combinedDefinition);
		}

		[TestMethod]
		public void DefinitionCombining_ArrayOfUnknowns_MergeWithArrayOfComposites()
		{
			var definition1 = new CompositeModelDefinition(
				new Dictionary<string, IModelDefinition>
				{
					{
						"Array",
						new ArrayModelDefinition(new PrimitiveModelDefinition(TypeDefinition.Unknown))
					}
				});

			var definition2 = new CompositeModelDefinition(
				new Dictionary<string, IModelDefinition>
				{
					{
						"Array",
						new ArrayModelDefinition(
							new CompositeModelDefinition(
								new Dictionary<string, IModelDefinition>
								{
									{ "Name", new PrimitiveModelDefinition(TypeDefinition.String) }
								}))
					}
				});

			var combinedDefinition = new DefaultTemplateFactory().CombineModelDefinition(new[] { definition1, definition2 });

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(
					new Dictionary<string, IModelDefinition>
					{
						{
							"Array",
							new ArrayModelDefinition(
								new CompositeModelDefinition(
									new Dictionary<string, IModelDefinition>
									{
										{ "Name", new PrimitiveModelDefinition(TypeDefinition.String) }
									}))
						}
					}),
				combinedDefinition);
		}
		
				
		[TestMethod]
		public void DefinitionCombining_ArrayOfComposites_MergeWithComposite()
		{
			var definition1 = new CompositeModelDefinition(
				new Dictionary<string, IModelDefinition>
				{
					{
						"Array",
						new ArrayModelDefinition(
							new CompositeModelDefinition(
								new Dictionary<string, IModelDefinition>
								{
									{ "Name", new PrimitiveModelDefinition(TypeDefinition.String) }
								}))
					}
				});

			var definition2 = new CompositeModelDefinition(
				new Dictionary<string, IModelDefinition>
				{
					{
						"Array",
						new CompositeModelDefinition(
							new Dictionary<string, IModelDefinition>
							{
								{ "CompositeField", new PrimitiveModelDefinition(TypeDefinition.String) }
							},
							new Dictionary<IMethodCallDefinition, IModelDefinition>
							{
								{
									new MethodCallDefinition(
										"Take",
										new IMethodArgumentDefinition[]
										{
											new MethodArgumentDefinition(TypeDefinition.Integer, 3)
										}),
									new CompositeModelDefinition(
										new Dictionary<string, IModelDefinition>
										{
											{ "A", new PrimitiveModelDefinition(TypeDefinition.Boolean) },
											{ "B", new PrimitiveModelDefinition(TypeDefinition.Decimal) }
										})
								}
							})
					}
				});

			var combinedDefinition = new DefaultTemplateFactory().CombineModelDefinition(new[] { definition1, definition2 });

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(
					new Dictionary<string, IModelDefinition>
					{
						{
							"Array",
							new ArrayModelDefinition(
								new CompositeModelDefinition(
									new Dictionary<string, IModelDefinition>
									{
										{ "Name", new PrimitiveModelDefinition(TypeDefinition.String) }
									}),
								new Dictionary<string, IModelDefinition>
								{
									{
										"Array",
										new CompositeModelDefinition(
											new Dictionary<string, IModelDefinition>
											{
												{ "CompositeField", new PrimitiveModelDefinition(TypeDefinition.String) }
											})
									}
								},
								new Dictionary<IMethodCallDefinition, IModelDefinition>
								{
									{
										new MethodCallDefinition(
											"Take",
											new IMethodArgumentDefinition[]
											{
												new MethodArgumentDefinition(TypeDefinition.Integer, 3)
											}),
										new CompositeModelDefinition(
											new Dictionary<string, IModelDefinition>
											{
												{ "A", new PrimitiveModelDefinition(TypeDefinition.Boolean) },
												{ "B", new PrimitiveModelDefinition(TypeDefinition.Decimal) }
											})
									}
								})
						}
					}),
				combinedDefinition);
		}

		
		[TestMethod]
		public void DefinitionCombining_ArrayOfComposites_MergeWithArrayOfUnknowns()
		{
			var definition1 = new CompositeModelDefinition(
				new Dictionary<string, IModelDefinition>
				{
					{
						"Array",
						new ArrayModelDefinition(
							new CompositeModelDefinition(
								new Dictionary<string, IModelDefinition>
								{
									{ "Name", new PrimitiveModelDefinition(TypeDefinition.String) }
								}))
					}
				});

			var definition2 = new CompositeModelDefinition(
				new Dictionary<string, IModelDefinition>
				{
					{
						"Array",
						new ArrayModelDefinition(new PrimitiveModelDefinition(TypeDefinition.Unknown))
					}
				});

			var combinedDefinition = new DefaultTemplateFactory().CombineModelDefinition(new[] { definition1, definition2 });

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(
					new Dictionary<string, IModelDefinition>
					{
						{
							"Array",
							new ArrayModelDefinition(
								new CompositeModelDefinition(
									new Dictionary<string, IModelDefinition>
									{
										{ "Name", new PrimitiveModelDefinition(TypeDefinition.String) }
									}))
						}
					}),
				combinedDefinition);
		}

		[TestMethod]
		public void DefinitionCombining_Unknown_MergingWithUnknown()
		{
			var definition1 = new CompositeModelDefinition(
				new Dictionary<string, IModelDefinition>
				{
					{
						"Thing",
						new PrimitiveModelDefinition(TypeDefinition.Unknown)
					}
				});

			var definition2 = new CompositeModelDefinition(
				new Dictionary<string, IModelDefinition>
				{
					{
						"Thing",
						new PrimitiveModelDefinition(TypeDefinition.Unknown)
					}
				});

			var combinedDefinition = new DefaultTemplateFactory().CombineModelDefinition(new[] { definition1, definition2 });

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(
					new Dictionary<string, IModelDefinition>
					{
						{
							"Thing",
							new PrimitiveModelDefinition(TypeDefinition.Unknown)
						}
					}),
			combinedDefinition);
		}
		
		[TestMethod]
		public void DefinitionCombining_TwoIntersectingMethodsByCaseInsensitiveArgs()
		{
			var definition1 = new CompositeModelDefinition(
				new Dictionary<string, IModelDefinition>
				{
					{
						"Composite", new CompositeModelDefinition(
							methods: new Dictionary<IMethodCallDefinition, IModelDefinition>
							{
								{
									new MethodCallDefinition(
										"Call",
										new IMethodArgumentDefinition[]
										{
											new MethodArgumentDefinition(TypeDefinition.String, "CASEINSENSITIVE")
										}),
									new CompositeModelDefinition(
										new Dictionary<string, IModelDefinition>
										{
											{ "A", new PrimitiveModelDefinition(TypeDefinition.Boolean) }
										})
								}
							})
					}
				});

			var definition2 = new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
			{
				{
					"Composite", new CompositeModelDefinition(methods: new Dictionary<IMethodCallDefinition, IModelDefinition>
					{
						{
							new MethodCallDefinition(
								"Call",
								new IMethodArgumentDefinition[]
								{
									new MethodArgumentDefinition(TypeDefinition.String, "caseinsensitive")
								}),
							new CompositeModelDefinition(
								new Dictionary<string, IModelDefinition>
								{
									{ "C", new PrimitiveModelDefinition(TypeDefinition.DateTime) }
								})
						}
					})
				}
			});

			var combinedDefinition = new DefaultTemplateFactory().CombineModelDefinition(new[] { definition1, definition2 });

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(
					new Dictionary<string, IModelDefinition>
					{
						{
							"Composite", new CompositeModelDefinition(
								methods: new Dictionary<IMethodCallDefinition, IModelDefinition>

								{
									{
										new MethodCallDefinition(
											"Call",
											new IMethodArgumentDefinition[]
											{
												new MethodArgumentDefinition(TypeDefinition.String, "CASEINSENSITIVE")
											}),
										new CompositeModelDefinition(
											new Dictionary<string, IModelDefinition>
											{
												{ "A", new PrimitiveModelDefinition(TypeDefinition.Boolean) },
												{ "C", new PrimitiveModelDefinition(TypeDefinition.DateTime) }
											})
									}
								}
							)
						}
					}),
				combinedDefinition);
		}
	}
}
