using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mindbox.Quokka
{
	public interface ITemplate
	{
		bool IsConstant { get; }

		IList<ITemplateError> Errors { get; }

		ICompositeModelDefinition GetModelDefinition();

		string Render(ICompositeModelValue model, CallContextContainer callContextContainer = null);
	}
}
