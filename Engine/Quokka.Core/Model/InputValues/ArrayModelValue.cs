using System;
using System.Collections.Generic;
using System.Linq;

namespace Mindbox.Quokka
{
	public class ArrayModelValue : IArrayModelValue
	{
		public IList<IModelValue> Elements { get; }

		internal ArrayModelValue(params IModelValue[] elements)
			: this((IEnumerable<IModelValue>)elements)
		{
		}

		public ArrayModelValue(IEnumerable<IModelValue> elements)
			: this(Enumerable.Empty<IModelField>(), Enumerable.Empty<IModelMethod>(), elements)
		{
		}
		
		public ArrayModelValue(
			IEnumerable<IModelField> fields,
			IEnumerable<IModelMethod> methods,
			IEnumerable<IModelValue> elements)
			//: base(fields, methods)
		{
			if (elements == null)
				throw new ArgumentNullException(nameof(elements));

			Elements = elements
				.ToList()
				.AsReadOnly();
		}
	}
}