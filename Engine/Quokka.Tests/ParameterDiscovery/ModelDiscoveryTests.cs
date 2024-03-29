﻿// // Copyright 2022 Mindbox Ltd
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

using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mindbox.Quokka.Tests
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
					{ "Name", new PrimitiveModelDefinition(TypeDefinition.Primitive) }
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
					{ "Age", new PrimitiveModelDefinition(TypeDefinition.Primitive) },
					{ "FirstName", new PrimitiveModelDefinition(TypeDefinition.Primitive) },
					{ "LastName", new PrimitiveModelDefinition(TypeDefinition.Primitive) }
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
							{ "Email", new PrimitiveModelDefinition(TypeDefinition.Primitive) }
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
								new PrimitiveModelDefinition(TypeDefinition.Primitive)
							},
							{
								"MobilePhone",
								new PrimitiveModelDefinition(TypeDefinition.Primitive)
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
								new PrimitiveModelDefinition(TypeDefinition.Primitive)
							},
							{
								"Id",
								new PrimitiveModelDefinition(TypeDefinition.Primitive)
							},
							{
								"MobilePhone",
								new PrimitiveModelDefinition(TypeDefinition.Primitive)
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
									{ "Email", new PrimitiveModelDefinition(TypeDefinition.Primitive) },
									{ "MobilePhone", new PrimitiveModelDefinition(TypeDefinition.Primitive) }
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
					{ "IsDebug", new PrimitiveModelDefinition(TypeDefinition.Boolean) }
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
							{ "IsDebug", new PrimitiveModelDefinition(TypeDefinition.Boolean) },
							{ "IsStaging", new PrimitiveModelDefinition(TypeDefinition.Boolean) }
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
							{ "IsDebug", new PrimitiveModelDefinition(TypeDefinition.Boolean) },
							{ "Time", new PrimitiveModelDefinition(TypeDefinition.Primitive) }
						})
					}
				}),
				model);
		}

		[TestMethod]
		public void ModelDiscovery_IfCondition_StringComparison()
		{
			var model = new Template(@"
				@{ if City = ""Toledo"" } 
					Text
				@{ end if }
				")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{ "City", new PrimitiveModelDefinition(TypeDefinition.String) }
				}),
				model);
		}

		[TestMethod]
		public void ModelDiscovery_IfCondition_StringComparisonWithVariable()
		{
			var model = new Template(@"
				@{ if Product1 = Product2 & ""String"" }
					Test
				@{ end if }
				")
				.GetModelDefinition();
			
			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{ "Product1", new PrimitiveModelDefinition(TypeDefinition.String) },
					{ "Product2", new PrimitiveModelDefinition(TypeDefinition.Primitive) }
				}),
				model);
		}

		[TestMethod]
		public void ModelDiscovery_IfCondition_NullCheck()
		{
			var model = new Template(@"
				@{ if City != null } 
					Text
				@{ end if }
				")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{ "City", new PrimitiveModelDefinition(TypeDefinition.Unknown) }
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
					{ "ClickCount", new PrimitiveModelDefinition(TypeDefinition.Decimal) }
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
							{ "ClickCount", new PrimitiveModelDefinition(TypeDefinition.Decimal) },
							{ "SentCount", new PrimitiveModelDefinition(TypeDefinition.Decimal) }
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
									{ "Id", new PrimitiveModelDefinition(TypeDefinition.Primitive) }
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
								"Orders", new ArrayModelDefinition(
									new PrimitiveModelDefinition(TypeDefinition.Unknown))
							}
						})
					},
					{ "SomethingGlobal", new PrimitiveModelDefinition(TypeDefinition.Primitive) }
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
							{ "Date", new PrimitiveModelDefinition(TypeDefinition.Primitive) }
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
							{ "Date", new PrimitiveModelDefinition(TypeDefinition.Primitive) },
							{ "Url", new PrimitiveModelDefinition(TypeDefinition.Primitive) }
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
									{ "Name", new PrimitiveModelDefinition(TypeDefinition.Primitive) },
									{ "Price", new PrimitiveModelDefinition(TypeDefinition.Primitive) }
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
							{ "IsOpen", new PrimitiveModelDefinition(TypeDefinition.Boolean) }
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
							{ "Date", new PrimitiveModelDefinition(TypeDefinition.Primitive) },
							{ "Price", new PrimitiveModelDefinition(TypeDefinition.Primitive) }
						}))
					},
					{
						"Seasons", new ArrayModelDefinition(new PrimitiveModelDefinition(TypeDefinition.Unknown))
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
							{ "Text", new PrimitiveModelDefinition(TypeDefinition.Primitive) }
						}))
					},
					{
						"OtherOffers", new ArrayModelDefinition(new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
						{
							{ "Url", new PrimitiveModelDefinition(TypeDefinition.Primitive) }
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
							{ "Text", new PrimitiveModelDefinition(TypeDefinition.Primitive) },
							{ "Url", new PrimitiveModelDefinition(TypeDefinition.Primitive) }
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
							{ "Text", new PrimitiveModelDefinition(TypeDefinition.Primitive) },
							{ "Url", new PrimitiveModelDefinition(TypeDefinition.Primitive) }
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
					${ offer.Details.Price }
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
									{ "Price", new PrimitiveModelDefinition(TypeDefinition.Primitive) },
									{ "Url", new PrimitiveModelDefinition(TypeDefinition.Primitive) }
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
									{ "Name", new PrimitiveModelDefinition(TypeDefinition.Primitive) }
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
						"Orders", new ArrayModelDefinition(
							new ArrayModelDefinition(new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
							{
								{ "Name", new PrimitiveModelDefinition(TypeDefinition.Primitive) }
							})))
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
								"Dates", new ArrayModelDefinition(new PrimitiveModelDefinition(TypeDefinition.Primitive))
							},
							{
								"Products", new ArrayModelDefinition(new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
								{
									{ "Name", new PrimitiveModelDefinition(TypeDefinition.Primitive) }
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
									{ "Name", new PrimitiveModelDefinition(TypeDefinition.Primitive) },
									{ "Price", new PrimitiveModelDefinition(TypeDefinition.Primitive) }
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
									{ "Name", new PrimitiveModelDefinition(TypeDefinition.Primitive) },
									{ "Price", new PrimitiveModelDefinition(TypeDefinition.Primitive) }
								}))
							}
						}))
					}
				}),
				model);
		}

		[TestMethod]
		public void ModelDiscovery_StringConcatenation()
		{
			var model = new Template(@"${ composite.PropertyA & variableB & ""constant"" }")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{
						"composite", new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
						{
							{ "PropertyA", new PrimitiveModelDefinition(TypeDefinition.Primitive) }
						})
					},
					{
						"variableB", new PrimitiveModelDefinition(TypeDefinition.Primitive)
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
						"Offers", new ArrayModelDefinition(
							new PrimitiveModelDefinition(TypeDefinition.Unknown))
					}
				}),
				model);
		}

		[TestMethod]
		public void ModelDiscovery_VariableTypeDiscovery()
		{
			var model = new Template(
				"@{ set a = Recipient } @{ set b = 3 } ${ a + b }")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{ "Recipient", new PrimitiveModelDefinition(TypeDefinition.Decimal) },
				}),
				model);
		}

		[TestMethod]
		public void ModelDiscovery_RecursiveVariableDefinition()
		{
			var model = new Template(
				@"
					@{ set b = Recipient } 
					@{ set c = Recipient } 
					@{ set a = b } 
					@{ set b = c } 
					@{ set c = a } 
					${ c + a }")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{ "Recipient", new PrimitiveModelDefinition(TypeDefinition.Decimal) },
				}),
				model);
		}

		[TestMethod]
		public void ModelDiscovery_MemberValueAssignment()
		{
			var model = new Template(
				@"
					@{ set b = Recipient.Age } 
					${ b + 5 }")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{ "Recipient",
						new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
						{
							{ "Age", new PrimitiveModelDefinition(TypeDefinition.Decimal) }
						})
					}
				}),
				model);
		}

		[TestMethod]
		public void ModelDiscovery_MethodResultAssignment()
		{
			var model = new Template(
				@"
					@{ set b = Recipient.GetAge() } 
					${ b + 5 }")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(new Dictionary<string, IModelDefinition>
				{
					{ "Recipient",
						new CompositeModelDefinition(
							new Dictionary<string, IModelDefinition> { },
							new Dictionary<IMethodCallDefinition, IModelDefinition>
								{ { new MethodCallDefinition("GetAge", new List<MethodArgumentDefinition>()),
									new PrimitiveModelDefinition(TypeDefinition.Decimal) } }
						)
					}
				}),
				model);
		}

		[TestMethod]
		public void ModelDiscovery_ForLoop_ArrayContainsFieldsAndMethods()
		{
			var model = new Template(@"
				@{ for element in Offers } 
					${ element.Date } 
				@{ end for }

				${ Offers.Count }
				${ Offers.GetNonEmptyCount() + 10 }
				")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(
					new Dictionary<string, IModelDefinition>
					{
						{
							"Offers",
							new ArrayModelDefinition(
								new CompositeModelDefinition(
									new Dictionary<string, IModelDefinition>
									{
										{ "Date", new PrimitiveModelDefinition(TypeDefinition.Primitive) }
									}),
								fields: new Dictionary<string, IModelDefinition>
								{
									{ "Count", new PrimitiveModelDefinition(TypeDefinition.Primitive) }
								},
								methods: new Dictionary<IMethodCallDefinition, IModelDefinition>
								{
									{
										new MethodCallDefinition("GetNonEmptyCount", Array.Empty<IMethodArgumentDefinition>()),
										new PrimitiveModelDefinition(TypeDefinition.Decimal)
									}
								})
						}
					}),
				model);
		}
	}
}
