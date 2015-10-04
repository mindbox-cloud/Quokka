using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Quokka.Tests
{
	[TestClass]
	public class ParameterDiscoveryTests
	{
		[TestMethod]
		public void ParameterDiscovery_SingleOutputParameter()
		{
			var parameterDefinitions = new Template(
				"${ Name }")
				.GetParameterDefinitions();

			TemplateAssert.AreParameterDefinitionsEqual(
				new[]
				{
					new ParameterDefinition(
						"Name",
						VariableType.Primitive)
				},
				parameterDefinitions);
		}

		[TestMethod]
		public void ParameterDiscovery_MultipleOutputParameters()
		{
			var parameterDefinitions = new Template(
				"${ FirstName } ${ LastName } ${ Age }")
				.GetParameterDefinitions();

			TemplateAssert.AreParameterDefinitionsEqual(
				new[]
				{
					new ParameterDefinition(
						"Age",
						VariableType.Primitive),
					new ParameterDefinition(
						"FirstName",
						VariableType.Primitive),
                    new ParameterDefinition(
						"LastName",
						VariableType.Primitive)
				},
				parameterDefinitions);
		}

		[TestMethod]
		public void ParameterDiscovery_CompositeParameter()
		{
			var parameterDefinitions = new Template(
				"${ Customer.Email }")
				.GetParameterDefinitions();

			TemplateAssert.AreParameterDefinitionsEqual(
				new[]
				{
					new CompositeParameterDefinition(
						"Customer",
						new IParameterDefinition[]
						{
							new ParameterDefinition(
								"Email",
								VariableType.Primitive)
						})
				},
				parameterDefinitions);
		}

		[TestMethod]
		public void ParameterDiscovery_CompositeParameterWithTwoFields()
		{
			var parameterDefinitions = new Template(
				"${ Customer.Email } ${ Customer.MobilePhone }")
				.GetParameterDefinitions();

			TemplateAssert.AreParameterDefinitionsEqual(
				new[]
				{
					new CompositeParameterDefinition(
						"Customer",
						new IParameterDefinition[]
						{
							new ParameterDefinition(
								"Email",
								VariableType.Primitive),

							new ParameterDefinition(
								"MobilePhone",
								VariableType.Primitive)
						})
				},
				parameterDefinitions);
		}

		[TestMethod]
		public void ParameterDiscovery_CompositeParameter_CaseInsensitive()
		{
			var parameterDefinitions = new Template(
				"${ Customer.Email } ${ CUSTOMER.MobilePhone } ${ cUsToMeR.Id }")
				.GetParameterDefinitions();

			TemplateAssert.AreParameterDefinitionsEqual(
				new[]
				{
					new CompositeParameterDefinition(
						"Customer",
						new IParameterDefinition[]
						{
							new ParameterDefinition(
								"Email",
								VariableType.Primitive),

							new ParameterDefinition(
								"Id",
								VariableType.Primitive),

							new ParameterDefinition(
								"MobilePhone",
								VariableType.Primitive)
						})
				},
				parameterDefinitions);
		}

		[TestMethod]
		public void ParameterDiscovery_SecondLevelCompositeParameterWithTwoFields()
		{
			var parameterDefinitions = new Template(
				"${ Customer.Contacts.Email } ${ Customer.Contacts.MobilePhone }")
				.GetParameterDefinitions();

			TemplateAssert.AreParameterDefinitionsEqual(
				new[]
				{
					new CompositeParameterDefinition(
						"Customer",
						new IParameterDefinition[]
						{
							new CompositeParameterDefinition(
								"Contacts",
								new IParameterDefinition[]
								{
									new ParameterDefinition(
										"Email",
										VariableType.Primitive),

									new ParameterDefinition(
										"MobilePhone",
										VariableType.Primitive)
								})
						})
				},
				parameterDefinitions);
		}

		[TestMethod]
		public void ParameterDiscovery_IfCondition_FirstLevelParameter()
		{
			var parameterDefinitions = new Template(
				@"@{ if IsDebug } 
					Text
				@{ end if }")
				.GetParameterDefinitions();

			TemplateAssert.AreParameterDefinitionsEqual(
				new[]
				{
					new ParameterDefinition(
						"IsDebug",
						VariableType.Boolean)
				},
				parameterDefinitions);
		}

		[TestMethod]
		public void ParameterDiscovery_IfCondition_SecondLevelParameters()
		{
			var parameterDefinitions = new Template(@"
				@{ if Context.IsDebug or Context.IsStaging } 
					Text
				@{ end if }
				")
				.GetParameterDefinitions();

			TemplateAssert.AreParameterDefinitionsEqual(
				new[]
				{
					new CompositeParameterDefinition(
						"Context",
						new IParameterDefinition[]
						{
							new ParameterDefinition(
								"IsDebug",
								VariableType.Boolean),
							new ParameterDefinition(
								"IsStaging",
								VariableType.Boolean)
						})
				},
				parameterDefinitions);
		}

		[TestMethod]
		public void ParameterDiscovery_IfCondition_SecondLevelParameter_AnotherFieldOnFirstLevel()
		{
			var parameterDefinitions = new Template(@"
				${ Context.Time }

				@{ if context.IsDebug } 
					Text
				@{ end if }
				")
				.GetParameterDefinitions();

			TemplateAssert.AreParameterDefinitionsEqual(
				new[]
				{
					new CompositeParameterDefinition(
						"Context",
						new IParameterDefinition[]
						{
							new ParameterDefinition(
								"IsDebug",
								VariableType.Boolean),
							new ParameterDefinition(
								"Time",
								VariableType.Primitive) 
						})
				},
				parameterDefinitions);
		}
		
		[TestMethod]
		public void ParameterDiscovery_ArithmeticExpression_FirstLevelParameter()
		{
			var parameterDefinitions = new Template(
				"${ ClickCount * 2 }")
				.GetParameterDefinitions();

			TemplateAssert.AreParameterDefinitionsEqual(
				new[]
				{
					new ParameterDefinition(
						"ClickCount",
						VariableType.Integer)
				},
				parameterDefinitions);
		}

		[TestMethod]
		public void ParameterDiscovery_ArithmeticExpression_SecondLevelParameters()
		{
			var parameterDefinitions = new Template(
				"${ 100 * Statistics.ClickCount / Statistics.SentCount }")
				.GetParameterDefinitions();

			TemplateAssert.AreParameterDefinitionsEqual(
				new[]
				{
					new CompositeParameterDefinition(
						"Statistics",
						new IParameterDefinition[]
						{
							new ParameterDefinition(
								"ClickCount",
								VariableType.Integer),
							new ParameterDefinition(
								"SentCount",
								VariableType.Integer),
						})
				},
				parameterDefinitions);
		}

		[TestMethod]
		public void ParameterDiscovery_ForLoop_FirstLevelParameterAsCollection()
		{
			var parameterDefinitions = new Template(@"
				@{ for element in Offers } 
					Text
				@{ end for }
				")
				.GetParameterDefinitions();

			TemplateAssert.AreParameterDefinitionsEqual(
				new[]
				{
					new ArrayParameterDefinition(
						"Offers",
						new IParameterDefinition[0])
				},
				parameterDefinitions);
		}

		[TestMethod]
		public void ParameterDiscovery_ForLoop_SecondLevelParameterAsCollection()
		{
			var parameterDefinitions = new Template(@"
				@{ for element in Customer.Orders } 
					Text
				@{ end for }
				")
				.GetParameterDefinitions();

			TemplateAssert.AreParameterDefinitionsEqual(
				new[]
				{
					new CompositeParameterDefinition(
						"Customer",
						new IParameterDefinition[]
						{
							new ArrayParameterDefinition(
								"Orders",
								new IParameterDefinition[0])
						})
				},
				parameterDefinitions);
		}

		[TestMethod]
		public void ParameterDiscovery_ForLoop_ElementFirstLevelField()
		{
			var parameterDefinitions = new Template(@"
				@{ for element in Offers } 
					${ element.Date }
				@{ end for }
				")
				.GetParameterDefinitions();

			TemplateAssert.AreParameterDefinitionsEqual(
				new[]
				{
					new ArrayParameterDefinition(
						"Offers",
						new IParameterDefinition[]
						{
							new ParameterDefinition(
								"Date",
								VariableType.Primitive)
						})
				},
				parameterDefinitions);
		}

		[TestMethod]
		public void ParameterDiscovery_ForLoop_MultipleElementFirstLevelFields()
		{
			var parameterDefinitions = new Template(@"
				@{ for element in Offers } 
					${ element.Date }
					${ element.Url }
				@{ end for }
				")
				.GetParameterDefinitions();

			TemplateAssert.AreParameterDefinitionsEqual(
				new[]
				{
					new ArrayParameterDefinition(
						"Offers",
						new IParameterDefinition[]
						{
							new ParameterDefinition(
								"Date",
								VariableType.Primitive),
							new ParameterDefinition(
								"Url",
								VariableType.Primitive)
						})
				},
				parameterDefinitions);
		}

		[TestMethod]
		public void ParameterDiscovery_ForLoop_MultipleElementSecondLevelFields()
		{
			var parameterDefinitions = new Template(@"
				@{ for element in Offers } 
					${ element.Product.Price }
					${ element.Product.Name }
				@{ end for }
				")
				.GetParameterDefinitions();

			TemplateAssert.AreParameterDefinitionsEqual(
				new[]
				{
					new ArrayParameterDefinition(
						"Offers",
						new IParameterDefinition[]
						{
							new CompositeParameterDefinition(
								"Product",
								new IParameterDefinition[]
								{
									new ParameterDefinition(
										"Name",
										VariableType.Primitive),

									new ParameterDefinition(
										"Price",
										VariableType.Primitive)
								}), 
						})
				},
				parameterDefinitions);
		}

		[TestMethod]
		public void ParameterDiscovery_ForLoop_IfConditionOnElementVariable()
		{
			var parameterDefinitions = new Template(@"
				@{ for element in Offers } 
					@{ if element.IsOpen }
						Try our offer
					@{ end if }
				@{ end for }
				")
				.GetParameterDefinitions();

			TemplateAssert.AreParameterDefinitionsEqual(
				new[]
				{
					new ArrayParameterDefinition(
						"Offers",
						new IParameterDefinition[]
						{
							new ParameterDefinition(
								"IsOpen",
								VariableType.Boolean)
						})
				},
				parameterDefinitions);
		}

		[TestMethod]
		public void ParameterDiscovery_ForLoop_InnerForUsingFieldOfOuterForElement()
		{
			var parameterDefinitions = new Template(@"
				@{ for offer in Offers } 
					${ offer.Date }

					@{ for season in Seasons } 
						${ offer.Price }
					@{ end for }
				@{ end for }
				")
				.GetParameterDefinitions();

			TemplateAssert.AreParameterDefinitionsEqual(
				new[]
				{
					new ArrayParameterDefinition(
						"Offers",
						new IParameterDefinition[]
						{
							new ParameterDefinition(
								"Date",
								VariableType.Primitive),
							new ParameterDefinition(
								"Price",
								VariableType.Primitive)
						}),
					new ArrayParameterDefinition(
						"Seasons",
						new IParameterDefinition[0]
					)
				},
				parameterDefinitions);
		}

		[TestMethod]
		public void ParameterDiscovery_AdjacentForLoops_SameElementVariableNameForDifferentCollections()
		{
			var parameterDefinitions = new Template(@"
				@{ for offer in Offers } 
					${ offer.Text }
				@{ end for }

				@{ for offer in OtherOffers } 
					${ offer.Url }
				@{ end for }
				")
				.GetParameterDefinitions();

			TemplateAssert.AreParameterDefinitionsEqual(
				new[]
				{
					new ArrayParameterDefinition(
						"Offers",
						new IParameterDefinition[]
						{
							new ParameterDefinition(
								"Text",
								VariableType.Primitive)
						}),
					new ArrayParameterDefinition(
						"OtherOffers",
						new IParameterDefinition[]
						{
							new ParameterDefinition(
								"Url",
								VariableType.Primitive)
						})
				},
				parameterDefinitions);
		}

		[TestMethod]
		public void ParameterDiscovery_AdjacentForLoops_SameElementVariableNameForSameCollection()
		{
			var parameterDefinitions = new Template(@"
				@{ for offer in Offers } 
					${ offer.Text }
				@{ end for }

				@{ for offer in Offers } 
					${ offer.Url }
				@{ end for }
				")
				.GetParameterDefinitions();

			TemplateAssert.AreParameterDefinitionsEqual(
				new[]
				{
					new ArrayParameterDefinition(
						"Offers",
						new IParameterDefinition[]
						{
							new ParameterDefinition(
								"Text",
								VariableType.Primitive),
							new ParameterDefinition(
								"Url",
								VariableType.Primitive)
						})
				},
				parameterDefinitions);
		}

		[TestMethod]
		public void ParameterDiscovery_AdjacentForLoops_DifferentElementVariableNameForSameCollection()
		{
			var parameterDefinitions = new Template(@"
				@{ for offer in Offers } 
					${ offer.Text }
				@{ end for }

				@{ for myOffer in Offers } 
					${ myOffer.Url }
				@{ end for }
				")
				.GetParameterDefinitions();

			TemplateAssert.AreParameterDefinitionsEqual(
				new[]
				{
					new ArrayParameterDefinition(
						"Offers",
						new IParameterDefinition[]
						{
							new ParameterDefinition(
								"Text",
								VariableType.Primitive),
							new ParameterDefinition(
								"Url",
								VariableType.Primitive)
						})
				},
				parameterDefinitions);
		}
	}
}
