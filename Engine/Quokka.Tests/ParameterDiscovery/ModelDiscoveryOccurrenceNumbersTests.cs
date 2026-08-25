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

using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Mindbox.Quokka.Tests;

namespace Mindbox.Quokka
{
	[TestClass]
	public class ModelDiscoveryOccurrenceNumbersTests
	{
		private static readonly string[] NonIdempotentMethodNames = { "Refresh" };

		[TestMethod]
		public void ModelDiscovery_NonIdempotentMethod_IdenticalCalls_GetSeparateDefinitions()
		{
			var modelDefinition = CreateTemplate("${ Object.Refresh() }${ Object.Refresh() }")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(
					new Dictionary<string, IModelDefinition>
					{
						{
							"Object", new CompositeModelDefinition(
								methods: new Dictionary<IMethodCallDefinition, IModelDefinition>
								{
									{ Refresh(1), new PrimitiveModelDefinition(TypeDefinition.Primitive) },
									{ Refresh(2), new PrimitiveModelDefinition(TypeDefinition.Primitive) }
								})
						}
					}),
				modelDefinition);
		}

		[TestMethod]
		public void ModelDiscovery_IdempotentMethod_IdenticalCalls_ShareASingleDefinition()
		{
			var modelDefinition = new Template("${ Object.Refresh() }${ Object.Refresh() }")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(
					new Dictionary<string, IModelDefinition>
					{
						{
							"Object", new CompositeModelDefinition(
								methods: new Dictionary<IMethodCallDefinition, IModelDefinition>
								{
									{ Refresh(), new PrimitiveModelDefinition(TypeDefinition.Primitive) }
								})
						}
					}),
				modelDefinition);
		}

		[TestMethod]
		public void ModelDiscovery_NonIdempotentMethod_CallsOnDifferentOwners_AreNumberedIndependently()
		{
			var modelDefinition = CreateTemplate(
					"${ First.Refresh() }${ Second.Refresh() }${ First.Refresh() }")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(
					new Dictionary<string, IModelDefinition>
					{
						{
							"First", new CompositeModelDefinition(
								methods: new Dictionary<IMethodCallDefinition, IModelDefinition>
								{
									{ Refresh(1), new PrimitiveModelDefinition(TypeDefinition.Primitive) },
									{ Refresh(2), new PrimitiveModelDefinition(TypeDefinition.Primitive) }
								})
						},
						{
							"Second", new CompositeModelDefinition(
								methods: new Dictionary<IMethodCallDefinition, IModelDefinition>
								{
									{ Refresh(1), new PrimitiveModelDefinition(TypeDefinition.Primitive) }
								})
						}
					}),
				modelDefinition);
		}

		[TestMethod]
		public void ModelDiscovery_NonIdempotentMethod_CallsWithDifferentArguments_AreNumberedIndependently()
		{
			var modelDefinition = CreateTemplate(
					"${ Object.Refresh('a') }${ Object.Refresh('b') }${ Object.Refresh('a') }")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(
					new Dictionary<string, IModelDefinition>
					{
						{
							"Object", new CompositeModelDefinition(
								methods: new Dictionary<IMethodCallDefinition, IModelDefinition>
								{
									{ Refresh("a", 1), new PrimitiveModelDefinition(TypeDefinition.Primitive) },
									{ Refresh("b", 1), new PrimitiveModelDefinition(TypeDefinition.Primitive) },
									{ Refresh("a", 2), new PrimitiveModelDefinition(TypeDefinition.Primitive) }
								})
						}
					}),
				modelDefinition);
		}

		[TestMethod]
		public void ModelDiscovery_NonIdempotentMethodName_IsMatchedCaseInsensitively()
		{
			var modelDefinition = new DefaultTemplateFactory(nonIdempotentMethodNames: new[] { "REFRESH" })
				.CreateTemplate("${ Object.Refresh() }${ Object.Refresh() }")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(
					new Dictionary<string, IModelDefinition>
					{
						{
							"Object", new CompositeModelDefinition(
								methods: new Dictionary<IMethodCallDefinition, IModelDefinition>
								{
									{ Refresh(1), new PrimitiveModelDefinition(TypeDefinition.Primitive) },
									{ Refresh(2), new PrimitiveModelDefinition(TypeDefinition.Primitive) }
								})
						}
					}),
				modelDefinition);
		}

		[TestMethod]
		public void ModelDiscovery_NonIdempotentMethod_HtmlTemplate_IdenticalCalls_GetSeparateDefinitions()
		{
			var modelDefinition = new DefaultTemplateFactory(nonIdempotentMethodNames: NonIdempotentMethodNames)
				.CreateHtmlTemplate("<p>${ Object.Refresh() }${ Object.Refresh() }</p>")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(
					new Dictionary<string, IModelDefinition>
					{
						{
							"Object", new CompositeModelDefinition(
								methods: new Dictionary<IMethodCallDefinition, IModelDefinition>
								{
									{ Refresh(1), new PrimitiveModelDefinition(TypeDefinition.Primitive) },
									{ Refresh(2), new PrimitiveModelDefinition(TypeDefinition.Primitive) }
								})
						}
					}),
				modelDefinition);
		}

		[TestMethod]
		public void ModelDiscovery_NonIdempotentMethod_IdenticalCallChains_GetSeparateResultDefinitions()
		{
			var modelDefinition = CreateTemplate(@"
					@{ for item in Object.Refresh().GetItems() }${ item.Name }@{ end for }
					@{ for item in Object.Refresh().GetItems() }${ item.Price }@{ end for }
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
									{ Refresh(1), ItemCollection("Name") },
									{ Refresh(2), ItemCollection("Price") }
								})
						}
					}),
				modelDefinition);
		}

		[TestMethod]
		public void ModelDiscovery_NonIdempotentMethod_CallsOnCollectionElement_ShareOneDefinition()
		{
			var modelDefinition = CreateTemplate(@"
					@{ for a in Collection }${ a.Refresh().X }@{ end for }
					@{ for b in Collection }${ b.Refresh().Y }@{ end for }
				")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(
					new Dictionary<string, IModelDefinition>
					{
						{
							"Collection", new ArrayModelDefinition(
								new CompositeModelDefinition(
									methods: new Dictionary<IMethodCallDefinition, IModelDefinition>
									{
										{
											Refresh(1),
											new CompositeModelDefinition(
												new Dictionary<string, IModelDefinition>
												{
													{ "X", new PrimitiveModelDefinition(TypeDefinition.Primitive) },
													{ "Y", new PrimitiveModelDefinition(TypeDefinition.Primitive) }
												})
										}
									}))
						}
					}),
				modelDefinition);
		}

		[TestMethod]
		public void ModelDiscovery_NonIdempotentMethod_CallsInConditionArms_GetSeparateDefinitions()
		{
			var modelDefinition = CreateTemplate(
					"@{ if Flag }${ Object.Refresh() }@{ else }${ Object.Refresh() }@{ end if }")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(
					new Dictionary<string, IModelDefinition>
					{
						{ "Flag", new PrimitiveModelDefinition(TypeDefinition.Boolean) },
						{
							"Object", new CompositeModelDefinition(
								methods: new Dictionary<IMethodCallDefinition, IModelDefinition>
								{
									{ Refresh(1), new PrimitiveModelDefinition(TypeDefinition.Primitive) },
									{ Refresh(2), new PrimitiveModelDefinition(TypeDefinition.Primitive) }
								})
						}
					}),
				modelDefinition);
		}

		[TestMethod]
		public void ModelDiscovery_NonIdempotentMethod_CallInLoopBody_IsNumberedOnce()
		{
			var modelDefinition = CreateTemplate(
					"@{ for item in Collection }${ Object.Refresh() }@{ end for }")
				.GetModelDefinition();

			TemplateAssert.AreCompositeModelDefinitionsEqual(
				new CompositeModelDefinition(
					new Dictionary<string, IModelDefinition>
					{
						{
							"Collection",
							new ArrayModelDefinition(new PrimitiveModelDefinition(TypeDefinition.Unknown))
						},
						{
							"Object", new CompositeModelDefinition(
								methods: new Dictionary<IMethodCallDefinition, IModelDefinition>
								{
									{ Refresh(1), new PrimitiveModelDefinition(TypeDefinition.Primitive) }
								})
						}
					}),
				modelDefinition);
		}

		private static IModelDefinition ItemCollection(string itemFieldName)
		{
			return new CompositeModelDefinition(
				methods: new Dictionary<IMethodCallDefinition, IModelDefinition>
				{
					{
						new MethodCallDefinition("GetItems", Array.Empty<IMethodArgumentDefinition>()),
						new ArrayModelDefinition(
							new CompositeModelDefinition(
								new Dictionary<string, IModelDefinition>
								{
									{ itemFieldName, new PrimitiveModelDefinition(TypeDefinition.Primitive) }
								}))
					}
				});
		}

		private static MethodCallDefinition Refresh(int? occurrenceNumber = null)
		{
			return new MethodCallDefinition(
				"Refresh",
				Array.Empty<IMethodArgumentDefinition>(),
				occurrenceNumber);
		}

		private static MethodCallDefinition Refresh(string argument, int? occurrenceNumber)
		{
			return new MethodCallDefinition(
				"Refresh",
				new[] { new MethodArgumentDefinition(TypeDefinition.String, argument) },
				occurrenceNumber);
		}

		private static ITemplate CreateTemplate(string templateText)
		{
			return new DefaultTemplateFactory(nonIdempotentMethodNames: NonIdempotentMethodNames)
				.CreateTemplate(templateText);
		}
	}
}
