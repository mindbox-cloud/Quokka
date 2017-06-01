using System;
using System.Collections.Generic;
using System.Linq;

namespace Mindbox.Quokka
{
	public class ArrayModelValue : CompositeModelValue, IArrayModelValue
	{
		public IList<IModelValue> Elements { get; }

		internal ArrayModelValue(params IModelValue[] elements)
			: this((IEnumerable<IModelValue>)elements)
		{
		}

		public ArrayModelValue(IEnumerable<IModelValue> elements)
			: this(elements, Enumerable.Empty<IModelField>(), Enumerable.Empty<IModelMethod>())
		{
		}

		public ArrayModelValue(
			IEnumerable<IModelValue> elements,
			IEnumerable<IModelField> fields = null,
			IEnumerable<IModelMethod> methods = null)
			: base(fields, methods)
		{
			if (elements == null)
				throw new ArgumentNullException(nameof(elements));

			Elements = elements
				.ToList()
				.AsReadOnly();
		}
	}
}