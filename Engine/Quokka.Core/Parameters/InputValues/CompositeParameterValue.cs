using System.Collections.Generic;
using System.Linq;

namespace Quokka
{
	internal class CompositeParameterValue : ICompositeParameterValue
	{
		public IList<IParameterField> Fields { get; }

		public CompositeParameterValue(params IParameterField[] fields)
		{
			Fields = fields
				.ToList()
				.AsReadOnly();
		}
	}
}