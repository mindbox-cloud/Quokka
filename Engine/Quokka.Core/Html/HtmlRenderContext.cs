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
using System.IO;

using Mindbox.Quokka.Abstractions;

namespace Mindbox.Quokka.Html
{
	internal class HtmlRenderContext : RenderContext
	{
		private readonly HtmlRenderContext parentRenderContext;

		public bool HasIdentificationCodeBeenRendered { get; private set; }

		public Func<Guid, string, string> RedirectLinkProcessor { get; } 

		public string IdentificationCode { get; } 
		
		public bool HasPreHeaderBeenRendered { get; private set; }
		
		public string PreHeader { get; } 

		public HtmlRenderContext(
			RuntimeVariableScope variableScope,
			FunctionRegistry functions, 
			Func<Guid, string, string> redirectLinkProcessor,
			string identificationCode,
			string preHeader,
			RenderSettings settings,
			ICallContextContainer callContextContainer)
			: this(variableScope, functions, redirectLinkProcessor, identificationCode, preHeader, settings, callContextContainer, null)
		{
			// empty
		}

		private HtmlRenderContext(
			RuntimeVariableScope variableScope,
			FunctionRegistry functions, 
			Func<Guid, string, string> redirectLinkProcessor,
			string identificationCode,
			string preHeader,
			RenderSettings settings,
			ICallContextContainer callContextContainer,
			HtmlRenderContext parentRenderContext)
			: base(variableScope, functions, settings, callContextContainer)
		{
			RedirectLinkProcessor = redirectLinkProcessor;
			IdentificationCode = identificationCode;
			PreHeader = preHeader;
			this.parentRenderContext = parentRenderContext;
		}

		public override RenderContext CreateInnerContext(RuntimeVariableScope variableScope)
		{
			return new HtmlRenderContext(
				variableScope, Functions, RedirectLinkProcessor, IdentificationCode, PreHeader, Settings, CallContextContainer, this)
			{
				HasIdentificationCodeBeenRendered = HasIdentificationCodeBeenRendered,
				HasPreHeaderBeenRendered = HasIdentificationCodeBeenRendered
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
		
		public void LogPreHeaderRendering()
		{
			HasPreHeaderBeenRendered = true;
			if (parentRenderContext != null)
				parentRenderContext.HasPreHeaderBeenRendered = true;
		}
	}
}
