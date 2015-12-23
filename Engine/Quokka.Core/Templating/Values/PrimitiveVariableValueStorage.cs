using System;

namespace Quokka
{
	internal class PrimitiveVariableValueStorage : VariableValueStorage
	{
		private readonly IPrimitiveModelValue primitiveModel;

		public PrimitiveVariableValueStorage(IPrimitiveModelValue primitiveModel)
		{
			this.primitiveModel = primitiveModel;
		}

		public PrimitiveVariableValueStorage(object primitiveValue)
		{
			this.primitiveModel = new PrimitiveModelValue(primitiveValue);
		}

		public override object GetPrimitiveValue()
		{
			return primitiveModel.Value;
		}

		public override bool CheckIfValueIsNull()
		{
			return primitiveModel.Value == null;
		}
	}
}