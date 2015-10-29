using System;
using System.Text;

namespace Quokka.Html
{
	internal class HtmlRenderContext : RenderContext
	{
		private readonly HtmlRenderContext parentRenderContext;

		public bool HasIdentificationCodeBeenRendered { get; private set; }

		public Func<Guid, string, string> RedirectLinkProcessor { get; } 

		public string IdentificationCode { get; } 

		public HtmlRenderContext(
			RuntimeVariableScope variableScope,
			FunctionRegistry functions, Func<Guid,
			string, string> redirectLinkProcessor,
			string identificationCode,
			HtmlRenderContext parentRenderContext = null)
			: base(variableScope, functions)
		{
			RedirectLinkProcessor = redirectLinkProcessor;
			IdentificationCode = identificationCode;
			this.parentRenderContext = parentRenderContext;
		}

		public override RenderContext CreateInnerContext(RuntimeVariableScope variableScope)
		{
			return new HtmlRenderContext(variableScope, Functions, RedirectLinkProcessor, IdentificationCode, this)
			{
				HasIdentificationCodeBeenRendered = HasIdentificationCodeBeenRendered
			};
		}

		public override void OnRenderingEnd(StringBuilder resultBuilder)
		{
			if (IdentificationCode != null && !HasIdentificationCodeBeenRendered)
				resultBuilder.Append(IdentificationCode);
		}

		public void LogIdentificationCodeRendering()
		{
			HasIdentificationCodeBeenRendered = true;
			if (parentRenderContext != null)
				parentRenderContext.HasIdentificationCodeBeenRendered = true;
		}
	}
}
