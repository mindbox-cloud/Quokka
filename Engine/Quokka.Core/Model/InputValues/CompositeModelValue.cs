using System;
using System.Collections.Generic;
using System.Linq;

namespace Mindbox.Quokka
{
	public class CompositeModelValue : ICompositeModelValue
	{
		public IEnumerable<IModelField> Fields { get; }
		public IEnumerable<IModelMethod> Methods { get; }

		internal CompositeModelValue()
			:this(Array.Empty<IModelField>(), Array.Empty<IModelMethod>())
		{
		}

		internal CompositeModelValue(params IModelField[] fields)
			: this((IEnumerable<IModelField>)fields)
		{
		}

		internal CompositeModelValue(params IModelMethod[] methods)
			: this((IEnumerable<IModelMethod>)methods)
		{
		}

		internal CompositeModelValue(IEnumerable<IModelField> fields)
			: this(fields, Array.Empty<IModelMethod>())
		{
		}
		internal CompositeModelValue(IEnumerable<IModelMethod> methods)
			: this(Array.Empty<IModelField>(), methods)
		{
		}

		public CompositeModelValue(IEnumerable<IModelField> fields, IEnumerable<IModelMethod> methods)
		{
			if (fields == null)
				throw new ArgumentNullException(nameof(fields));
			if (methods == null)
				throw new ArgumentNullException(nameof(methods));

			Fields = fields.ToList();

			Methods = methods
				.ToList()
				.AsReadOnly();
		}

	}
}