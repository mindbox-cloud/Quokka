﻿using System;
using System.IO;

namespace Mindbox.Quokka.Html
{
	internal class HtmlRenderContext : RenderContext
	{
		private readonly HtmlRenderContext parentRenderContext;

		public bool HasIdentificationCodeBeenRendered { get; private set; }

		public Func<Guid, string, string> RedirectLinkProcessor { get; } 

		public string IdentificationCode { get; } 

		public HtmlRenderContext(
			RuntimeVariableScope variableScope,
			FunctionRegistry functions, 
			Func<Guid, string, string> redirectLinkProcessor,
			string identificationCode,
			ICallContextContainer callContextContainer)
			: this(variableScope, functions, redirectLinkProcessor, identificationCode, callContextContainer, null)
		{
			// empty
		}

		private HtmlRenderContext(
			RuntimeVariableScope variableScope,
			FunctionRegistry functions, 
			Func<Guid, string, string> redirectLinkProcessor,
			string identificationCode,
			ICallContextContainer callContextContainer,
			HtmlRenderContext parentRenderContext)
			: base(variableScope, functions, callContextContainer)
		{
			RedirectLinkProcessor = redirectLinkProcessor;
			IdentificationCode = identificationCode;
			this.parentRenderContext = parentRenderContext;
		}

		public override RenderContext CreateInnerContext(RuntimeVariableScope variableScope)
		{
			return new HtmlRenderContext(
				variableScope, Functions, RedirectLinkProcessor, IdentificationCode, CallContextContainer, this)
			{
				HasIdentificationCodeBeenRendered = HasIdentificationCodeBeenRendered
			};
		}

		public override void OnRenderingEnd(TextWriter resultWriter)
		{
			if (IdentificationCode != null && !HasIdentificationCodeBeenRendered)
				resultWriter.Write(IdentificationCode);
		}

		public void LogIdentificationCodeRendering()
		{
			HasIdentificationCodeBeenRendered = true;
			if (parentRenderContext != null)
				parentRenderContext.HasIdentificationCodeBeenRendered = true;
		}
	}
}
