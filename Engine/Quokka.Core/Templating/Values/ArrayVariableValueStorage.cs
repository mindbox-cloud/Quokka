using System;
using System.Collections.Generic;
using System.Linq;

namespace Quokka
{
	internal class ArrayVariableValueStorage : VariableValueStorage
	{
		private readonly IEnumerable<VariableValueStorage> elements;

		public ArrayVariableValueStorage(IArrayModelValue modelValue)
		{
			if (modelValue == null)
				throw new ArgumentNullException(nameof(modelValue));

			elements = modelValue
				.Values
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