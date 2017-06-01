using System;
using System.Collections.Generic;
using System.Linq;

namespace Mindbox.Quokka
{
	public class CompositeModelValue : ICompositeModelValue
	{
		public IEnumerable<IModelField> Fields { get; }
		public IEnumerable<IModelMethod> Methods { get; }

		public CompositeModelValue()
			: this(Enumerable.Empty<IModelField>(), Enumerable.Empty<IModelMethod>())
		{
		}

		public CompositeModelValue(params IModelField[] fields)
			: this((IEnumerable<IModelField>)fields)
		{
		}

		public CompositeModelValue(params IModelMethod[] methods)
			: this((IEnumerable<IModelMethod>)methods)
		{
		}

		public CompositeModelValue(IEnumerable<IModelField> fields)
			: this(fields, Enumerable.Empty<IModelMethod>())
		{
		}
		public CompositeModelValue(IEnumerable<IModelMethod> methods)
			: this(Enumerable.Empty<IModelField>(), methods)
		{
		}

		public CompositeModelValue(IEnumerable<IModelField> fields, IEnumerable<IModelMethod> methods)
		{
			Fields = fields?.ToArray() ?? Array.Empty<IModelField>();
			Methods = methods?.ToArray() ?? Array.Empty<IModelMethod>();
		}
	}
}