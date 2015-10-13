using System.Collections.Generic;

namespace Quokka
{
	internal interface IErrorListener
	{
		IReadOnlyCollection<ITemplateError> GetErrors();
	}
}