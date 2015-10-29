using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quokka.Html
{
	internal class LinkBlock : TemplateNodeBase, IStaticBlockPart
	{
		/// <summary>
		/// Link url may consist of multiple constant blocks and parameter/expression output blocks,
		/// e.g. "http://${ domain }/index.${ extension }". So we store 
		/// </summary>
		private readonly IReadOnlyList<ITemplateNode> urlComponents;

		private readonly string redirectUrlText;

		private readonly Guid uniqueKey;

		public int Offset { get; }
		public int Length { get; }

		public LinkBlock(IReadOnlyList<ITemplateNode> urlComponents, string redirectUrlText, int offset, int length)
		{
			this.urlComponents = urlComponents;
			this.redirectUrlText = redirectUrlText;
			Offset = offset;
			Length = length;

			uniqueKey = Guid.NewGuid();
		}

		public override void CompileVariableDefinitions(SemanticAnalysisContext context)
		{
			foreach (var component in urlComponents)
			{
				component.CompileVariableDefinitions(context);
			}
		}

		public override void Render(StringBuilder resultBuilder, RenderContext context)
		{
			var htmlRenderContext = (HtmlRenderContext)context;

			var linkBuilder = new StringBuilder();

			foreach (var component in urlComponents)
			{
				component.Render(linkBuilder, context);
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
					redirectUrlText,
					null,
					uniqueKey,
					isConstant: !urlComponents.OfType<OutputInstructionBlock>().Any()));
		}
	}
}
