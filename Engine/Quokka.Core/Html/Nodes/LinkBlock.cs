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
using System.IO;
using System.Linq;
using System.Text;

namespace Mindbox.Quokka.Html
{
	internal class LinkBlock : TemplateNodeBase, IStaticBlockPart
	{
		public int Offset => hrefValue.Offset;
		public int Length => hrefValue.Length;

		public readonly AttributeValue hrefValue;
		public readonly AttributeValue nameValue;

		private readonly Guid uniqueKey;

		public LinkBlock(AttributeValue hrefValue, AttributeValue nameValue)
		{
			this.hrefValue = hrefValue;
			this.nameValue = nameValue;
			uniqueKey = Guid.NewGuid();
		}

		public override void PerformSemanticAnalysis(AnalysisContext context)
		{
			if (!hrefValue.IsQuoted)
			{
				var htmlErrorSubListener = context.ErrorListener.GetRegisteredSubListener<HtmlSemanticErrorSubListener>();
				htmlErrorSubListener.AddHrefAttributeMustBeQuotedError(hrefValue.Location);
			}

			foreach (var component in hrefValue.TextComponents)
			{
				component.PerformSemanticAnalysis(context);
			}
		}

		public override void Render(TextWriter resultWriter, RenderContext renderContext)
		{
			var htmlRenderContext = (HtmlRenderContext)renderContext;

			var linkBuilder = new StringBuilder();
			using (var linkWriter = new StringWriter(linkBuilder))
			{
				foreach (var component in hrefValue.TextComponents)
				{
					component.Render(linkWriter, renderContext);
				}
			}

			string redirectUrl = linkBuilder.ToString();
			if (htmlRenderContext.RedirectLinkProcessor != null)
			{
				string processedRedirectUrl = htmlRenderContext.RedirectLinkProcessor(uniqueKey, redirectUrl);
				resultWriter.Write(processedRedirectUrl);
			}
			else
			{
				resultWriter.Write(redirectUrl);
			}
		}

		public override void CompileGrammarSpecificData(GrammarSpecificDataAnalysisContext context)
		{
			var htmlContext = (HtmlDataAnalysisContext)context;

			htmlContext.AddReference(
				new Reference(
					hrefValue.Text,
					nameValue?.Text,
					uniqueKey,
					isConstant: !hrefValue.TextComponents.OfType<OutputInstructionBlock>().Any()));
		}

		public override BlockDTO GetTreeDTO()
		{
			var result = base.GetTreeDTO();
			result.type = "LinkBlock";
			result.children = hrefValue.TextComponents.Select(c => c.GetTreeDTO()).ToList();
			return result;
		}

	}
}
