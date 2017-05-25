using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mindbox.Quokka.Html
{
	internal class LinkBlock : TemplateNodeBase, IStaticBlockPart
	{
		public int Offset => hrefValue.Offset;
		public int Length => hrefValue.Length;

		private readonly AttributeValue hrefValue;
		private readonly AttributeValue nameValue;

		private readonly Guid uniqueKey;

		public LinkBlock(AttributeValue hrefValue, AttributeValue nameValue)
		{
			this.hrefValue = hrefValue;
			this.nameValue = nameValue;
			uniqueKey = Guid.NewGuid();
		}

		public override void CompileVariableDefinitions(SemanticAnalysisContext context)
		{
			foreach (var component in hrefValue.TextComponents)
			{
				component.CompileVariableDefinitions(context);
			}
		}

		public override void Render(StringBuilder resultBuilder, RenderContext renderContext)
		{
			var htmlRenderContext = (HtmlRenderContext)renderContext;

			var linkBuilder = new StringBuilder();

			foreach (var component in hrefValue.TextComponents)
			{
				component.Render(linkBuilder, renderContext);
			}

			string redirectUrl = linkBuilder.ToString();
			if (htmlRenderContext.RedirectLinkProcessor != null)
			{
				string processedRedirectUrl = htmlRenderContext.RedirectLinkProcessor(uniqueKey, redirectUrl);
				resultBuilder.Append(processedRedirectUrl);
			}
			else
			{
				resultBuilder.Append(redirectUrl);
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
	}
}
