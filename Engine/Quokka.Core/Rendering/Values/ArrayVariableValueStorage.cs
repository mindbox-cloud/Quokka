using System;
using System.Collections.Generic;
using System.Linq;

namespace Mindbox.Quokka
{
	internal class ArrayVariableValueStorage : CompositeVariableValueStorage
	{
		private readonly IEnumerable<VariableValueStorage> elements;
		
		public ArrayVariableValueStorage(IArrayModelValue modelValue)
			: base(modelValue)
		{
			if (modelValue == null)
				throw new ArgumentNullException(nameof(modelValue));
			
			elements = modelValue
				.Elements
				.Select(CreateStorageForValue)
				.ToList()
				.AsReadOnly();
		}

		public override IEnumerable<VariableValueStorage> GetElements()
		{
			return elements;
		}

		public override bool CheckIfValueIsNull()
		{
			return false;
		}
	}
}