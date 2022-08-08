using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mindbox.Quokka.Abstractions;

namespace Mindbox.Quokka
{
	public interface ITemplate : IRenderWithParameters
	{
		bool IsConstant { get; }

		IList<ITemplateError> Errors { get; }

		ICompositeModelDefinition GetModelDefinition();

		string Render(ICompositeModelValue model, CallContextContainer callContextContainer = null);

		void Render(TextWriter textWriter, ICompositeModelValue model, CallContextContainer callContextContainer = null);
	}
}
