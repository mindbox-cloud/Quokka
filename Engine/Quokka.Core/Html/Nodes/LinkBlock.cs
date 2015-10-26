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

		public int Offset { get; }
		public int Length { get; }

		public LinkBlock(IReadOnlyList<ITemplateNode> urlComponents, string redirectUrlText, int offset, int length)
		{
			this.urlComponents = urlComponents;
			this.redirectUrlText = redirectUrlText;
			Offset = offset;
			Length = length;
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
			foreach (var component in urlComponents)
			{
				component.Render(resultBuilder, context);
			}
		}

		public override void CompileGrammarSpecificData(GrammarSpecificDataAnalysisContext context)
		{
			var htmlContext = (HtmlDataAnalysisContext)context;
			htmlContext.AddReference(
				new Reference(
					redirectUrlText,
					null,
					isConstant: !urlComponents.OfType<OutputInstructionBlock>().Any()));
		}
	}
}
