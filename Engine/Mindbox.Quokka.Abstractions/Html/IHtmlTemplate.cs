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

using Mindbox.Quokka.Abstractions;

namespace Mindbox.Quokka.Html
{
	public interface IHtmlTemplate : ITemplate
	{
		/// <summary>
		/// Get a collection of external references (links) ordered from top to bottom.
		/// </summary>
		IReadOnlyList<Reference> GetReferences();

		/// <summary>
		/// Render Html message
		/// </summary>
		/// <param name="model">Model for parameters</param>
		/// <param name="redirectLinkProcessor">Action that will be applied to each link url</param>
		/// <param name="identificationCode">Html code that will be rendered at the end of the document (if specified)</param>
		/// <param name="preHeader">Html code that will be rendered at the beginning of the document (if specified)</param>
		/// <param name="callContextContainer">Container of call context values that can be used in functions with context</param>
		/// <returns>Rendered Html message</returns>
		string Render(
			ICompositeModelValue model, 
			Func<Guid, string, string> redirectLinkProcessor,
			string identificationCode, 
			string preHeader,
			ICallContextContainer callContextContainer = null);
		
		/// <summary>
		/// Render Html message
		/// </summary>
		/// <param name="model">Model for parameters</param>
		/// <param name="settings">Settings applied to rendering process (ex. Localization for format functions)</param>
		/// <param name="redirectLinkProcessor">Action that will be applied to each link url</param>
		/// <param name="identificationCode">Html code that will be rendered at the end of the document (if specified)</param>
		/// <param name="preHeader">Html code that will be rendered at the beginning of the document (if specified)</param>
		/// <param name="callContextContainer">Container of call context values that can be used in functions with context</param>
		/// <returns></returns>
		string Render(
			ICompositeModelValue model, 
			RenderSettings settings,
			Func<Guid, string, string> redirectLinkProcessor,
			string identificationCode, 
			string preHeader,
			ICallContextContainer callContextContainer = null);
		
		void Render(
			TextWriter textWriter,
			ICompositeModelValue model,
			Func<Guid, string, string> redirectLinkProcessor,
			string identificationCode = null,
			string preHeader = null,
			ICallContextContainer callContextContainer = null);
		
		void Render(
			TextWriter textWriter,
			ICompositeModelValue model,
			RenderSettings settings,
			Func<Guid, string, string> redirectLinkProcessor,
			string identificationCode = null,
			string preHeader= null,
			ICallContextContainer callContextContainer = null);
	}
}