﻿using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Quokka.Tests
{
	[TestClass]
	public class ModelDiscoveryTests
	{
		[TestMethod]
		public void ModelDiscovery_EmptyTemplate()
		{
			var parameterDefinition = new Template("")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				CompositeModelDefinition.Empty,
				parameterDefinition);
		}

		[TestMethod]
		public void ModelDiscovery_SingleOutputParameter()
		{
			var model = new Template(
				"${ Name }")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{ "Name", new PrimitiveModelDefinition(VariableType.Primitive) }
				}),
				model);
		}

		[TestMethod]
		public void ModelDiscovery_MultipleOutputParameters()
		{
			var model = new Template(
				"${ FirstName } ${ LastName } ${ Age }")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{ "Age", new PrimitiveModelDefinition(VariableType.Primitive) },
					{ "FirstName", new PrimitiveModelDefinition(VariableType.Primitive) },
					{ "LastName", new PrimitiveModelDefinition(VariableType.Primitive) }
				}),
				model);
		}

		[TestMethod]
		public void ModelDiscovery_CompositeParameter()
		{
			var model = new Template(
				"${ Customer.Email }")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{
						"Customer", new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
						{
							{ "Email", new PrimitiveModelDefinition(VariableType.Primitive) }
						})
					}
				}),
				model);
		}

		[TestMethod]
		public void ModelDiscovery_CompositeParameterWithTwoFields()
		{
			var model = new Template(
				"${ Customer.Email } ${ Customer.MobilePhone }")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{
						"Customer",
						new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
						{
							{
								"Email",
								new PrimitiveModelDefinition(VariableType.Primitive)
							},
							{
								"MobilePhone",
								new PrimitiveModelDefinition(VariableType.Primitive)
							}
						})
					}
				}),
				model);
		}

		[TestMethod]
		public void ModelDiscovery_CompositeParameter_CaseInsensitive()
		{
			var model = new Template(
				"${ Customer.Email } ${ CUSTOMER.MobilePhone } ${ cUsToMeR.Id }")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{
						"Customer",
						new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
						{
							{
								"Email",
								new PrimitiveModelDefinition(VariableType.Primitive)
							},
							{
								"Id",
								new PrimitiveModelDefinition(VariableType.Primitive)
							},
							{
								"MobilePhone",
								new PrimitiveModelDefinition(VariableType.Primitive)
							}
						})
					}
				}),
				model);
		}

		[TestMethod]
		public void ModelDiscovery_SecondLevelCompositeParameterWithTwoFields()
		{
			var model = new Template(
				"${ Customer.Contacts.Email } ${ Customer.Contacts.MobilePhone }")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{
						"Customer", new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
						{
							{
								"Contacts", new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
								{
									{ "Email", new PrimitiveModelDefinition(VariableType.Primitive) },
									{ "MobilePhone", new PrimitiveModelDefinition(VariableType.Primitive) }
								})
							}
						})
					}
				}),
				model);
		}


		[TestMethod]
		public void ModelDiscovery_IfCondition_FirstLevelParameter()
		{
			var model = new Template(
				@"@{ if IsDebug } 
					Text
				@{ end if }")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{ "IsDebug", new PrimitiveModelDefinition(VariableType.Boolean) }
				}),
				model);
		}

		[TestMethod]
		public void ModelDiscovery_IfCondition_SecondLevelParameters()
		{
			var model = new Template(@"
				@{ if Context.IsDebug or Context.IsStaging } 
					Text
				@{ end if }
				")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{
						"Context", new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
						{
							{ "IsDebug", new PrimitiveModelDefinition(VariableType.Boolean) },
							{ "IsStaging", new PrimitiveModelDefinition(VariableType.Boolean) }
						})
					}
				}),
				model);
		}


		[TestMethod]
		public void ModelDiscovery_IfCondition_SecondLevelParameter_AnotherFieldOnFirstLevel()
		{
			var model = new Template(@"
				${ Context.Time }

				@{ if context.IsDebug } 
					Text
				@{ end if }
				")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{
						"Context", new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
						{
							{ "IsDebug", new PrimitiveModelDefinition(VariableType.Boolean) },
							{ "Time", new PrimitiveModelDefinition(VariableType.Primitive) }
						})
					}
				}),
				model);
		}



		[TestMethod]
		public void ModelDiscovery_ArithmeticExpression_FirstLevelParameter()
		{
			var model = new Template(
				"${ ClickCount * 2 }")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{ "ClickCount", new PrimitiveModelDefinition(VariableType.Integer) }
				}),
				model);
		}


		[TestMethod]
		public void ModelDiscovery_ArithmeticExpression_SecondLevelParameters()
		{
			var model = new Template(
				"${ 100 * Statistics.ClickCount / Statistics.SentCount }")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{
						"Statistics", new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
						{
							{ "ClickCount", new PrimitiveModelDefinition(VariableType.Integer) },
							{ "SentCount", new PrimitiveModelDefinition(VariableType.Integer) }
						})
					}
				}),
				model);
		}


		[TestMethod]
		public void ModelDiscovery_ForLoop_SecondLevelParameterAsCollection()
		{
			var model = new Template(@"
				@{ for element in Customer.Orders } 
					${ element.Id }
				@{ end for }
				")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{
						"Customer", new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
						{
							{
								"Orders", new ArrayModelDefinition(new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
								{
									{ "Id", new PrimitiveModelDefinition(VariableType.Primitive) }
								}))
							}
						})
					}
				}),
				model);
		}

		[TestMethod]
		public void ModelDiscovery_ForLoop_GlobalParameterInsideTheLoop()
		{
			var model = new Template(@"
				@{ for element in Customer.Orders } 
					${ SomethingGlobal }
				@{ end for }
				")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{
						"Customer", new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
						{
							{
								"Orders", new ArrayModelDefinition(CompositeModelDefinition.Empty)
							}
						})
					},
					{ "SomethingGlobal", new PrimitiveModelDefinition(VariableType.Primitive) }
				}),
				model);
		}

		[TestMethod]
		public void ModelDiscovery_ForLoop_ElementFirstLevelField()
		{
			var model = new Template(@"
				@{ for element in Offers } 
					${ element.Date }
				@{ end for }
				")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{
						"Offers", new ArrayModelDefinition(new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
						{
							{ "Date", new PrimitiveModelDefinition(VariableType.Primitive) }
						}))
					}
				}),
				model);
		}

		[TestMethod]
		public void ModelDiscovery_ForLoop_MultipleElementFirstLevelFields()
		{
			var model = new Template(@"
				@{ for element in Offers } 
					${ element.Date }
					${ element.Url }
				@{ end for }
				")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{
						"Offers", new ArrayModelDefinition(new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
						{
							{ "Date", new PrimitiveModelDefinition(VariableType.Primitive) },
							{ "Url", new PrimitiveModelDefinition(VariableType.Primitive) }
						}))
					}
				}),
				model);
		}

		[TestMethod]
		public void ModelDiscovery_ForLoop_MultipleElementSecondLevelFields()
		{
			var model = new Template(@"
				@{ for element in Offers } 
					${ element.Product.Price }
					${ element.Product.Name }
				@{ end for }
				")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{
						"Offers", new ArrayModelDefinition(new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
						{
							{
								"Product", new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
								{
									{ "Name", new PrimitiveModelDefinition(VariableType.Primitive) },
									{ "Price", new PrimitiveModelDefinition(VariableType.Primitive) }
								})
							}
						}))
					}
				}),
				model);
		}

		[TestMethod]
		public void ModelDiscovery_ForLoop_IfConditionOnElementVariable()
		{
			var model = new Template(@"
				@{ for element in Offers } 
					@{ if element.IsOpen }
						Try our offer
					@{ end if }
				@{ end for }
				")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{
						"Offers", new ArrayModelDefinition(new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
						{
							{ "IsOpen", new PrimitiveModelDefinition(VariableType.Boolean) }
						}))
					}
				}),
				model);
		}

		[TestMethod]
		public void ModelDiscovery_NestedForLoops_UsingFieldFromOuterForElement()
		{
			var model = new Template(@"
				@{ for offer in Offers } 
					${ offer.Date }

					@{ for season in Seasons } 
						${ offer.Price }
					@{ end for }
				@{ end for }
				")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{
						"Offers", new ArrayModelDefinition(new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
						{
							{ "Date", new PrimitiveModelDefinition(VariableType.Primitive) },
							{ "Price", new PrimitiveModelDefinition(VariableType.Primitive) }
						}))
					},
					{
						"Seasons", new ArrayModelDefinition(CompositeModelDefinition.Empty)
					}
				}),
				model);
		}

		[TestMethod]
		public void ModelDiscovery_AdjacentForLoops_SameElementVariableNameForDifferentCollections()
		{
			var model = new Template(@"
				@{ for offer in Offers } 
					${ offer.Text }
				@{ end for }

				@{ for offer in OtherOffers } 
					${ offer.Url }
				@{ end for }
				")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{
						"Offers", new ArrayModelDefinition(new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
						{
							{ "Text", new PrimitiveModelDefinition(VariableType.Primitive) }
						}))
					},
					{
						"OtherOffers", new ArrayModelDefinition(new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
						{
							{ "Url", new PrimitiveModelDefinition(VariableType.Primitive) }
						}))
					}
				}),
				model);
		}

		[TestMethod]
		public void ModelDiscovery_AdjacentForLoops_SameElementVariableNameForSameCollection()
		{
			var model = new Template(@"
				@{ for offer in Offers } 
					${ offer.Text }
				@{ end for }

				@{ for offer in Offers } 
					${ offer.Url }
				@{ end for }
				")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{
						"Offers", new ArrayModelDefinition(new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
						{
							{ "Text", new PrimitiveModelDefinition(VariableType.Primitive) },
							{ "Url", new PrimitiveModelDefinition(VariableType.Primitive) }
						}))
					}
				}),
				model);
		}

		[TestMethod]
		public void ModelDiscovery_AdjacentForLoops_DifferentElementVariableNameForSameCollection()
		{
			var model = new Template(@"
				@{ for offer in Offers } 
					${ offer.Text }
				@{ end for }

				@{ for myOffer in Offers } 
					${ myOffer.Url }
				@{ end for }
				")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{
						"Offers", new ArrayModelDefinition(new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
						{
							{ "Text", new PrimitiveModelDefinition(VariableType.Primitive) },
							{ "Url", new PrimitiveModelDefinition(VariableType.Primitive) }
						}))
					}
				}),
				model);
		}

		[TestMethod]
		public void ModelDiscovery_AdjacentForLoops_SecondLevelElementFieldsForSameCollection()
		{
			var model = new Template(@"
				@{ for offer in Offers } 
					${$ offer.Details.Price }
				@{ end for }

				@{ for myOffer in Offers } 
					${ myOffer.Details.Url }
				@{ end for }
				")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{
						"Offers", new ArrayModelDefinition(new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
						{
							{
								"Details", new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
								{
									{ "Price", new PrimitiveModelDefinition(VariableType.Primitive) },
									{ "Url", new PrimitiveModelDefinition(VariableType.Primitive) }
								})
							}
						}))
					}
				}),
				model);
		}

		[TestMethod]
		public void ModelDiscovery_NestedForLoops_IterationOverCollectionElement()
		{
			var model = new Template(@"
				@{ for order in Orders } 
					@{ for product in order.Products }
						${ product.Name }
					@{ end for }
				@{ end for }
				")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{
						"Orders", new ArrayModelDefinition(new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
						{
							{
								"Products", new ArrayModelDefinition(new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
								{
									{ "Name", new PrimitiveModelDefinition(VariableType.Primitive) }
								}))
							}
						}))
					}
				}),
				model);
		}
		
		[TestMethod]
		public void ModelDiscovery_NestedForLoops_IterationOverCollectionElementItself()
		{
			var model = new Template(@"
				@{ for order in Orders } 
					@{ for product in order }
						${ product.Name }
					@{ end for }
				@{ end for }
				")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{
						"Orders", new ArrayModelDefinition(new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
						{
							{
								"Products", new ArrayModelDefinition(new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
								{
									{ "Name", new PrimitiveModelDefinition(VariableType.Primitive) }
								}))
							}
						}))
					}
				}),
				model);
		}

		[TestMethod]
		public void ModelDiscovery_NestedForLoops_MultipleIterationsOverDifferentCollectionElements()
		{
			var model = new Template(@"
				@{ for order in Orders } 
					@{ for product in order.Products }
						${ product.Name }
					@{ end for }

					@{ for date in order.Dates }
						${ date }
					@{ end for }
				@{ end for }
				")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{
						"Orders", new ArrayModelDefinition(new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
						{
							{
								"Dates", new ArrayModelDefinition(CompositeModelDefinition.Empty)
							},
							{
								"Products", new ArrayModelDefinition(new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
								{
									{ "Name", new PrimitiveModelDefinition(VariableType.Primitive) }
								}))
							}
						}))
					}
				}),
				model);
		}

		[TestMethod]
		public void ModelDiscovery_NestedForLoops_MultipleIterationsOverSameCollectionElements_SameName()
		{
			var model = new Template(@"
				@{ for order in Orders } 
					@{ for product in order.Products }
						${ product.Name }
					@{ end for }

					@{ for product in order.Products }
						${ product.Price }
					@{ end for }
				@{ end for }
				")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{
						"Orders", new ArrayModelDefinition(new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
						{
							{
								"Products", new ArrayModelDefinition(new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
								{
									{ "Name", new PrimitiveModelDefinition(VariableType.Primitive) },
									{ "Price", new PrimitiveModelDefinition(VariableType.Primitive) }
								}))
							}
						}))
					}
				}),
				model);
		}

		[TestMethod]
		public void ModelDiscovery_NestedForLoops_MultipleIterationsOverSameCollectionElements_DifferentNames()
		{
			var model = new Template(@"
				@{ for order in Orders } 
					@{ for product in order.Products }
						${ product.Name }
					@{ end for }

					@{ for product2 in order.Products }
						${ product2.Price }
					@{ end for }
				@{ end for }
				")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{
						"Orders", new ArrayModelDefinition(new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
						{
							{
								"Products", new ArrayModelDefinition(new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
								{
									{ "Name", new PrimitiveModelDefinition(VariableType.Primitive) },
									{ "Price", new PrimitiveModelDefinition(VariableType.Primitive) }
								}))
							}
						}))
					}
				}),
				model);
		}

		[TestMethod]
		public void ModelDiscovery_ForLoop_CollectionElementWithoutUsages()
		{
			var model = new Template(@"
				@{ for element in Offers } 
					Text
				@{ end for }
				")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{
						"Offers", new ArrayModelDefinition(CompositeModelDefinition.Empty)
					}
				}),
				model);
		}
	}
}